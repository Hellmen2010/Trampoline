using System;
using UnityEngine;
using UnityEngine.UI;

namespace Trampoline.CodeBase.Core.UI
{
    public class MainMenuView : MonoBehaviour
    {
        public event Action OnPlayButton;
        public event Action OnStatisticButton;
        
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _statisticButton;

        private void Start()
        {
            _playButton.onClick.AddListener(PlayButtonClicked);
            _statisticButton.onClick.AddListener(StatisticButtonClicked);
        }

        private void StatisticButtonClicked() => OnStatisticButton?.Invoke();

        private void PlayButtonClicked() => OnPlayButton?.Invoke();

        private void OnDestroy()
        {
            _playButton.onClick.RemoveListener(PlayButtonClicked);
            _statisticButton.onClick.RemoveListener(StatisticButtonClicked);
        }
    }
}