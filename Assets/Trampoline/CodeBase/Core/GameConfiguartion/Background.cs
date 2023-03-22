using System;
using UnityEngine;
using UnityEngine.UI;

namespace Trampoline.CodeBase.Core.GameConfiguartion
{
    public class Background : MonoBehaviour
    {
        [SerializeField] private Image _background;
        [SerializeField] private Sprite _iphone;
        [SerializeField] private Sprite _ipad;

        private void Start()
        {
            SetImage();
        }

        private void SetImage()
        {
            _background.sprite = Screen.height >= 1500
                ? _ipad
                : _iphone;
        }
    }
}