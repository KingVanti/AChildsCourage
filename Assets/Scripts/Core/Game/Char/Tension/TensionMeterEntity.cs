using AChildsCourage.Game.Shade;
using UnityEngine;
using static AChildsCourage.Game.Char.TensionMeter;

namespace AChildsCourage.Game.Char
{

    public class TensionMeterEntity : MonoBehaviour
    {

        [SerializeField] private float canSeeShadeGain;
        [SerializeField] private float shadeDetectionDistance;
        [SerializeField] private Range<float> shadeDistanceGainRange;
        [SerializeField] private float baseDrain;

        [FindInScene] private FlashlightEntity flashlight;
        [FindInScene] private ShadeBodyEntity shade;
        [FindInScene] private CharControllerEntity @char;


        public TensionMeter TensionMeter { get; private set; } = EmptyTensionMeter;

        private bool CanSeeShade => flashlight.ShinesOn(ShadePosition);

        private float ShadeDistance => Vector2.Distance(ShadePosition, CharPosition);

        private bool IsInShadeDetectionRange => ShadeDistance < shadeDetectionDistance;

        private float ShadeDistanceInterpolator => ShadeDistance.Remap(0, shadeDetectionDistance, 1, 0);

        private float ShadeDistanceGain => shadeDistanceGainRange.Map(Range.Lerp, ShadeDistanceInterpolator);

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

    }

}