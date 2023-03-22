using UnityEngine;
using UnityEngine.UI;

namespace Trampoline.CodeBase.Core.UI.Settings
{
    public class SettingsView : MonoBehaviour
    {
        [SerializeField] private Slider _volumeSlider;

        private void Start() => Hide();

        public void Hide() => gameObject.SetActive(false);
    }
}