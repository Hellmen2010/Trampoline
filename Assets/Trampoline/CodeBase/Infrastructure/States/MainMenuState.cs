using Trampoline.CodeBase.Core.UI;
using Trampoline.CodeBase.Core.UI.Settings;
using Trampoline.CodeBase.Core.UI.Statistics;
using Trampoline.CodeBase.Infrastructure.States.GameStateMachine;
using Trampoline.CodeBase.Services.EntityContainer;
using Trampoline.CodeBase.Services.Factories.UIFactory;
using Trampoline.CodeBase.Services.PersistentProgress;
using Trampoline.CodeBase.Services.SceneLoader;
using UnityEngine;

namespace Trampoline.CodeBase.Infrastructure.States
{
    public class MainMenuState : IState
    {
        private const string MainMenuScene = "MainMenu";
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameStateMachine _stateMachine;
        private readonly IUIFactory _uiFactory;
        private readonly IEntityContainer _entityContainer;
        private readonly IPersistentProgress _progress;
        private MainMenuView _mainMenuView;
        private Statistics _statistics;
        private BackButton _backButton;

        public MainMenuState(ISceneLoader sceneLoader, IGameStateMachine stateMachine, IUIFactory uiFactory, IEntityContainer entityContainer, IPersistentProgress progress)
        {
            _sceneLoader = sceneLoader;
            _stateMachine = stateMachine;
            _uiFactory = uiFactory;
            _entityContainer = entityContainer;
            _progress = progress;
        }

        public void Enter()
        {
            _sceneLoader.Load(MainMenuScene, OnSceneLoaded);
        }

        private void OnSceneLoaded()
        {
            Transform root = _uiFactory.CreateRootCanvas();
            _uiFactory.CreateMainMenu(root);
            CacheEntities();
            Subscribe();
            _entityContainer.GetEntity<SettingsButton>().Hide();
            _statistics.FillPanel(_progress.Progress.StatisticsElements);
            _statistics.HideView();
        }

        private void CacheEntities()
        {
            _mainMenuView = _entityContainer.GetEntity<MainMenuView>();
            _statistics = _entityContainer.GetEntity<Statistics>();
            _backButton = _entityContainer.GetEntity<BackButton>();
        }

        private void Subscribe()
        {
            _mainMenuView.OnPlayButton += MoveToCreateGameState;
            _mainMenuView.OnStatisticButton += _statistics.ShowView;
            _backButton.OnBackButton += _statistics.HideView;
            _statistics.SubscribeBackButton();
        }

        public void Exit()
        {
            _mainMenuView.OnPlayButton -= MoveToCreateGameState;
            _mainMenuView.OnStatisticButton -= _statistics.ShowView;
            _backButton.OnBackButton -= _statistics.HideView;
            _statistics.UnSubscribeBackButton();
        }

        private void MoveToCreateGameState() => _stateMachine.Enter<CreateGameState>();
    }
}