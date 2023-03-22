using UnityEngine;

namespace Trampoline.CodeBase.Core.GameConfiguartion
{
    public class CameraSetup : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        private void Start()
        {
            Setup();
        }

        private void Setup()
        {
            if (Screen.height < 1500) return;
            _camera.transform.position = new Vector3(0,2,-10);
            _camera.orthographicSize = 8f;
        }
    }
}