using TMPro;
using UnityEngine;

namespace Trampoline.CodeBase.Core.UI.Timer
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        public void SetText(int value) => _text.text = value.ToString();

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}