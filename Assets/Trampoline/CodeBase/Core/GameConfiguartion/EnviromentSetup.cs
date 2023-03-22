using UnityEngine;

namespace Trampoline.CodeBase.Core.GameConfiguartion
{
    public class EnviromentSetup : MonoBehaviour
    {
        [SerializeField] private GameObject _ipad;
        [SerializeField] private GameObject _iphone;

        private void Start()
        {
            Setup();
        }

        private void Setup()
        {
            if (Screen.height >= 1500)
            {
                _ipad.SetActive(true);
                _iphone.SetActive(false);
            }
            else
            {
                _ipad.SetActive(false);
                _iphone.SetActive(true);
            }
        }
    }
}