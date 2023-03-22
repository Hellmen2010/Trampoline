using Trampoline.CodeBase.Core.UI;
using Trampoline.CodeBase.Infrastructure.States.GameStateMachine;
using Trampoline.CodeBase.Services.EntityContainer;

namespace Trampoline.CodeBase.Infrastructure.States
{
    public class ResultState : IPayloadedState<int>
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IEntityContainer _entityContainer;
        private WinPopUp _winPopUp;

        public ResultState(IGameStateMachine stateMachine, IEntityContainer entityContainer)
        {
            _stateMachine = stateMachine;
            _entityContainer = entityContainer;
        }
        public void Enter(int balls)
        {
            CacheEntities();
            SetupWinPopUp(balls);
            Subscribe();
        }

        public void Exit()
        {
            UnSubscribe();
        }

        private void SetupWinPopUp(int balls)
        {
            _winPopUp.SetWinText(balls);
            _winPopUp.Show();
        }

        private void CacheEntities()
        {
            _winPopUp = _entityContainer.GetEntity<WinPopUp>();
        }

        private void Subscribe()
        {
            _winPopUp.OnCloseButton += MoveToPreparation;
            _winPopUp.OnMenuButton += MoveToMenu;
        }

        private void UnSubscribe()
        {
            _winPopUp.OnCloseButton -= MoveToPreparation;
            _winPopUp.OnMenuButton -= MoveToMenu;
        }
        
        private void MoveToMenu() => _stateMachine.Enter<MainMenuState>();
        private void MoveToPreparation() => _stateMachine.Enter<PreparationState>();
    }
}