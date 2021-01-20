using UnityEngine;
using static AChildsCourage.M;
using static AChildsCourage.Range;

namespace AChildsCourage.Game.Char
{

    public readonly struct FlashlightShine
    {

        public static bool ShinesOn(Vector2 position, FlashlightShine shine) =>
            Vector2.Distance(position, shine.position) <= shine.OuterRadius;

        private static float CalculateOuterRadius(float power, FlashlightParams @params) =>
            @params.OuterRadiusRange.Map(Lerp, 1f - power);

        private static float CalculateInnerRadius(float power, FlashlightParams @params) =>
            @params.InnerRadiusRange.Map(Lerp, 1f - power);

        private static float CalculateIntensity(float power, FlashlightParams @params) =>
            power * @params.MaxIntensity;

        private static float CalculatePower(float distance, FlashlightParams @params) =>
            distance.Map(Remap, 0f, @params.MaxShineDistance, 1f, 0f).Map(Squared);


        private readonly Vector2 position;


        public float Power { get; }

        public float OuterRadius { get; }

        public float InnerRadius { get; }

        public float Intensity { get; }


        public FlashlightShine(Vector2 position, float distance, FlashlightParams @params)
        {
            this.position = position;
            Power = CalculatePower(distance, @params);
            OuterRadius = CalculateOuterRadius(Power, @params);
            InnerRadius = CalculateInnerRadius(Power, @params);
            Intensity = CalculateIntensity(Power, @params);
        }

    }

}