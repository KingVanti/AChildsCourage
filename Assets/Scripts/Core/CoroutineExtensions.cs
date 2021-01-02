using System;
using System.Collections;
using UnityEngine;

namespace AChildsCourage
{

    public static class CoroutineExtensions
    {

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