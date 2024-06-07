using UnityEngine;
using UnityEngine.Events;

namespace FruitCatcher
{
    public class InputManager : MonoBehaviour
    {
        [Header("User input key")] [SerializeField]
        private string inputKey = "Fire1";

        [Header("Left click action")] [SerializeField]
        private UnityEvent onLeftAction = null;

        [Header("Right click action")] [SerializeField]
        private UnityEvent onRightAction = null;

        private float _x = 0;

        private void RunDown() => _x = Input.mousePosition.x;
        
        private void RunUp() {
            if (_x < Input.mousePosition.x) onRightAction?.Invoke();
            if (_x > Input.mousePosition.x) onLeftAction?.Invoke();
        }
        
        private void Update()
        {
            if (Input.GetButtonDown(inputKey)) RunDown();
            if (Input.GetButtonUp(inputKey)) RunUp();
        }
    }
}