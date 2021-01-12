using System;
using AChildsCourage.Game.Floors.Courage;
using AChildsCourage.Game.Shade;
using UnityEngine;
using static AChildsCourage.Game.Char.TensionLevelRange;
using static AChildsCourage.Game.Char.TensionMeter;
using static AChildsCourage.Range;

namespace AChildsCourage.Game.Char
{

    public class TensionMeterEntity : MonoBehaviour
    {

        [Pub] public event EventHandler<TensionLevelChangedEventArgs> OnTensionLevelChanged;


        [SerializeField] private float canSeeShadeGain;
        [SerializeField] private float shadeDetectionDistance;
        [SerializeField] private Range<float> shadeDistanceGainRange;
        [SerializeField] private float baseDrain;
        [SerializeField] private Range<float> minNormalTensionRange;
        [SerializeField] private Range<float> minHighTensionRange;

        [FindInScene] private FlashlightEntity flashlight;
        [FindInScene] private ShadeBodyEntity shade;
        [FindInScene] private CharControllerEntity @char;

        private TensionLevel tensionLevel;
        private TensionMeter tensionMeter = EmptyTensionMeter;
        private TensionLevelRange currentTensionLevelRange;


        private TensionLevel TensionLevel
        {
            get => tensionLevel;
            set
            {
                if (tensionLevel == value) return;

                tensionLevel = value;
                OnTensionLevelChanged?.Invoke(this, new TensionLevelChangedEventArgs(TensionLevel));
            }
        }

        public TensionMeter TensionMeter
        {
            get => tensionMeter;
            private set
            {
                tensionMeter = value;
                TensionLevel = currentTensionLevelRange.Map(CalculateLevel, value.Tension);
            }
        }

        private bool CanSeeShade => flashlight.ShinesOn(ShadePosition);

        private float ShadeDistance => Vector2.Distance(ShadePosition, CharPosition);

        private bool IsInShadeDetectionRange => ShadeDistance < shadeDetectionDistance;

        private float ShadeDistanceInterpolator => ShadeDistance.Remap(0, shadeDetectionDistance, 1, 0);

        private float ShadeDistanceGain => shadeDistanceGainRange.Map(Lerp, ShadeDistanceInterpolator);

        private float TensionDelta =>
            IsInShadeDetectionRange
                ? ShadeDistanceGain + (CanSeeShade ? canSeeShadeGain : 0)
                : -baseDrain;

        private Vector2 ShadePosition => shade.transform.position;

        private Vector2 CharPosition => @char.transform.position;


        private void Update() =>
            UpdateTensionMeter();

        private void UpdateTensionMeter() =>
            TensionMeter = TensionMeter.Map(ChangeBy, TensionDelta * Time.deltaTime);

        [Sub(nameof(CourageManagerEntity.OnCollectedCourageChanged))]
        private void OnCollectedCourageChanged(object _, CollectedCourageChangedEventArgs eventArgs) =>
            UpdateTensionLevelRange(eventArgs.CompletionPercent);

        private void UpdateTensionLevelRange(float completionPercent) =>
            currentTensionLevelRange = CalculateTensionLevelRange(completionPercent);

        private TensionLevelRange CalculateTensionLevelRange(float completionPercent) =>
            new TensionLevelRange(minNormalTensionRange.Map(Lerp, completionPercent),
                                  minHighTensionRange.Map(Lerp, completionPercent));

    }

}