using UnityEngine;
using UnityEngine.UI;

namespace Trampoline.CodeBase.Core.UI.HeathBar
{
    public class HealthBarView : MonoBehaviour
    {
        private Image[] _health;
        public Transform HealthRoot;

        public void SetHealthSprite(Sprite sprite, int number) => _health[number].sprite = sprite;

        public void SetHealthsViews(Image[] views) => _health = views;
    }
}