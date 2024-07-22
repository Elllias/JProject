using System;
using Mirror;
using UnityEngine;

namespace Core
{
    public class InputHandler : MonoBehaviour
    {
        public event Action CubeSpawnButtonPressed;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CubeSpawnButtonPressed?.Invoke();
            }
        }
    }
}