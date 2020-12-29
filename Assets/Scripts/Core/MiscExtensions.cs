using System;
using System.Collections;
using UnityEngine;

namespace AChildsCourage
{

    public static class MiscExtensions
    {

        public static Coroutine RestartCoroutine(this MonoBehaviour monoBehaviour, Coroutine coroutine, Func<IEnumerator> routineFunction)
        {
            if (coroutine != null) monoBehaviour.StopCoroutine(coroutine);
            return monoBehaviour.StartCoroutine(routineFunction());
        }

    }

}