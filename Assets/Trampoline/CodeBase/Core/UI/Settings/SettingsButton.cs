using System;
using UnityEngine;
using UnityEngine.UI;

namespace Trampoline.CodeBase.Core.UI.Settings
{
    public class SettingsButton : MonoBehaviour
    {
        [SerializeField] private Button _settingsButton;
        private SettingsView _view;

        public void Construct(SettingsView view) => 
            _view = view;

        private void Start() => _settingsButton.onClick.AddListener(SwitchView);

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

        private void SwitchView() => _view.gameObject.SetActive(!_view.gameObject.activeInHierarchy);

        private void OnDisable() => _view.Hide();

        private void OnDestroy() => _settingsButton.onClick.RemoveListener(SwitchView);
    }
}