using System;
using AChildsCourage.Game.Char;
using AChildsCourage.Infrastructure;
using UnityEngine;
using static AChildsCourage.Game.Shade.MAwareness;
using static AChildsCourage.Game.Shade.MVisibility;
using static AChildsCourage.MRange;

namespace AChildsCourage.Game.Shade
{

    public class ShadeAwarenessEntity : MonoBehaviour
    {

        [Pub] public event EventHandler<AwarenessChangedEventArgs> OnShadeAwarenessChanged;

        [SerializeField] private float awarenessLossPerSecond;
        [SerializeField] private float baseAwarenessGainPerSecond;
        [SerializeField] private float primaryVisionMultiplier;
        [SerializeField] private float maxDistanceMultiplier;
        [SerializeField] private float flashLightMultiplier;
        [SerializeField] private Range<float> distanceRange;
        [SerializeField] private EnumArray<MovementState, float> movementStateMultipliers;
        [SerializeField] private EnumArray<AwarenessLevel, float> minAwarenessForAwarenessLevel;

        [FindInScene] private CharControllerEntity charController;
        [FindInScene] private FlashlightEntity flashlight;

        private Visibility currentCharVisibility;
        private Awareness currentAwareness;
        private AwarenessLevel currentAwarenessLevel;


        public Awareness CurrentAwareness
        {
            get => currentAwareness;
            private set
            {
                if (currentAwareness.Equals(value)) return;

                currentAwareness = value;
                currentAwarenessLevel = CalculateAwarenessLevel();
                OnShadeAwarenessChanged?.Invoke(this, new AwarenessChangedEventArgs(CurrentAwareness, currentAwarenessLevel));
            }
        }


        private float AwarenessGainPerSecond => baseAwarenessGainPerSecond * PrimaryVisionMultiplier * DistanceMultiplier * MovementMultiplier * FlashLightMultiplier;

        private float PrimaryVisionMultiplier => currentCharVisibility == Visibility.Primary ? primaryVisionMultiplier : 1;

        private float DistanceMultiplier => Remap(DistanceToCharacter, distanceRange, Between(maxDistanceMultiplier, 1));

        private float DistanceToCharacter => Vector3.Distance(transform.position, charController.transform.position);

        private float MovementMultiplier => movementStateMultipliers[charController.CurrentMovementState];

        private float FlashLightMultiplier => flashlight.IsTurnedOn ? flashLightMultiplier : 1;

        private float AwarenessChange => CanSeeChar ? AwarenessGainPerSecond : -awarenessLossPerSecond;

        private bool CanSeeChar => currentCharVisibility != Visibility.NotVisible;


        private void Update() =>
            UpdateAwareness();

        private void UpdateAwareness() =>
            CurrentAwareness = CurrentAwareness.Map(ChangeBy, AwarenessChange * Time.deltaTime);


        [Sub(nameof(ShadeBodyEntity.OnShadeOutOfBounds))]
        private void OnShadeBanished(object _1, EventArgs _2) =>
            ClearAwareness();

        private void ClearAwareness() =>
            CurrentAwareness = NoAwareness;

        private AwarenessLevel CalculateAwarenessLevel() =>
            HasEnoughAwarenessForLevel(AwarenessLevel.Hunting) ? AwarenessLevel.Hunting
            : HasEnoughAwarenessForLevel(AwarenessLevel.Suspicious) ? AwarenessLevel.Suspicious
            : AwarenessLevel.Oblivious;

        private bool HasEnoughAwarenessForLevel(AwarenessLevel level) =>
            CurrentAwareness.Value >= minAwarenessForAwarenessLevel[level];


        [Sub(nameof(ShadeEyesEntity.OnCharVisibilityChanged))]
        private void OnCharVisibilityChanged(object _, CharVisibilityChangedEventArgs eventArgs) =>
            currentCharVisibility = eventArgs.CharVisibility;

    }

}