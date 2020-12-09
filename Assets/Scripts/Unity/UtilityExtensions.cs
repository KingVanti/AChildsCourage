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
    }

}