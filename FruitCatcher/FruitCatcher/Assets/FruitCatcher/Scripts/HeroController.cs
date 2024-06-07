using UnityEngine;

namespace FruitCatcher
{
    public class HeroController
    {
        private readonly GameObject _hero = null;

        private readonly Vector3 _leftPos = new Vector3(-1.5f, -4, 5);
        private readonly Vector3 _centerPos = new Vector3(0, -4, 5);
        private readonly Vector3 _rightPos = new Vector3(1.5f, -4, 5);

        private int _positionIndex = 1;

        public HeroController(GameObject hero)
        {
            _hero = hero;
            _positionIndex = 1;
            SetHeroPosition();
        }

        private void SetHeroPosition()
        {
            _hero.transform.position = _positionIndex switch
            {
                0 => _leftPos,
                1 => _centerPos,
                2 => _rightPos,
                _ => _hero.transform.position
            };
        }

        public void MoveHero(int direction)
        {
            _positionIndex += direction;
            if (_positionIndex < 0) _positionIndex = 0;
            if (_positionIndex > 2) _positionIndex = 2;
            SetHeroPosition();
        }

        public TypePosition GetHeroPositionType()
        {
            return _positionIndex switch
            {
                0 => TypePosition.Left,
                1 => TypePosition.Center,
                2 => TypePosition.Right,
                _ => TypePosition.Center
            };
        }
    }
}