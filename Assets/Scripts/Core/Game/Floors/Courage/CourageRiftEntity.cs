using System;
using System.Collections;
using AChildsCourage.Game.Char;
using JetBrains.Annotations;
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

        [FindComponent(ComponentFindMode.OnChildren)]
        private CanvasGroup contextInfo;

        [FindComponent] private SpriteRenderer spriteRenderer;

        [FindComponent(ComponentFindMode.OnChildren)]
        private ParticleSystem riftParticleSystem;

        [FindComponent(ComponentFindMode.OnChildren)]
        private Light2D courageLight;

        [SerializeField] private AnimationCurve lightCurve;
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
            set => courageLight.intensity = value;
        }

        private float LightOuterRadius
        {
            set => courageLight.pointLightOuterRadius = value;
        }

        private int MaxSpriteIndex => riftStageSprites.Length - 1;

        private void Start() => StartCoroutine(CourageLighting());

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(EntityTags.Char))
                if (isOpen)
                    contextInfo.alpha = 1;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(EntityTags.Char)) contextInfo.alpha = 0;
        }

        private void Leave() => OnCharEnteredRift?.Invoke(this, EventArgs.Empty);

        [Sub(nameof(CharControllerEntity.OnRiftEscapeUpdate))] [UsedImplicitly]
        private void OnCharacterEscaping(object _, RiftEscapeEventArgs eventArgs)
        {
            isEscaping = eventArgs.IsEscapingThroughRift;

            if (isEscaping)
                escapeCoroutine = StartCoroutine(Escaping());
            else
                StopCoroutine(escapeCoroutine);
        }

        [Sub(nameof(FloorRecreatorEntity.OnFloorRecreated))] [UsedImplicitly]
        private void OnFloorRecreated(object _, FloorRecreatedEventArgs eventArgs) =>
            transform.position = eventArgs.Floor.Map(GetEndRoomCenter);

        [Sub(nameof(CourageManagerEntity.OnCollectedCourageChanged))] [UsedImplicitly]
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
                graphValue = isEscaping
                    ? Mathf.MoveTowards(graphValue, 1, speed * Time.deltaTime)
                    : Mathf.MoveTowards(graphValue, 0, speed * Time.deltaTime * 5f);

                LightIntensity = Mathf.Clamp(lightCurve.Evaluate(graphValue) * maxLightIntensity, 0.3f, maxLightIntensity);
                LightOuterRadius = Mathf.Clamp(lightCurve.Evaluate(graphValue) * maxOuterRadius, 12, maxOuterRadius);

                yield return null;
            }
        }

    }

}