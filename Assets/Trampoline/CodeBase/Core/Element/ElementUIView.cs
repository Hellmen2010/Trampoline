using Trampoline.CodeBase.Data.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Trampoline.CodeBase.Core.Element
{
    public class ElementUIView : MonoBehaviour
    {
        public Image Image;
        public ElementType Type { get; private set; }

        public void SetElement(Element element)
        {
            Image.sprite = element.Sprite;
            Type = element.Type;
        }
        
        public void SetElement(ElementUIView element)
        {
            Image.sprite = element.Image.sprite;
            Type = element.Type;
        }
    }
}