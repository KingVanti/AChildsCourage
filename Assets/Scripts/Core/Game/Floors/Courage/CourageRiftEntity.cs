using System;
using System.Collections;
using AChildsCourage.Game.Char;
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
        [SerializeField] private AnimationCurve lightCurve;

        [FindComponent] private SpriteRenderer spriteRenderer;
        [FindComponent(ComponentFindMode.OnChildren)]
        private ParticleSystem riftParticleSystem;

        [FindComponent(ComponentFindMode.OnChildren)]
        private Light2D courageLight;
        [FindInScene] private CourageManagerEntity courageManager;

        private bool isOpen;
        private bool isEscaping;
        private Coroutine escapeCoroutine;

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

        private float LightIntensity
        {
            get => courageLight.intensity;
            set => courageLight.intensity = value;
        }

        private float LightOuterRadius
        {
            get => courageLight.pointLightOuterRadius;
            set => courageLight.pointLightOuterRadius = value;
        }

        private int MaxSpriteIndex => riftStageSprites.Length - 1;

        private void Start() => StartCoroutine(CourageLighting());

        private void Leave() => OnCharEnteredRift?.Invoke(this, EventArgs.Empty);

        [Sub(nameof(CharControllerEntity.OnRiftEscapeUpdate))]
        private void OnCharacterEscaping(object _, RiftEscapeEventArgs eventArgs)
        {
            isEscaping = eventArgs.IsEscapingThroughRift;

            if (isEscaping)
                escapeCoroutine = StartCoroutine(Escaping());
            else
                StopCoroutine(escapeCoroutine);
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

        private IEnumerator Escaping()
        {
            while (isEscaping)
            {
                yield return new WaitForSeconds(escapeTime);
                Leave();
            }
        }

        private IEnumerator CourageLighting()
        {
            var graphValue = 0f;
            var speed = 1 / escapeTime;

            while (true)
            {
                if (isEscaping)
                    graphValue = Mathf.MoveTowards(graphValue, 1, speed * Time.deltaTime);
                else
                    graphValue = Mathf.MoveTowards(graphValue, 0, speed * Time.deltaTime * 5f);

                LightIntensity = Mathf.Clamp(lightCurve.Evaluate(graphValue) * maxLightIntensity, 0.3f, maxLightIntensity);
                LightOuterRadius = Mathf.Clamp(lightCurve.Evaluate(graphValue) * maxOuterRadius, 12, maxOuterRadius);

                yield return null;
            }
        }

    }

}