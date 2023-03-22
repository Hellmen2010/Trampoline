using Trampoline.CodeBase.Core.Element;
using Trampoline.CodeBase.Core.Spawner;
using Trampoline.CodeBase.Core.Trampoline;
using Trampoline.CodeBase.Core.UI;
using Trampoline.CodeBase.Core.UI.HeathBar;
using Trampoline.CodeBase.Core.UI.Movement;
using Trampoline.CodeBase.Core.UI.Settings;
using Trampoline.CodeBase.Core.UI.Statistics;
using Trampoline.CodeBase.Core.UI.Timer;
using UnityEngine;
using UnityEngine.UI;

namespace Trampoline.CodeBase.Data.StaticData
{
    [CreateAssetMenu(fileName = "PrefabsData", menuName = "StaticData/PrefabsData")]
    public class PrefabsData : ScriptableObject
    {
        [Header("UI")]
        public Transform RootCanvas;
        public MainMenuView MainMenuIpadPrefab;
        public MainMenuView MainMenuIphonePrefab;
        public SettingsView SettingsViewPrefab;
        public SettingsButton SettingsButtonPrefab;
        public LastElementsTopPanelView LastElementsPrefab;
        public ElementUIView ElementUIViewPrefab;
        public HealthBarView HealthBarPrefab;
        public StatisticsPanelView statisticsPanelViewPrefab;
        public BackButton BackButtonPrefab;
        public ControllView ControllViewPrefab;
        public StatisticElementView StatisticElementViewPrefab;
        public WinPopUp WinPopUpPrefab;
        public Image HealthPrefab;
        public TimerView TimerPrefab;

        [Header("GameScene")]
        public BallSpawnerView BallSpawner;
        public BallView BallView;
        public TrampolineView TrampolineViewPrefab;
    }
}