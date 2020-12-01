using static System.Math;

namespace AChildsCourage
{

    public static class CustomMath
    {

        public static float Map(float f, float sA, float sB, float tA, float tB) => tA + (tB - tA) * ((f - sA) / (sB - sA));

        public static float CalculateAngle(float yPos, float xPos) => (float) (Atan2(yPos, xPos) * (180d / PI));

        public static float Clamp(this float f, float min, float max) => f <= min ? min : f >= max ? max : f;

        public static int Clamp(this int i, int min, int max) => i <= min ? min : i >= max ? max : i;

        public static float Inverse(this float f) => 1f / f;

    }

}