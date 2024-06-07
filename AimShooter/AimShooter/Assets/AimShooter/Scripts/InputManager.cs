using UnityEngine;
using UnityEngine.Events;

namespace AimShooter
{
    public class InputManager : MonoBehaviour
    {
        [Header("User input key")] [SerializeField]
        private string inputKey = "Fire1";

        [Header("On User Click")] [SerializeField]
        private UnityEvent onUserClick = null;

        private void Update()
        {
            if (Input.GetButtonDown(inputKey))
            {
                onUserClick?.Invoke();
            }
        }
    }
}