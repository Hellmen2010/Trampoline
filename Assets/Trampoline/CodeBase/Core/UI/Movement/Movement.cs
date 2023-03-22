using Trampoline.CodeBase.Core.Trampoline;
using Trampoline.CodeBase.Services.EntityContainer;

namespace Trampoline.CodeBase.Core.UI.Movement
{
    public class Movement
    {
        private readonly IEntityContainer _entityContainer;
        private ControllView _controllView;
        private TrampolineMover _trampolineMover;
        private BackButton _backButton;

        public Movement(IEntityContainer entityContainer)
        {
            _entityContainer = entityContainer;
            CacheEntities();
        }
        
        public void Subscribe()
        {
            _controllView.LeftButton.OnPressed += StartMoveLeft;
            _controllView.LeftButton.OnReleased += StopSideMove;
            _controllView.RightButton.OnPressed += StartMoveRight;
            _controllView.RightButton.OnReleased += StopSideMove;
        }
        
        public void UnSubscribe()
        {
            _controllView.LeftButton.OnPressed -= StartMoveLeft;
            _controllView.LeftButton.OnReleased -= StopSideMove;
            _controllView.RightButton.OnPressed -= StartMoveRight;
            _controllView.RightButton.OnReleased -= StopSideMove;
        }

        public void Enable() => _controllView.EnableControl();
        public void Disable() => _controllView.DisableControl();
        
        private void StartMoveLeft()
        {
            _controllView.DisableRight();
            _trampolineMover.StartMoveLeft();
            _backButton.Disable();
        }
        
        private void StartMoveRight()
        {
            _controllView.DisableLeft();
            _trampolineMover.StartMoveRight();
            _backButton.Disable();
        }
        
        public void StopSideMove()
        {
            _controllView.EnableControl();
            _trampolineMover.StopSideMovement();
            _backButton.Enable();
        }

        private void CacheEntities()
        {
            _controllView = _entityContainer.GetEntity<ControllView>();
            _trampolineMover = _entityContainer.GetEntity<TrampolineMover>();
            _backButton = _entityContainer.GetEntity<BackButton>();
        }
    }
}