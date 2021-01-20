using System;
using UnityEngine;

namespace AChildsCourage.Game.Char
{

    [Serializable]
    public struct FlashlightParams
    {

        [SerializeField] private float maxShineDistance;
        [SerializeField] private float maxIntensity;
        [SerializeField] private Range<float> innerRadiusRange;
        [SerializeField] private Range<float> outerRadiusRange;


        public float MaxShineDistance => maxShineDistance;

        public float MaxIntensity => maxIntensity;

        public Range<float> InnerRadiusRange => innerRadiusRange;

        public Range<float> OuterRadiusRange => outerRadiusRange;

    }

}