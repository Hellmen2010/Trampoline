using System;
using UnityEngine;
using UnityEngine.UI;

namespace Trampoline.CodeBase.Core.UI
{
    public class BackButton : MonoBehaviour
    {
        public event Action OnBackButton;
        
        [SerializeField] private Button _backButton;

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);

        public void Disable() => _backButton.interactable = false;
        public void Enable() => _backButton.interactable = true;
        
        private void Start() => _backButton.onClick.AddListener(BackButtonPressed);

        private void BackButtonPressed() => OnBackButton?.Invoke();

        private void OnDestroy() => _backButton.onClick.RemoveListener(BackButtonPressed);
    }
}