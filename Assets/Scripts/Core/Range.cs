﻿using System;
using UnityEngine;

namespace AChildsCourage
{

    public static class MRange
    {

        public static float Lerp(Range<float> range, float t) => range.Min + (range.Max - range.Min) * t;

        [Serializable]
        public struct Range<TValue> where TValue : IComparable<TValue>
        {

            public TValue Min => min;

            public TValue Max => max;

#pragma warning disable 649

            [SerializeField] private TValue min;
            [SerializeField] private TValue max;

#pragma warning restore 649

        }

    }

}