using UnityEngine;

namespace AChildsCourage
{

    internal static class UtilityExtensions
    {

        internal static T Log<T>(this T item, string text)
        {
            Debug.Log(text);
            return item;
        }

        internal static Color WithAlpha(this Color c, float a) =>
            new Color(c.r, c.g, c.b, a);

        internal static Vector3 WithZ(this Vector2 v, float z) =>
            new Vector3(v.x, v.y, z);

    }

}