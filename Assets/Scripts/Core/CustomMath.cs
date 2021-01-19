using UnityEngine;
using static UnityEngine.Mathf;

namespace AChildsCourage
{

    internal static class CustomMath
    {

        internal static float Remap(float sA, float sB, float tA, float tB, float f) =>
            Lerp(tA, tB, InverseLerp(sA, sB, f));

        internal static float Remap(int sA, int sB, float tA, float tB, int i) =>
            ((float) i).Map(Remap, (float) sA, (float) sB, tA, tB);

        internal static float RemapSquared(float sA, float sB, float tA, float tB, float f) =>
            Lerp(tA, tB, InverseLerp(sA, sB, f).Map(Squared));

        internal static float CalculateAngle(Vector2 vector) =>
            CalculateAngle(vector.x, vector.y);

        internal static float CalculateAngle(float xPos, float yPos) =>
            Atan2(yPos, xPos) * Rad2Deg;

        internal static float Clamp(float min, float max, float f) =>
            f <= min ? min
            : f >= max ? max
            : f;

        internal static int Clamp(int min, int max, int i) =>
            i <= min ? min
            : i >= max ? max
            : i;

        internal static float Inverse(float f) =>
            1f / f;

        internal static float Raise(float pow, float f) =>
            Pow(f, pow);

        internal static float Squared(float f) =>
            Pow(f, 2);

        internal static int Minus(int sub, int i) =>
            i - sub;

        internal static float Plus(float add, float f) =>
            f + add;

        internal static int Times(int mult, int i) =>
            i * mult;

        internal static float Times(float mult, int i) =>
            i * mult;

        internal static float CalculateCircleArea(float radius) =>
            PI * radius.Map(Squared);

        internal static float Mod(float mod, float f) =>
            f % mod;

        internal static float NormalizeAngle(float angle) =>
            Repeat(angle, 360);

    }

}