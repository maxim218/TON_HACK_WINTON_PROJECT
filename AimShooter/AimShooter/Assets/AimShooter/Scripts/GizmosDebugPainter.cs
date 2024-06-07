using UnityEngine;

namespace AimShooter
{
    public class GizmosDebugPainter : MonoBehaviour
    {
        private void OnDrawGizmos()
        {
            const float size = 0.2f;
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, size);
        }
    }
}