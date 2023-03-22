using Trampoline.CodeBase.Core.Element;
using Trampoline.CodeBase.Core.UI;
using Trampoline.CodeBase.Core.UI.Movement;
using Trampoline.CodeBase.Core.UI.Settings;
using Trampoline.CodeBase.Infrastructure.States.GameStateMachine;
using Trampoline.CodeBase.Services.EntityContainer;
using Trampoline.CodeBase.Services.Factories.GameFactory;
using Trampoline.CodeBase.Services.Factories.UIFactory;
using Trampoline.CodeBase.Services.SceneLoader;
using UnityEngine;
using UnityEngine.Pool;

namespace Trampoline.CodeBase.Infrastructure.States
{
    public class CreateGameState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IUIFactory _uiFactory;
        private readonly IGameFactory _gameFactory;
        private readonly IEntityContainer _entityContainer;
        private const string GameSceneName = "Game";

        public CreateGameState(IGameStateMachine stateMachine, ISceneLoader sceneLoader, IUIFactory uiFactory, IGameFactory gameFactory, IEntityContainer entityContainer)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _uiFactory = uiFactory;
            _gameFactory = gameFactory;
            _entityContainer = entityContainer;
        }

        public void Enter()
        {
            _sceneLoader.Load(GameSceneName, CreateGame);
            _entityContainer.GetEntity<SettingsButton>().Show();
        }

        public void Exit()
        {
        }

        private void CreateGame()
        {
            Transform root = _uiFactory.CreateRootCanvas();
            _uiFactory.CreateLastElementsPanel(root);
            _uiFactory.CreateHealthBar(root);
            _uiFactory.CreateControllView(root);
            _uiFactory.CreateWinPopUp(root);
            _uiFactory.CreateTimer(root);
            _gameFactory.CreateBallSpawner();
            _gameFactory.CreateTrampoline();
            CreateMovement();
            MoveToPreparation();
        }

        private void CreateMovement()
        {
            Movement movement = new Movement(_entityContainer);
            _entityContainer.RegisterEntity(movement);
        }

        private void MoveToPreparation() => _stateMachine.Enter<PreparationState>();
    }
}