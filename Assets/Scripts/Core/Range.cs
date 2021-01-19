using System;
using UnityEngine;

namespace AChildsCourage
{

    public static class Range
    {

        public static float Lerp(float t, Range<float> range) =>
            Mathf.Lerp(range.Min, range.Max, t);

    }

    [Serializable]
    public struct Range<TValue> where TValue : IComparable<TValue>
    {

        [SerializeField] private TValue min;
        [SerializeField] private TValue max;

        public TValue Min => min;

        public TValue Max => max;


        public Range(TValue min, TValue max)
        {
            this.min = min;
            this.max = max;
        }

    }

}