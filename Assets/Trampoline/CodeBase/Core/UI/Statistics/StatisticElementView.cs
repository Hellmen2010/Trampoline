using Trampoline.CodeBase.Data.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Trampoline.CodeBase.Core.UI.Statistics
{
    public class StatisticElementView : MonoBehaviour
    {
        [SerializeField] private RectTransform _rect;
        [SerializeField] private Image _image;
        public ElementType Type { get; private set; }

        public void SetElement(Element.Element element)
        {
            _image.sprite = element.Sprite;
            Type = element.Type;
        }
        
        public void SetupRect(float sideSize, Vector2 pos)
        {
            _rect.sizeDelta = new Vector2(sideSize, sideSize);
            _rect.anchoredPosition = pos;
        }

        public void Show() => gameObject.SetActive(true);

        public void Hide() => gameObject.SetActive(false);
    }
}