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

        public static Color WithAlpha(this Color c, float a) =>
            new Color(c.r, c.g, c.b, a);
        
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

        public static Coroutine DoContinually(this MonoBehaviour monoBehaviour, Action action, float waitTime = 0)
        {
            IEnumerator DoAction()
            {
                while (true)
                {
                    action();
                    yield return new WaitForSeconds(waitTime);
                }
            }

            return monoBehaviour.StartCoroutine(DoAction());
        }

    }

}