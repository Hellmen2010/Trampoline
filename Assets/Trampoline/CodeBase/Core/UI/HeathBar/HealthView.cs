using UnityEngine;
using UnityEngine.UI;

namespace Trampoline.CodeBase.Core.UI.HeathBar
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Image _image;

        public void ChangeSprite(Sprite sprite) => _image.sprite = sprite;
    }
}