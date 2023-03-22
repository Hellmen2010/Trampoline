using Trampoline.CodeBase.Infrastructure.ServiceContainer;
using Trampoline.CodeBase.Services.EntityContainer;
using Trampoline.CodeBase.Services.Factories.GameFactory;
using Trampoline.CodeBase.Services.Factories.UIFactory;
using Trampoline.CodeBase.Services.PersistentProgress;
using Trampoline.CodeBase.Services.SaveLoad;
using Trampoline.CodeBase.Services.SceneLoader;
using Trampoline.CodeBase.Services.Sound;
using Trampoline.CodeBase.Services.StaticData;
using Trampoline.CodeBase.Services.StaticData.StaticDataProvider;

namespace Trampoline.CodeBase.Infrastructure.States
{
    public class BootstrapperState : IState
    {
        private const string Initial = "Initial";
        private readonly GameStateMachine.GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapperState(GameStateMachine.GameStateMachine stateMachine, SceneLoader sceneLoader, AllServices services, ISoundService soundService, ICoroutineRunner coroutineRunner)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _services = services;

            RegisterServices(soundService, coroutineRunner);
        }

        public void Enter() => _sceneLoader.Load(Initial, onLoaded: MoveToLoadProgress);

        private void MoveToLoadProgress() => 
            _stateMachine.Enter<LoadProgressState>();

        public void Exit()
        {
            
        }

        private void RegisterServices(ISoundService soundService, ICoroutineRunner coroutineRunner)
        {
            _services.RegisterSingle<IStaticDataProvider>(new StaticDataProvider());
            _services.RegisterSingle<IStaticData>(new StaticData(_services.Single<IStaticDataProvider>()));
            _services.RegisterSingle<IPersistentProgress>(new PersistentProgress(_services.Single<IStaticData>()));
            _services.RegisterSingle<ISoundService>(soundService);
            _services.RegisterSingle<ISaveLoad>(new SaveLoad(_services.Single<IPersistentProgress>()));
            _services.RegisterSingle<IEntityContainer>(new EntityContainer());
            _services.RegisterSingle<IUIFactory>(new UIFactory(_services.Single<IStaticData>(), _services.Single<IEntityContainer>()));
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IStaticData>(), _services.Single<IEntityContainer>(), coroutineRunner, soundService));
        }
    }
}
