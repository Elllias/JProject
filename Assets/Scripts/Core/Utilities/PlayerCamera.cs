using Mirror;
using UnityEngine;

namespace Core.Utilities
{
    public class PlayerCamera : NetworkBehaviour
    {
        private Camera _mainCam;

        public override void OnStartLocalPlayer()
        {
            if (!_mainCam) return;
            
            Cursor.lockState = CursorLockMode.Locked;
                
            var camTransform = _mainCam.transform;
                
            camTransform.SetParent(transform);
            camTransform.localPosition = new Vector3(0f, 0, 0);
            camTransform.localEulerAngles = new Vector3(0, 0f, 0f);
        }

        public override void OnStopLocalPlayer()
        {
            if (!_mainCam) return;
            
            Cursor.lockState = CursorLockMode.None;
                
            var camTransform = _mainCam.transform;
                
            camTransform.SetParent(null);
            camTransform.localPosition = new Vector3(0f, 10f, 0f);
            camTransform.localEulerAngles = new Vector3(90f, 0f, 0f);
        }
        
        private void Awake()
        {
            _mainCam = Camera.main;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
