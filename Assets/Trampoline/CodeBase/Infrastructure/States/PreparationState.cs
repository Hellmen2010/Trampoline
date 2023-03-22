using System.Collections;
using Trampoline.CodeBase.Core.UI;
using Trampoline.CodeBase.Core.UI.HeathBar;
using Trampoline.CodeBase.Core.UI.Movement;
using Trampoline.CodeBase.Core.UI.Timer;
using Trampoline.CodeBase.Infrastructure.States.GameStateMachine;
using Trampoline.CodeBase.Services.EntityContainer;
using Trampoline.CodeBase.Services.StaticData;
using UnityEngine;

namespace Trampoline.CodeBase.Infrastructure.States
{
    public class PreparationState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IEntityContainer _entityContainer;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IStaticData _staticData;
        private HealthBar _healthBar;
        private TimerView _timer;
        private Movement _movement;

        public PreparationState(IGameStateMachine stateMachine, IEntityContainer entityContainer, ICoroutineRunner coroutineRunner, IStaticData staticData)
        {
            _stateMachine = stateMachine;
            _entityContainer = entityContainer;
            _coroutineRunner = coroutineRunner;
            _staticData = staticData;
        }
        public void Enter()
        {
            CacheEntities();
            ResetValues();
            MoveToGameLoop();
            Subscribe();
            _movement.Enable();
        }

        public void Exit()
        {
            UnSubscribe();
        }

        private void Subscribe() => _movement.Subscribe();
        private void UnSubscribe() => _movement.UnSubscribe();

        private void ResetValues() => _healthBar.ResetHealth();

        private void CacheEntities()
        {
            _healthBar = _entityContainer.GetEntity<HealthBar>();
            _timer = _entityContainer.GetEntity<TimerView>();
            _movement = _entityContainer.GetEntity<Movement>();
        }

        private void MoveToGameLoop() => _coroutineRunner.StartCoroutine(StartRoundRoutine());

        private IEnumerator StartRoundRoutine()
        {
            _timer.Show();
            int timer = _staticData.GameConfig.TimerBeforeStart;
            while (timer >= 0)
            {
                _timer.SetText(timer);
                yield return new WaitForSeconds(1f);
                timer--;
            }
            _timer.Hide();
            _stateMachine.Enter<GameLoopState>();
        }
    }
}