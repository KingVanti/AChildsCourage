using System;
using UnityEngine;

namespace AChildsCourage
{

    public static class UtilityExtensions
    {

        public static T Log<T>(this T item)
        {
            Debug.Log(item);
            return item;
        }

        public static T Log<T>(this T item, Func<T, string> formatter)
        {
            Debug.Log(formatter(item));
            return item;
        }
        
        public static T Log<T>(this T item, string text)
        {
            Debug.Log(text);
            return item;
        }

        public static Color WithAlpha(this Color c, float a) =>
            new Color(c.r, c.g, c.b, a);

    }

}