using System;
using AChildsCourage.Game.Char;
using UnityEngine;
using static AChildsCourage.Game.Shade.Awareness;
using static AChildsCourage.Game.Shade.Visibility;
using static AChildsCourage.Range;

namespace AChildsCourage.Game.Shade
{

    public class ShadeAwarenessEntity : MonoBehaviour
    {

        [Pub] public event EventHandler<AwarenessChangedEventArgs> OnShadeAwarenessChanged;
        [Pub] public event EventHandler<CharSpottedEventArgs> OnCharSpotted;

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
                CurrentAwarenessLevel = CalculateAwarenessLevel();
                OnShadeAwarenessChanged?.Invoke(this, new AwarenessChangedEventArgs(CurrentAwareness, CurrentAwarenessLevel));
            }
        }

        private AwarenessLevel CurrentAwarenessLevel
        {
            get => currentAwarenessLevel;
            set
            {
                if(CurrentAwarenessLevel == value) return;

                currentAwarenessLevel = value;

                switch (CurrentAwarenessLevel)
                {
                    case AwarenessLevel.Aware: 
                        OnCharSpotted?.Invoke(this, new CharSpottedEventArgs(CharPosition));
                        break;
                }
            }
        }

        private float AwarenessGainPerSecond => baseAwarenessGainPerSecond * PrimaryVisionMultiplier * DistanceMultiplier * MovementMultiplier * FlashLightMultiplier;

        private float PrimaryVisionMultiplier => currentCharVisibility.Equals(Primary) ? primaryVisionMultiplier : 1;

        private float DistanceMultiplier => Remap(DistanceToCharacter, distanceRange, Between(maxDistanceMultiplier, 1));

        private float DistanceToCharacter => Vector3.Distance(transform.position, charController.transform.position);

        private float MovementMultiplier => movementStateMultipliers[charController.CurrentMovementState];

        private float FlashLightMultiplier => flashlight.IsTurnedOn ? flashLightMultiplier : 1;

        private float AwarenessChange => CanSeeChar ? AwarenessGainPerSecond : -awarenessLossPerSecond;

        private bool CanSeeChar => !currentCharVisibility.Equals(NotVisible);

        private Vector2 CharPosition => charController.transform.position;


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
            HasEnoughAwarenessForLevel(AwarenessLevel.Aware) ? AwarenessLevel.Aware
            : HasEnoughAwarenessForLevel(AwarenessLevel.Suspicious) ? AwarenessLevel.Suspicious
            : AwarenessLevel.Oblivious;

        private bool HasEnoughAwarenessForLevel(AwarenessLevel level) =>
            CurrentAwareness >= minAwarenessForAwarenessLevel[level];

        [Sub(nameof(ShadeEyesEntity.OnCharVisibilityChanged))]
        private void OnCharVisibilityChanged(object _, CharVisibilityChangedEventArgs eventArgs) =>
            currentCharVisibility = eventArgs.CharVisibility;

    }

}