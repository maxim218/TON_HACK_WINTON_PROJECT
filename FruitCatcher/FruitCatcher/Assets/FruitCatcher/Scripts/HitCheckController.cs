namespace FruitCatcher
{
    public static class HitCheckController
    {
        private const float Top = -3.1f;
        private const float Bottom = -4.9f;

        public static bool IsHit(HeroController heroController, float elementPosY, TypePosition elementTypePosition)
        {
            if (elementTypePosition != heroController.GetHeroPositionType())
                return false;

            if (elementPosY > Top)
                return false;

            if (elementPosY < Bottom)
                return false;

            return true;
        }
    }
}