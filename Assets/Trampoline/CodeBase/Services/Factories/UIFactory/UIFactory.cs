using Trampoline.CodeBase.Core.Element;
using Trampoline.CodeBase.Core.UI;
using Trampoline.CodeBase.Core.UI.HeathBar;
using Trampoline.CodeBase.Core.UI.Movement;
using Trampoline.CodeBase.Core.UI.Settings;
using Trampoline.CodeBase.Core.UI.Statistics;
using Trampoline.CodeBase.Core.UI.Timer;
using Trampoline.CodeBase.Services.EntityContainer;
using Trampoline.CodeBase.Services.StaticData;
using UnityEngine;
using UnityEngine.UI;

namespace Trampoline.CodeBase.Services.Factories.UIFactory
{
    public class UIFactory : IUIFactory
    {
        private readonly IStaticData _staticData;
        private readonly IEntityContainer _entityContainer;

        public UIFactory(IStaticData staticData, IEntityContainer entityContainer)
        {
            _staticData = staticData;
            _entityContainer = entityContainer;
        }
        
        public Transform CreateRootCanvas() => 
            Object.Instantiate(_staticData.Prefabs.RootCanvas);

        public void CreateMainMenu(Transform root)
        {
            MainMenuView view = Screen.height >= 1500 
                ? Object.Instantiate(_staticData.Prefabs.MainMenuIpadPrefab, root)
                : Object.Instantiate(_staticData.Prefabs.MainMenuIphonePrefab, root);;
            _entityContainer.RegisterEntity(view);
        }

        public SettingsView CreateSettingsView(Transform root) => 
            Object.Instantiate(_staticData.Prefabs.SettingsViewPrefab, root);

        public void CreateSettingsButton(Transform root, SettingsView view)
        {
            SettingsButton settingsButton = Object.Instantiate(_staticData.Prefabs.SettingsButtonPrefab, root);
            settingsButton.Construct(view);
            _entityContainer.RegisterEntity(settingsButton);
        }

        public void CreateLastElementsPanel(Transform root)
        {
            LastElementsTopPanelView lastElementsView = Object.Instantiate(_staticData.Prefabs.LastElementsPrefab, root);
            int topPanelSize = Screen.height >= 1500
                ? _staticData.GameConfig.TopPanelSizeIpad
                : _staticData.GameConfig.TopPanelSizeIphone;
            LastElements lastElements = new LastElements(lastElementsView, _staticData.ElementsByType, this, topPanelSize);
            _entityContainer.RegisterEntity(lastElements);
        }

        public void CreateHealthBar(Transform root)
        {
            HealthBarView view = Object.Instantiate(_staticData.Prefabs.HealthBarPrefab, root);
            HealthBar healthBar = new HealthBar(view, _staticData);
            view.SetHealthsViews(CreateHealth(view.HealthRoot, _staticData.GameConfig.MaxHealth));
            _entityContainer.RegisterEntity(healthBar);
        }

        private Image[] CreateHealth(Transform healthRoot, int maxHealth)
        {
            Image[] healths = new Image[maxHealth];
            for (int i = 0; i < maxHealth; i++)
                healths[i] = Object.Instantiate(_staticData.Prefabs.HealthPrefab, healthRoot);
            return healths;
        }

        public void CreateStatisticsView(Transform root)
        {
            StatisticsPanelView panelView = Object.Instantiate(_staticData.Prefabs.statisticsPanelViewPrefab, root);
            _entityContainer.RegisterEntity(panelView);
        }

        public StatisticElementView CreateStatisticsElementView(RectTransform viewField) => 
            Object.Instantiate(_staticData.Prefabs.StatisticElementViewPrefab, viewField);

        public void CreateBackButton(Transform root)
        {
            BackButton backButton = Object.Instantiate(_staticData.Prefabs.BackButtonPrefab, root);
            _entityContainer.RegisterEntity(backButton);
        }

        public void CreateControllView(Transform root)
        {
            ControllView controllView = Object.Instantiate(_staticData.Prefabs.ControllViewPrefab, root);
            _entityContainer.RegisterEntity(controllView);
        }

        public void CreateWinPopUp(Transform root)
        {
            WinPopUp winPopUp = Object.Instantiate(_staticData.Prefabs.WinPopUpPrefab, root);
            winPopUp.Hide();
            _entityContainer.RegisterEntity(winPopUp);
        }

        public void CreateTimer(Transform root)
        {
            TimerView timer = Object.Instantiate(_staticData.Prefabs.TimerPrefab, root);
            timer.Hide();
            _entityContainer.RegisterEntity(timer);
        }

        public ElementUIView CreateTopPanelElement(Element element, Transform elementsRoot)
        {
            ElementUIView elementUI = Object.Instantiate(_staticData.Prefabs.ElementUIViewPrefab, elementsRoot);
            elementUI.SetElement(element);
            return elementUI;
        }
    }
}