using UnityEngine;

namespace AimShooter
{
    public class DebuggingDistanceCalculator : MonoBehaviour
    {
        [Header("Target Object")] [SerializeField]
        private GameObject targetObject = null;

        [ContextMenu("Calculate and render Distance")]
        public void CalculateAndRenderDistance()
        {
            if (targetObject)
            {
                float distance = Vector3.Distance(targetObject.transform.position, transform.position);
                string message = "Distance: " + distance;
                Debug.Log(message);
            }
        }
    }
}