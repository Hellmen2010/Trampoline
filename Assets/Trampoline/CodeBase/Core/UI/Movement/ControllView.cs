using UnityEngine;

namespace Trampoline.CodeBase.Core.UI.Movement
{
    public class ControllView : MonoBehaviour
    {
        public MoveButton LeftButton;
        public MoveButton RightButton;
        
        public void DisableControl()
        {
            LeftButton.interactable = false;
            RightButton.interactable = false;
        }
        
        public void EnableControl()
        {
            LeftButton.interactable = true;
            RightButton.interactable = true;
        }

        public void DisableLeft() => LeftButton.interactable = false;

        public void DisableRight() => RightButton.interactable = false;
    }
}