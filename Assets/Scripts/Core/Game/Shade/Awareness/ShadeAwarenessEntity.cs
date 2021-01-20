using System;
using AChildsCourage.Game.Char;
using UnityEngine;
using static AChildsCourage.M;
using static AChildsCourage.Game.Shade.Awareness;
using static AChildsCourage.Game.Char.Visibility;

namespace AChildsCourage.Game.Shade
{

    public class ShadeAwarenessEntity : MonoBehaviour
    {

        [Pub] public event EventHandler<CharLostEventArgs> OnCharLost;

        [Pub] public event EventHandler<CharSpottedEventArgs> OnCharSpotted;

        [Pub] public event EventHandler<CharSuspectedEventArgs> OnCharSuspected;

        [Pub] public event EventHandler<AwarenessChangedEventArgs> OnShadeAwarenessChanged;

        [SerializeField] private float baseAwarenessGainPerSecond;
        [SerializeField] private float primaryVisionMultiplier;
        [SerializeField] private float maxDistanceMultiplier;
        [SerializeField] private float charLitMultiplier;
        [SerializeField] private float isShoneOnMultiplier;
        [SerializeField] private float maxDistance;
        [SerializeField] private EnumArray<AwarenessLevel, float> awarenessLossPerSecond;
        [SerializeField] private EnumArray<MovementState, float> movementStateMultipliers;
        [SerializeField] private EnumArray<AwarenessLevel, float> minAwarenessForAwarenessLevel;

        [FindInScene] private CharControllerEntity charController;
        [FindInScene] private LightMeterEntity lightMeter;
        [FindInScene] private FlashlightEntity flashlight;

        private Visibility currentCharVisibility;
        private Awareness currentAwareness;
        private AwarenessLevel currentAwarenessLevel;
        private LastKnownCharInfo lastKnownCharInfo;


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
                if (CurrentAwarenessLevel == value) return;

                switch (value)
                {
                    case AwarenessLevel.Aware when CurrentAwarenessLevel != AwarenessLevel.Aware:
                        OnCharSpotted?.Invoke(this, new CharSpottedEventArgs(CharPosition));
                        break;
                    case AwarenessLevel.Suspicious when CurrentAwarenessLevel == AwarenessLevel.Aware:
                    case AwarenessLevel.Oblivious when CurrentAwarenessLevel == AwarenessLevel.Suspicious:
                        OnCharLost?.Invoke(this, new CharLostEventArgs(lastKnownCharInfo));
                        break;
                    case AwarenessLevel.Suspicious when CurrentAwarenessLevel == AwarenessLevel.Oblivious:
                        OnCharSuspected?.Invoke(this, new CharSuspectedEventArgs(CharPosition));
                        break;
                }

                currentAwarenessLevel = value;
            }
        }

        private float AwarenessGainPerSecond => baseAwarenessGainPerSecond * PrimaryVisionMultiplier *
                                                DistanceMultiplier * MovementMultiplier *
                                                FlashLightMultiplier * IsShoneOnMultiplier;

        private float PrimaryVisionMultiplier => currentCharVisibility.Equals(primary) ? primaryVisionMultiplier : 1;

        private float DistanceMultiplier => DistanceToCharacter.Map(Remap, 0f, maxDistance, maxDistanceMultiplier, 0f);

        private float DistanceToCharacter => Vector3.Distance(transform.position, charController.transform.position);

        private float MovementMultiplier => movementStateMultipliers[charController.CurrentMovementState];

        private float FlashLightMultiplier => lightMeter.DetectsLight ? charLitMultiplier : 1;

        private bool IsShoneOn => flashlight.ShinesOn(transform.position);

        private float IsShoneOnMultiplier => IsShoneOn ? isShoneOnMultiplier : 1;

        private float AwarenessChangePerSecond => CanSeeChar || IsShoneOn ? AwarenessGainPerSecond : -awarenessLossPerSecond[CurrentAwarenessLevel];

        private bool CanSeeChar => !currentCharVisibility.Equals(notVisible);

        private Vector2 CharPosition => charController.transform.position;

        private Vector2 CharVelocity => charController.Velocity;


        private void Update()
        {
            UpdateAwareness();
            UpdateLastKnownCharInfo();
        }

        private void UpdateLastKnownCharInfo()
        {
            if (CurrentAwarenessLevel == AwarenessLevel.Aware)
                lastKnownCharInfo = new LastKnownCharInfo(CharPosition, CharVelocity, Time.time);
        }

        private void UpdateAwareness() =>
            CurrentAwareness = CurrentAwareness.Map(ChangeBy, AwarenessChangePerSecond * Time.deltaTime);

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