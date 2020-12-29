using System;
using System.Collections;
using System.Collections.Generic;
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
        
        public static Coroutine RestartCoroutine(this MonoBehaviour monoBehaviour, Coroutine coroutine, Func<IEnumerator> routineFunction)
        {
            if (coroutine != null) monoBehaviour.StopCoroutine(coroutine);
            return monoBehaviour.StartCoroutine(routineFunction());
        }

    }

}