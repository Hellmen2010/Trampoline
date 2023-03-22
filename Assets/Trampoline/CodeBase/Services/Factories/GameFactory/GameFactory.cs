using Trampoline.CodeBase.Core.Element;
using Trampoline.CodeBase.Core.Spawner;
using Trampoline.CodeBase.Core.Trampoline;
using Trampoline.CodeBase.Infrastructure;
using Trampoline.CodeBase.Services.EntityContainer;
using Trampoline.CodeBase.Services.Sound;
using Trampoline.CodeBase.Services.StaticData;
using UnityEngine;
using UnityEngine.Pool;

namespace Trampoline.CodeBase.Services.Factories.GameFactory
{
    public class GameFactory : IGameFactory
    {
        private readonly IStaticData _staticData;
        private readonly IEntityContainer _entityContainer;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly ISoundService _soundService;

        public GameFactory(IStaticData staticData, IEntityContainer entityContainer, ICoroutineRunner coroutineRunner, ISoundService soundService)
        {
            _staticData = staticData;
            _entityContainer = entityContainer;
            _coroutineRunner = coroutineRunner;
            _soundService = soundService;
        }

        public void CreateBallSpawner()
        {
            BallSpawnerView view = Object.Instantiate(_staticData.Prefabs.BallSpawner);
            BallSpawner spawner = new BallSpawner(view, _staticData);
            spawner.CreateBallsPool(this);
            _entityContainer.RegisterEntity(spawner);
        }

        public BallView CreateBallView() => 
            Object.Instantiate(_staticData.Prefabs.BallView);

        public void CreateTrampoline()
        {
            TrampolineView view = Object.Instantiate(_staticData.Prefabs.TrampolineViewPrefab);
            TrampolineMover trampolineMover = new TrampolineMover(view, _staticData, _coroutineRunner, _soundService);
            _entityContainer.RegisterEntity(trampolineMover);
        }
    }
}