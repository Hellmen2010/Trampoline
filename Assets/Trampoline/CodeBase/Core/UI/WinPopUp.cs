using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Trampoline.CodeBase.Core.UI
{
    public class WinPopUp : MonoBehaviour
    {
        public event Action OnMenuButton;
        
        public event Action OnCloseButton;
        
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _menuButton;
        [SerializeField] private TMP_Text _winText;

        private void Start()
        {
            _menuButton.onClick.AddListener(MenuButtonClicked);
            _restartButton.onClick.AddListener(RestartButtonClicked);
        }

        public void SetWinText(int balls) => _winText.text = $"You scored {balls} balls";

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);

        private void MenuButtonClicked()
        {
            OnMenuButton?.Invoke();
            Hide();
        }

        private void RestartButtonClicked()
        {
            OnCloseButton?.Invoke();
            Hide();
        }

        private void OnDestroy()
        {
            _menuButton.onClick.RemoveListener(MenuButtonClicked);
            _restartButton.onClick.RemoveListener(RestartButtonClicked);
        }
    }
}