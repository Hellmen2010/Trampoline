using UnityEngine;

namespace Trampoline.CodeBase.Core.GameConfiguartion
{
    public class SetupUIForDevice : MonoBehaviour
    {
        [SerializeField] private RectTransform _object;
        [SerializeField] private RectTransform _iphone;
        [SerializeField] private RectTransform _ipad;

        private void Start()
        {
            Setup();
        }

        private void Setup() => 
            _object.position = Screen.height >= 1500 ? _ipad.position : _iphone.position;
    }
}