using System;
using System.Collections;
using UnityEngine;

namespace AChildsCourage
{

    internal static class CoroutineExtensions
    {

        internal static Coroutine RestartCoroutine(this MonoBehaviour monoBehaviour, Coroutine coroutine, Func<IEnumerator> routineFunction) => 
            monoBehaviour.RestartCoroutine(coroutine, routineFunction());

        internal static Coroutine RestartCoroutine(this MonoBehaviour monoBehaviour, Coroutine coroutine, IEnumerator routine)
        {
            if (coroutine != null) monoBehaviour.StopCoroutine(coroutine);
            return monoBehaviour.StartCoroutine(routine);
        }

        internal static Coroutine DoAfter(this MonoBehaviour monoBehaviour, Action action, float time)
        {
            IEnumerator WaitAndDo()
            {
                yield return new WaitForSeconds(time);
                action();
            }

            return monoBehaviour.StartCoroutine(WaitAndDo());
        }

        internal static Coroutine StartOnly(this MonoBehaviour monoBehaviour, Func<IEnumerator> routineFunction)
        {
            monoBehaviour.StopAllCoroutines();
            return monoBehaviour.StartCoroutine(routineFunction());
        }

        internal static Coroutine DoContinually(this MonoBehaviour monoBehaviour, Action action, float waitTime = 0)
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