using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace AChildsCourage
{

    internal static class MLerper
    {

        internal static IEnumerator StepLerp(Action<float> stepFunction, float stepSize)
        {
            var t = 0f;

            while (t < 1)
            {
                stepFunction?.Invoke(t);
                yield return t;
                t = Mathf.MoveTowards(t, 1, stepSize);
            }

            stepFunction?.Invoke(t);
            yield return t;
        }


        internal static IEnumerator TimeLerp(Action<float> stepFunction, float time, Action onLerpCompleted = null)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            stepFunction?.Invoke(0);
            yield return 0;

            while (stopwatch.Elapsed.TotalSeconds < time)
            {
                var t = Mathf.Clamp((float) (stopwatch.Elapsed.TotalSeconds / time), 0, 1);

                stepFunction?.Invoke(t);
                yield return t;
            }

            stepFunction?.Invoke(1);
            yield return 1;

            stopwatch.Stop();

            onLerpCompleted?.Invoke();
        }

    }

}