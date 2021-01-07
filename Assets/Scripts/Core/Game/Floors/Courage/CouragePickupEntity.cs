using AChildsCourage.Game.Char;
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

        private float maxIntensity;
        [SerializeField] private float maxPlayerDistance = default;
        [SerializeField] private float illuminateSpeed = default;

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
            maxIntensity = appearance.LightIntensity;
        }

        private void OnTriggerStay2D(Collider2D collision) {

            if (collision.CompareTag(EntityTags.Flashlight)) {

                FlashlightEntity fe = collision.GetComponent<FlashlightEntity>();

                if (fe.IsTurnedOn && (fe.DistanceToCharacter <= maxPlayerDistance)) {
                    if (lightSource.intensity <= maxIntensity) {
                        lightSource.intensity = Mathf.MoveTowards(lightSource.intensity, maxIntensity, Time.deltaTime * illuminateSpeed);
                    }
                }
            }

        }



    }

}