using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

namespace AChildsCourage
{

    public static class Lerper
    {

        #region Methods

        public static IEnumerator StepLerp(Action<float> stepFunction, float stepSize)
        {
            float t = 0;

            while (t < 1)
            {
                stepFunction?.Invoke(t);
                yield return t;
                t = Mathf.MoveTowards(t, 1, stepSize);
            }

            stepFunction?.Invoke(t);
            yield return t;
        }


        public static IEnumerator TimeLerp(Action<float> stepFunction, float time, Action onLerpCompleted = null)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            stepFunction?.Invoke(0);
            yield return 0;

            while (stopwatch.Elapsed.TotalSeconds < time)
            {
                float t = Mathf.Clamp((float)(stopwatch.Elapsed.TotalSeconds / time), 0, 1);

                stepFunction?.Invoke(t);
                yield return t;
            }

            stepFunction?.Invoke(1);
            yield return 1;

            stopwatch.Stop();

            onLerpCompleted?.Invoke();
        }

        #endregion

    }

}