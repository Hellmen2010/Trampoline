using Trampoline.CodeBase.Core.UI;
using Trampoline.CodeBase.Data.Progress;
using Trampoline.CodeBase.Infrastructure.States.GameStateMachine;
using Trampoline.CodeBase.Services.PersistentProgress;
using Trampoline.CodeBase.Services.SaveLoad;
using Trampoline.CodeBase.Services.Sound;
using Trampoline.CodeBase.Services.StaticData;
using UnityEngine;

namespace Trampoline.CodeBase.Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IPersistentProgress _progress;
        private readonly IStaticData _staticData;
        private readonly ISaveLoad _saveLoad;
        private readonly ISoundService _soundService;

        public LoadProgressState(IGameStateMachine stateMachine, IPersistentProgress progress, IStaticData staticData, ISaveLoad saveLoad, 
            ISoundService soundService)
        {
            _stateMachine = stateMachine;
            _progress = progress;
            _staticData = staticData;
            _saveLoad = saveLoad;
            _soundService = soundService;
        }
        public void Enter()
        {
            LoadProgressOrInitNew();
            SoundServiceIni();
            _stateMachine.Enter<CreatePersistentEntitiesState>();
        }

        private void SoundServiceIni()
        {
            _soundService.Construct(_staticData.SoundData, _progress.Progress.Settings);
            _soundService.EnableBackgroundMusic();
        }

        public void Exit()
        {
        }
        
        private void LoadProgressOrInitNew() => 
            _progress.Progress = _saveLoad.LoadProgress() ?? new PlayerProgress(_staticData);
    }
}