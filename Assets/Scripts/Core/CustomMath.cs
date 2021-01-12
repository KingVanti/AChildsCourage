using UnityEngine;
using static UnityEngine.Mathf;

namespace AChildsCourage
{

    internal static class CustomMath
    {

        internal static float Map(float f, float sA, float sB, float tA, float tB) =>
            Lerp(tA, tB, InverseLerp(sA, sB, f));

        internal static float Remap(this float f, float sA, float sB, float tA, float tB) =>
            Map(f, sA, sB, tA, tB);

        internal static float Remap(this int i, int sA, int sB, float tA, float tB) =>
            Map(i, sA, sB, tA, tB);

        internal static float RemapSquared(this float f, float sA, float sB, float tA, float tB) =>
            Lerp(tA, tB, InverseLerp(sA, sB, f).Squared());

        internal static float CalculateAngle(Vector2 vector) =>
            CalculateAngle(vector.x, vector.y);

        internal static float CalculateAngle(float xPos, float yPos) =>
            Atan2(yPos, xPos) * Rad2Deg;

        internal static float Clamp(this float f, float min, float max) =>
            f <= min ? min
            : f >= max ? max
            : f;

        internal static int Clamp(this int i, int min, int max) =>
            i <= min ? min
            : i >= max ? max
            : i;

        internal static float Inverse(this float f) =>
            1f / f;

        internal static float Raise(this float f, float pow) =>
            Pow(f, pow);

        internal static float Squared(this float f) =>
            Pow(f, 2);

        internal static int Minus(this int i, int sub) =>
            i - sub;

        internal static float Plus(this float f, float add) =>
            f + add;

        internal static int Times(this int i, int mult) =>
            i * mult;

        internal static float Times(this int i, float mult) =>
            i * mult;

        internal static float CalculateCircleArea(float radius) =>
            PI * radius.Squared();

        internal static float Mod(float mod, float f) =>
            f % mod;

        internal static float NormalizeAngle(float angle) =>
            Repeat(angle, 360);

    }

}