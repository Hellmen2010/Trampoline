using Trampoline.CodeBase.Core.UI;
using Trampoline.CodeBase.Core.UI.Settings;
using Trampoline.CodeBase.Core.UI.Statistics;
using Trampoline.CodeBase.Infrastructure.States.GameStateMachine;
using Trampoline.CodeBase.Services.EntityContainer;
using Trampoline.CodeBase.Services.Factories.UIFactory;
using Trampoline.CodeBase.Services.StaticData;
using UnityEngine;

namespace Trampoline.CodeBase.Infrastructure.States
{
    public class CreatePersistentEntitiesState : IState
    {
        private readonly IGameStateMachine _stateMachine;
        private readonly IUIFactory _uiFactory;
        private readonly IEntityContainer _entityContainer;
        private readonly IStaticData _staticData;

        public CreatePersistentEntitiesState(IGameStateMachine stateMachine, IUIFactory uiFactory, IEntityContainer entityContainer, IStaticData staticData)
        {
            _stateMachine = stateMachine;
            _uiFactory = uiFactory;
            _entityContainer = entityContainer;
            _staticData = staticData;
        }

        public void Enter()
        {
            CreatePersistentEntities();
            MoveToMainMenu();
        }

        public void Exit()
        {
        }

        private void CreatePersistentEntities()
        {
            Transform persistantRoot = CreatePersistentCanvas();
            CreateStatistics(persistantRoot);
            CreateSettings(persistantRoot);
            _uiFactory.CreateBackButton(persistantRoot);
        }

        private Transform CreatePersistentCanvas()
        {
            Transform root = _uiFactory.CreateRootCanvas();
            root.GetComponent<Canvas>().sortingOrder = 5;
            Object.DontDestroyOnLoad(root);
            return root;
        }

        private void CreateStatistics(Transform persistantRoot)
        {
            _uiFactory.CreateStatisticsView(persistantRoot);
            Statistics statistics = new Statistics(_entityContainer.GetEntity<StatisticsPanelView>(), _uiFactory, _entityContainer, _staticData);
            statistics.CreateStatistics();
            _entityContainer.RegisterEntity(statistics);
        }

        private void CreateSettings(Transform root)
        {
            SettingsView view = _uiFactory.CreateSettingsView(root);
            _uiFactory.CreateSettingsButton(root, view);
        }

        private void MoveToMainMenu() => _stateMachine.Enter<MainMenuState>();
    }
}