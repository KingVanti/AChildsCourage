using System;
using System.Collections;
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

        public static Coroutine DoAfter(this MonoBehaviour monoBehaviour, Action action, float time)
        {
            IEnumerator WaitAndDo()
            {
                yield return new WaitForSeconds(time);
                action();
            }

            return monoBehaviour.StartCoroutine(WaitAndDo());
        }

        public static Coroutine StartOnly(this MonoBehaviour monoBehaviour, Func<IEnumerator> routineFunction)
        {
            monoBehaviour.StopAllCoroutines();
            return monoBehaviour.StartCoroutine(routineFunction());
        }

    }

}