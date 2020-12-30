using AChildsCourage.Infrastructure;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace AChildsCourage.Game.Floors.Courage
{

    public class CouragePickupEntity : MonoBehaviour
    {

        private static readonly int emissionTextureKey = Shader.PropertyToID("_Emission");


        [FindComponent(ComponentFindMode.OnChildren)]
        private SpriteRenderer spriteRenderer;
        [FindComponent(ComponentFindMode.OnChildren)]
        private Light2D lightSource;

        public CourageVariant Variant { get; private set; }


        public void Initialize(CourageVariant variant, CouragePickupAppearance appearance)
        {
            Variant = variant;
            SetAppearance(appearance);
        }

        private void SetAppearance(CouragePickupAppearance appearance)
        {
            spriteRenderer.sprite = appearance.Sprite;
            spriteRenderer.transform.localScale = appearance.Scale;
            spriteRenderer.material.SetTexture(emissionTextureKey, appearance.Emission);
            lightSource.pointLightOuterRadius = appearance.LightOuterRadius;
            lightSource.intensity = appearance.LightIntensity;
        }

    }

}