using UnityEngine;

namespace AimShooter
{
    public static class ResultPointsCounter
    {
        public static int CalculatePoints(Vector3 centerPos, Vector3 bulletPos)
        {
            float distance = Vector3.Distance(centerPos, bulletPos);

            if (distance < 0.85f)
                return 10;
            if (distance < 1.6f)
                return 8;
            if (distance < 2.36f)
                return 6;
            if (distance < 3.09f)
                return 4;
            if (distance < 3.86f)
                return 2;

            return 0;
        }
    }
}