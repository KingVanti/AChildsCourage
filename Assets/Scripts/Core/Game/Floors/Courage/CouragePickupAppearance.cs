using UnityEngine;

namespace AChildsCourage.Game.Floors.Courage
{

    public class CouragePickupAppearance
    {

        public CourageVariant Variant { get; }

        public float LightOuterRadius { get; }

        public float LightIntensity { get; }

        public Texture2D Emission { get; }

        public Sprite Sprite { get; }

        public Vector3 Scale { get; }


        public CouragePickupAppearance(CourageVariant variant, float lightOuterRadius, float lightIntensity, Texture2D emission, Sprite sprite, Vector3 scale)
        {
            Variant = variant;
            LightOuterRadius = lightOuterRadius;
            LightIntensity = lightIntensity;
            Emission = emission;
            Sprite = sprite;
            Scale = scale;
        }

    }

}