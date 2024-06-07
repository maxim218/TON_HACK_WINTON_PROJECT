using UnityEngine;

namespace FruitCatcher
{
    public class BonusController : MonoBehaviour
    {
        private const float notActiveCoordinate = -999;

        private readonly Vector3 notActivePosition =
            new Vector3(notActiveCoordinate, notActiveCoordinate, notActiveCoordinate);

        private TypePosition _typePosition = TypePosition.Center;

        public void SetTypePosition(TypePosition value) => _typePosition = value;

        public TypePosition GetTypePosition() => _typePosition;

        public float GetPosY() => transform.position.y;

        public void OnHitWithHero()
        {
            gameObject.SetActive(false);
            transform.position = notActivePosition;
        }
    }
}