using System;
using System.Collections;
using AChildsCourage.Game.Char;
using AChildsCourage.Game.Floors.Courage.UI;
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
        [SerializeField] private AnimationCurve lightCurve;

        [FindComponent] private SpriteRenderer spriteRenderer;
        [FindComponent(ComponentFindMode.OnChildren)] private ParticleSystem riftParticleSystem;
        [FindComponent(ComponentFindMode.OnChildren)] private Light2D courageLight;
        [FindComponent(ComponentFindMode.OnChildren)] private CourageRiftInfoText infoText;

        [FindInScene] private CourageManagerEntity courageManager;


        private bool riftIsOpen;
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

        private bool IsEscaping
        {
            get => isEscaping;
            set
            {
                if (isEscaping == value) return;

                isEscaping = value;

                if (IsEscaping)
                    escapeCoroutine = this.DoAfter(Leave, escapeTime);
                else if (escapeCoroutine != null)
                    StopCoroutine(escapeCoroutine);
            }
        }

        private void Start() =>
            StartCoroutine(ContinuouslyUpdateLighting());

        private void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.CompareTag(EntityTags.Char))
                OnCharEnteredRiftArea();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag(EntityTags.Char))
                OnCharLeftRiftArea();
        }

        private void OnCharEnteredRiftArea()
        {
            if (riftIsOpen)
                infoText.ShowEnterRiftInfo();
            else
                infoText.ShowNeedMoreCourageInfo();
        }

        private void OnCharLeftRiftArea() =>
            infoText.Hide();

        private void Leave() =>
            OnCharEnteredRift?.Invoke(this, EventArgs.Empty);

        [Sub(nameof(CharControllerEntity.OnRiftEscapeUpdate))] [UsedImplicitly]
        private void OnCharacterEscaping(object _, RiftEscapeEventArgs eventArgs) =>
            IsEscaping = eventArgs.IsEscapingThroughRift;

        [Sub(nameof(FloorRecreatorEntity.OnFloorRecreated))] [UsedImplicitly]
        private void OnFloorRecreated(object _, FloorRecreatedEventArgs eventArgs) =>
            transform.position = eventArgs.Floor.Map(GetEndRoomCenter);

        [Sub(nameof(CourageManagerEntity.OnCollectedCourageChanged))] [UsedImplicitly]
        private void OnCollectedCourageChanged(object _, CollectedCourageChangedEventArgs eventArgs) =>
            UpdateRift(eventArgs.CompletionPercent);

        private void UpdateRift(float completionPercent)
        {
            riftIsOpen = Mathf.Approximately(completionPercent, HundredPercent);
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

        private IEnumerator ContinuouslyUpdateLighting()
        {
            var t = 0f;

            while (true)
            {
                var targetValue = IsEscaping ? 1 : 0;
                var delta = Time.deltaTime / escapeTime * (isEscaping ? 1f : 5f);

                t = Mathf.MoveTowards(t, targetValue, delta);
                UpdateLighting(lightCurve.Evaluate(t));
                yield return null;
            }
        }

        private void UpdateLighting(float curveValue)
        {
            LightIntensity = Mathf.Clamp(curveValue * maxLightIntensity, 0.3f, maxLightIntensity);
            LightOuterRadius = Mathf.Clamp(curveValue * maxOuterRadius, 12, maxOuterRadius);
        }

    }

}