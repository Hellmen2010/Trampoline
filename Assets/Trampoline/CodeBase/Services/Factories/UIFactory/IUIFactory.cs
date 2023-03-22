using Trampoline.CodeBase.Core.Element;
using Trampoline.CodeBase.Core.UI;
using Trampoline.CodeBase.Core.UI.Settings;
using Trampoline.CodeBase.Core.UI.Statistics;
using Trampoline.CodeBase.Infrastructure.ServiceContainer;
using UnityEngine;

namespace Trampoline.CodeBase.Services.Factories.UIFactory
{
    public interface IUIFactory : IService
    {
        Transform CreateRootCanvas();
        void CreateMainMenu(Transform root);
        SettingsView CreateSettingsView(Transform root);
        void CreateSettingsButton(Transform root, SettingsView view);
        ElementUIView CreateTopPanelElement(Element element, Transform elementsRoot);
        void CreateLastElementsPanel(Transform root);
        void CreateHealthBar(Transform root);
        void CreateStatisticsView(Transform persistantRoot);
        StatisticElementView CreateStatisticsElementView(RectTransform viewField);
        void CreateBackButton(Transform persistantRoot);
        void CreateControllView(Transform root);
        void CreateWinPopUp(Transform root);
        void CreateTimer(Transform root);
    }
}