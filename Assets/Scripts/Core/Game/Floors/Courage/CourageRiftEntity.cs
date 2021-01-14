using AChildsCourage.Game.Char;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using static AChildsCourage.Game.Floors.Floor;

namespace AChildsCourage.Game.Floors.Courage
{

    public class CourageRiftEntity : MonoBehaviour
    {

        private const float HundredPercent = 1;

        [Pub] public event EventHandler OnCharEnteredRift;

        [SerializeField] private Sprite[] riftStageSprites = new Sprite[5];
        [SerializeField] private float escapeTime = 5f;
        [SerializeField] private float maxLightIntensity = 10f;
        [SerializeField] private float maxOuterRadius = 12f;

        [FindComponent] private SpriteRenderer spriteRenderer;
        [FindComponent(ComponentFindMode.OnChildren)]
        private ParticleSystem riftParticleSystem;

        [FindComponent(ComponentFindMode.OnChildren)] private Light2D courageLight;
        [FindInScene] private CourageManagerEntity courageManager;

        private bool isOpen;
        private bool isEscaping = false;
        private Coroutine escapeCoroutine;

        private float lightSpeed = 0f;

        private float EmissionRate
        {
            set
            {
                var emission = riftParticleSystem.emission;
                emission.rateOverTime = value;
            }
        }

        private Sprite Sprite
        {
            set => spriteRenderer.sprite = value;
        }

        private float LightIntensity {
            get => courageLight.intensity;
            set => courageLight.intensity = value;
        }

        private float LightOuterRadius {
            get => courageLight.pointLightOuterRadius;
            set => courageLight.pointLightOuterRadius = value;
        }

        private int MaxSpriteIndex => riftStageSprites.Length - 1;

        private void Start() {
            StartCoroutine(CourageLighting());
        }

        private void Leave() {
            OnCharEnteredRift?.Invoke(this, EventArgs.Empty);
        }

        [Sub(nameof(CharControllerEntity.OnRiftEscapeUpdate))]
        private void OnCharacterEscaping(object _, RiftEscapeEventArgs eventArgs) {

            isEscaping = eventArgs.IsEscapingThroughRift;

            if (isEscaping) {
                escapeCoroutine = StartCoroutine(Escaping());
            } else {
                StopCoroutine(escapeCoroutine);
            }

        }

        [Sub(nameof(FloorRecreatorEntity.OnFloorRecreated))]
        private void OnFloorRecreated(object _, FloorRecreatedEventArgs eventArgs) =>
            transform.position = eventArgs.Floor.Map(GetEndRoomCenter);

        [Sub(nameof(CourageManagerEntity.OnCollectedCourageChanged))]
        private void OnCollectedCourageChanged(object _, CollectedCourageChangedEventArgs eventArgs) =>
            UpdateRift(eventArgs.CompletionPercent);

        private void UpdateRift(float completionPercent)
        {
            isOpen = Mathf.Approximately(completionPercent, HundredPercent);
            UpdateRiftAppearance(completionPercent);
        }

        private void UpdateRiftAppearance(float completionPercent)
        {
            UpdateEmissionRate(completionPercent);
            Sprite = GetSpriteFor(completionPercent);
        }

        private Sprite GetSpriteFor(float completionPercent)
        {
            var spriteIndex = Mathf.FloorToInt(MaxSpriteIndex * completionPercent);
            return riftStageSprites[spriteIndex];
        }

        private void UpdateEmissionRate(float completionPercent) =>
            EmissionRate = Mathf.Lerp(2f, 20f, completionPercent);

        IEnumerator Escaping() {

            while (isEscaping) {
                yield return new WaitForSeconds(escapeTime);
                Leave();
            }
        }

        IEnumerator CourageLighting() {

            float intensitySpeed = (maxLightIntensity - 0.3f) / escapeTime;
            float radiusSpeed = (maxOuterRadius - 12) / escapeTime;

            while (true) {

                if (isEscaping) {
                    LightIntensity = Mathf.MoveTowards(LightIntensity, maxLightIntensity, intensitySpeed * Time.deltaTime);
                    LightOuterRadius = Mathf.MoveTowards(LightOuterRadius, maxOuterRadius, radiusSpeed * Time.deltaTime);
                } else {
                    LightIntensity = Mathf.MoveTowards(LightIntensity, 0.3f, Time.deltaTime * intensitySpeed * 5f);
                    LightOuterRadius = Mathf.MoveTowards(LightOuterRadius, 12, Time.deltaTime * radiusSpeed * 5f);
                }

                yield return null;

            }
        }

    }

}