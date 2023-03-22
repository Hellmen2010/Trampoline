using System;
using UnityEngine;

namespace Trampoline.CodeBase.Core.UI.Statistics
{
    public class StatisticsPanelView : MonoBehaviour
    {
        public event Action OnShow;
        public event Action OnHide;
        
        public RectTransform Field;
        public RectTransform BackgroundRect;

        public void Show()
        {
            OnShow?.Invoke();
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            OnHide?.Invoke();
            gameObject.SetActive(false);
        }
    }
}