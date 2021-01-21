using AChildsCourage.Game.Char;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using static AChildsCourage.M;

namespace AChildsCourage.Game.Floors.Courage
{

    public class CouragePickupEntity : MonoBehaviour
    {

        private static readonly int emissionTextureKey = Shader.PropertyToID("_Emission");


        [SerializeField] private float illuminateSpeed;

        [FindComponent(ComponentFindMode.OnChildren)]
        private SpriteRenderer spriteRenderer;
        [FindComponent]
        private new Collider2D collider;
        [FindComponent(ComponentFindMode.OnChildren)]
        private Light2D lightSource;

        [FindInScene] private FlashlightEntity flashlight;

        private float charge;
        private float maxIntensity;


        private float Charge
        {
            get => charge;
            set
            {
                charge = value.Map(Clamp, 0f, 1f);
                lightSource.intensity = Mathf.Lerp(0, maxIntensity, Charge);

                spriteRenderer.enabled = Charge > 0;
                collider.enabled = Charge > 0;
            }
        }


        internal CourageVariant Variant { get; private set; }

        private void Update()
        {
            if (flashlight.ShinesOn(transform.position))
                Charge += illuminateSpeed * Time.deltaTime;
        }


        internal void Initialize(CourageVariant variant, CouragePickupAppearance appearance)
        {
            Variant = variant;
            SetAppearance(appearance);
            Charge = 0;
        }

        private void SetAppearance(CouragePickupAppearance appearance)
        {
            spriteRenderer.sprite = appearance.Sprite;
            spriteRenderer.transform.localScale = appearance.Scale;
            spriteRenderer.material.SetTexture(emissionTextureKey, appearance.Emission);
            lightSource.pointLightOuterRadius = appearance.LightOuterRadius;
            maxIntensity = appearance.LightIntensity;
        }

    }

}