using System.Collections;
using Trampoline.CodeBase.Data.StaticData;
using Trampoline.CodeBase.Infrastructure;
using Trampoline.CodeBase.Services.Sound;
using Trampoline.CodeBase.Services.StaticData;
using UnityEngine;

namespace Trampoline.CodeBase.Core.Trampoline
{
    public class TrampolineMover
    {
        public TrampolineView View { get; private set; }
        public Coroutine MoveRoutine{ get; private set; }

        private readonly GameConfig _config;
        private readonly ISoundService _soundService;
        private ICoroutineRunner _coroutineRunner;

        public TrampolineMover(TrampolineView view, IStaticData staticData, ICoroutineRunner coroutineRunner, ISoundService soundService)
        {
            _coroutineRunner = coroutineRunner;
            View = view;
            _config = staticData.GameConfig;
            _soundService = soundService;
        }

        public void StopSideMovement()
        {
            if(MoveRoutine == null) return;
            _coroutineRunner.StopCoroutine(MoveRoutine);
            //_soundService.DisableLoopEffect();
        }

        public void StartMoveLeft() => MoveRoutine = _coroutineRunner.StartCoroutine(MoveLeftRoutine());

        public void StartMoveRight() => MoveRoutine = _coroutineRunner.StartCoroutine(MoveRightRoutine());

        private IEnumerator MoveLeftRoutine()
        {
            //_soundService.EnableLoopEffect(SoundId.ClawCraneLeftRight);
            while (View.transform.position.x >= _config.LeftBoundary)
            {
                yield return new WaitForFixedUpdate();
                View.transform.position -= GetNextPosition(View.transform.position);
            }
        }
        
        private IEnumerator MoveRightRoutine()
        {
            //_soundService.EnableLoopEffect(SoundId.ClawCraneLeftRight);
            while (View.transform.position.x <= _config.RightBoundary)
            {
                yield return new WaitForFixedUpdate();
                View.transform.position += GetNextPosition(View.transform.position);
            }
        }

        private Vector3 GetNextPosition(Vector3 pos) => 
            new (_config.TrampolineMoveSpeed * Time.deltaTime, 0, 0);

    }
}