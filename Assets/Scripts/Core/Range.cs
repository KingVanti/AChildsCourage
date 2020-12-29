using System;
using UnityEngine;

namespace AChildsCourage
{

    public static class MRange
    {

        public static float Lerp(Range<float> range, float t) =>
            range.Min + (range.Max - range.Min) * t;

        public static float Remap(float value, Range<float> r1, Range<float> r2) => 
            value.Remap(r1.Min, r1.Max, r2.Min, r2.Max);

        public static Range<TValue> Between<TValue>(TValue from, TValue to) where TValue : IComparable<TValue> =>
            new Range<TValue>(from, to);

        
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

}