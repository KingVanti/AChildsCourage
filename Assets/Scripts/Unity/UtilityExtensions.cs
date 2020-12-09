using System;
using UnityEngine;

namespace AChildsCourage
{

    public static class UtilityExtensions
    {

        public static Vector3 ToVector(this float angle) =>
            new Vector3(
                Mathf.Cos(angle * Mathf.Deg2Rad),
                Mathf.Sin(angle * Mathf.Deg2Rad));

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
    }

}