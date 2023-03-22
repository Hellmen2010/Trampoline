using Trampoline.CodeBase.Core.Spawner;
using Trampoline.CodeBase.Core.UI;
using Trampoline.CodeBase.Core.UI.HeathBar;
using Trampoline.CodeBase.Core.UI.Movement;
using Trampoline.CodeBase.Core.UI.Statistics;
using Trampoline.CodeBase.Data.Enums;
using Trampoline.CodeBase.Infrastructure.States.GameStateMachine;
using Trampoline.CodeBase.Services.EntityContainer;
using Trampoline.CodeBase.Services.PersistentProgress;
using Trampoline.CodeBase.Services.SaveLoad;

namespace Trampoline.CodeBase.Infrastructure.States
{
    public class GameLoopState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IEntityContainer _entityContainer;
        private readonly IPersistentProgress _progress;
        private readonly ISaveLoad _saveLoad;
        private BallSpawner _ballSpawner;
        private BackButton _backButton;
        private LastElements _lastElements;
        private int _ballsDelivered;
        private HealthBar _healthBar;
        private Movement _movement;

        public GameLoopState(IGameStateMachine stateMachine, IEntityContainer entityContainer, IPersistentProgress progress, ISaveLoad saveLoad)
        {
            _stateMachine = stateMachine;
            _entityContainer = entityContainer;
            _progress = progress;
            _saveLoad = saveLoad;
        }
        public void Enter()
        {
            CacheEntities();
            Subscribe();
            _backButton.Show();
            _ballsDelivered = 0;
            _ballSpawner.StartSpawnBalls();
        }

        public void Exit()
        {
            _movement.Disable();
            _movement.StopSideMove();
            UnSubscribe();
        }

        private void CacheEntities()
        {
            _movement = _entityContainer.GetEntity<Movement>();
            _ballSpawner = _entityContainer.GetEntity<BallSpawner>();
            _backButton = _entityContainer.GetEntity<BackButton>();
            _lastElements = _entityContainer.GetEntity<LastElements>();
            _healthBar = _entityContainer.GetEntity<HealthBar>();
        }

        private void Subscribe()
        {
            _movement.Subscribe();
            _ballSpawner.OnBallLose += DecreaseHealth;
            _ballSpawner.OnBallDelivered += _lastElements.AddTopPanelElement;
            _ballSpawner.OnBallDelivered += IncreaseScore;
            _ballSpawner.OnBallDelivered += ElementDelivered;
            _ballSpawner.OnBallRelease += TryEndRound;
            _backButton.OnBackButton += MoveToMainMenu;
        }

        private void TryEndRound(int activeBalls)
        {
            if (_healthBar.CurrentHealth <= 0 && activeBalls <= 1) MoveToResult();
        }

        private void DecreaseHealth()
        {
            int currentHealth = _healthBar.DecreaseHealth();
            if (currentHealth <= 0) StopBallsSpawn();
        }

        private void StopBallsSpawn() => _ballSpawner.StopSpawnRoutine();

        private void IncreaseScore(ElementType obj) => _ballsDelivered++;

        private void ElementDelivered(ElementType type)
        {
            _progress.AddStatisticElement(type);
            _saveLoad.SaveProgress();
        }

        private void UnSubscribe()
        {
            _movement.UnSubscribe();
            _ballSpawner.UnSubscribe();
            _backButton.OnBackButton -= MoveToMainMenu;
        }

        private void MoveToMainMenu() => _stateMachine.Enter<MainMenuState>();
        private void MoveToResult() => _stateMachine.Enter<ResultState, int>(_ballsDelivered);
    }
}