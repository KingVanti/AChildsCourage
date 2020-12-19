using System;
using AChildsCourage.Game.Char;
using AChildsCourage.Infrastructure;
using UnityEngine;
using static AChildsCourage.Game.Shade.MAwareness;
using static AChildsCourage.Game.Shade.MVisibility;

namespace AChildsCourage.Game.Shade
{

    public class ShadeAwarenessEntity : MonoBehaviour
    {

        [Pub] public event EventHandler<AwarenessChangedEventArgs> OnShadeAwarenessChanged;

        #region Fields



        [SerializeField] private float awarenessLossPerSecond;
        [SerializeField] private float baseAwarenessGainPerSecond;
        [SerializeField] private float primaryVisionMultiplier;
        [SerializeField] private float minDistance;
        [SerializeField] private float maxDistance;
        [SerializeField] private float maxDistanceMultiplier;
        [SerializeField] private float walkingMultiplier;
        [SerializeField] private float sprintingMultiplier;
        [SerializeField] private float flashLightMultiplier;
        [SerializeField] private float minSuspiciousAwareness;
        [SerializeField] private float minHuntingAwareness;

        [FindInScene] private CharControllerEntity charController;
        [FindInScene] private Flashlight flashlight;

#pragma warning  restore 649

        private Visibility currentCharVisibility;
        private Awareness currentAwareness;
        private AwarenessLevel currentAwarenessLevel;

        #endregion

        #region Properties

        public Awareness CurrentAwareness
        {
            get => currentAwareness;
            private set
            {
                if (currentAwareness.Equals(value)) return;

                currentAwareness = value;
                UpdateAwarenessLevel();
                OnShadeAwarenessChanged?.Invoke(this, new AwarenessChangedEventArgs(CurrentAwareness, currentAwarenessLevel));
            }
        }


        private float CurrentAwarenessGain => baseAwarenessGainPerSecond * PrimaryVisionMultiplier * DistanceMultiplier * MovementMultiplier * FlashLightMultiplier;

        private float PrimaryVisionMultiplier => currentCharVisibility == Visibility.Primary
            ? primaryVisionMultiplier
            : 1;

        private float DistanceMultiplier => DistanceToCharacter.Remap(minDistance, maxDistance, maxDistanceMultiplier, 1);

        private float DistanceToCharacter => Vector3.Distance(transform.position, charController.transform.position);

        private float MovementMultiplier
        {
            get
            {
                switch (charController.CurrentMovementState)
                {
                    case MovementState.Standing: return 1;
                    case MovementState.Walking: return walkingMultiplier;
                    case MovementState.Sprinting: return sprintingMultiplier;
                    default: throw new Exception("Invalid movement state!");
                }
            }
        }

        private float FlashLightMultiplier => flashlight.IsTurnedOn
            ? flashLightMultiplier
            : 1;

        #endregion

        #region Methods

        [Sub(nameof(ShadeBrainEntity.OnShadeBanished))]
        private void OnShadeBanished(object _1, EventArgs _2) => ClearAwareness();

        private void ClearAwareness() => CurrentAwareness = NoAwareness;


        private void Update()
        {
            if (currentCharVisibility == Visibility.NotVisible)
                LooseAwareness();
            else
                GainAwareness();
        }

        private void LooseAwareness() => CurrentAwareness = MAwareness.LooseAwareness(CurrentAwareness, awarenessLossPerSecond * Time.deltaTime);

        private void GainAwareness() => CurrentAwareness = MAwareness.GainAwareness(CurrentAwareness, CurrentAwarenessGain * Time.deltaTime);

        private void UpdateAwarenessLevel()
        {
            if (CurrentAwareness.Value >= minHuntingAwareness)
                currentAwarenessLevel = AwarenessLevel.Hunting;
            else if (CurrentAwareness.Value >= minSuspiciousAwareness)
                currentAwarenessLevel = AwarenessLevel.Suspicious;
            else
                currentAwarenessLevel = AwarenessLevel.Oblivious;
        }


        [Sub(nameof(ShadeEyesEntity.OnCharVisibilityChanged))]
        private void OnCharVisibilityChanged(object _, CharVisibilityChangedEventArgs eventArgs) => currentCharVisibility = eventArgs.CharVisibility;

        #endregion

    }

}