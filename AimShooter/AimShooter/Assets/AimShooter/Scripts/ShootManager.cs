using UnityEngine;

namespace AimShooter
{
    public class ShootManager : MonoBehaviour
    {
        [Header("Center Of Target")] [SerializeField]
        private GameObject centerOfTarget = null;

        [Header("Bullet Arrow Prefab")] [SerializeField]
        private GameObject bulletPrefab = null;

        [Header("Can Fire Number")] [SerializeField]
        private int canFireNumber = 0;

        public int GetCanFireNumber() => canFireNumber;

        public bool IsFireAllowed() => (canFireNumber > 0);

        public void CreateBullet(Vector3 position, ScoreDataController playerScoreController)
        {
            if (playerScoreController != null)
            {
                bool isFireAllowedFlag = IsFireAllowed();
                if (isFireAllowedFlag)
                {
                    GameObject bullet = Instantiate(bulletPrefab);
                    bullet.transform.position = position;
                    canFireNumber -= 1;

                    int deltaScorePoints =
                        ResultPointsCounter.CalculatePoints(centerOfTarget.transform.position, position);
                    playerScoreController.AddScore(deltaScorePoints);
                    string message = "Player add score: " + deltaScorePoints;
                    Debug.Log(message);
                }
            }
        }
    }
}