using UnityEngine;

namespace AimShooter
{
    public class MovingCameraManager : MonoBehaviour
    {
        [Header("Target point")] [SerializeField]
        private GameObject targetPoint = null;

        private Camera _mainCamera = null;
        private bool _allowMoving = false;
        private Vector3 _hitPosition = Vector3.zero;

        public void SetCamera(Camera cameraObject) => _mainCamera = cameraObject;
        public void SetAllowMoving(bool allowMoving) => _allowMoving = allowMoving;
        public Vector3 GetHitPosition() => _hitPosition;
        private void CameraLookToTarget() => _mainCamera.transform.LookAt(targetPoint.transform.position);

        private static Vector3 GetMiddlePosition(Camera cameraObj)
        {
            float middleW = cameraObj.pixelWidth / 2.0f;
            float middleH = cameraObj.pixelHeight / 2.0f;
            return new Vector3(middleW, middleH, 0);
        }

        private void CalculateCameraViewHitPosition()
        {
            Vector3 cameraCenterPosition = GetMiddlePosition(_mainCamera);
            Ray ray = _mainCamera.ScreenPointToRay(cameraCenterPosition);
            if (Physics.Raycast(ray, out var hit)) _hitPosition = hit.point;
        }

        private void Update()
        {
            if (!_mainCamera) return;
            if (!_allowMoving) return;
            if (!targetPoint) return;

            CameraLookToTarget();
            CalculateCameraViewHitPosition();
        }

        private void OnDrawGizmos()
        {
            if (!_mainCamera) return;
            if (!_allowMoving) return;
            if (!targetPoint) return;

            const float size = 0.2f;
            Gizmos.color = Color.green;
            Gizmos.DrawLine(_mainCamera.transform.position, _hitPosition);
            Gizmos.DrawSphere(_hitPosition, size);
        }
    }
}