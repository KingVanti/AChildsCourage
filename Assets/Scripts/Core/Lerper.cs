﻿using System;
using System.Collections;
using UnityEngine;
using static AChildsCourage.Range;

namespace AChildsCourage
{

    internal static class Lerping
    {

        internal static IEnumerator TimeLerp(Action<float> stepFunction, float time, Action onLerpCompleted = null) =>
            TimeLerp(new Range<float>(0, 1), stepFunction, time, onLerpCompleted);

        internal static IEnumerator TimeLerp(Range<float> range, Action<float> stepFunction, float time, Action onLerpCompleted = null)
        {
            var t = 0f;

            void UpdateStepFunction() =>
                stepFunction?.Invoke(range.Map(Lerp, t));

            UpdateStepFunction();

            while (t < 1)
            {
                t = Mathf.MoveTowards(t, 1, Time.deltaTime / time);
                UpdateStepFunction();
                yield return null;
            }

            onLerpCompleted?.Invoke();
        }

    }

}