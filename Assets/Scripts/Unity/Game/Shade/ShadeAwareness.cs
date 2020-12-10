using System;
using AChildsCourage.Game.Items;
using AChildsCourage.Game.Player;
using UnityEngine;
using UnityEngine.Events;
using static AChildsCourage.Game.Shade.MAwareness;
using CharacterController = AChildsCourage.Game.Player.CharacterController;

namespace AChildsCourage.Game.Shade
{

    public class ShadeAwareness : MonoBehaviour
    {

        #region Subtypes

        [Serializable]
        public class AwarenessLevelEvent : UnityEvent<AwarenessLevel> { }

        #endregion

        #region Fields

        public AwarenessLevelEvent onAwarenessLevelChanged;
        public Events.Float onAwarenessChanged;

#pragma warning disable 649

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
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Flashlight flashlight;

#pragma warning  restore 649

        private Awareness currentAwareness;
        private AwarenessLevel currentAwarenessLevel;

        #endregion

        #region Properties

        public AwarenessLevel CurrentAwarenessLevel
        {
            get => currentAwarenessLevel;
            set
            {
                if (currentAwarenessLevel == value)
                    return;
                currentAwarenessLevel = value;
                onAwarenessLevelChanged.Invoke(currentAwarenessLevel);
            }
        }

        public Visibility CurrentCharacterVisibility { get; set; }

        public Awareness CurrentAwareness
        {
            get => currentAwareness;
            private set
            {
                if (currentAwareness.Equals(value))
                    return;

                currentAwareness = value;
                onAwarenessChanged.Invoke(currentAwareness.Value);
                UpdateAwarenessLevel();
            }
        }


        private float CurrentAwarenessGain => baseAwarenessGainPerSecond * PrimaryVisionMultiplier * DistanceMultiplier * MovementMultiplier * FlashLightMultiplier;

        private float PrimaryVisionMultiplier => CurrentCharacterVisibility == Visibility.Primary ? primaryVisionMultiplier : 1;

        private float DistanceMultiplier => DistanceToCharacter.Remap(minDistance, maxDistance, maxDistanceMultiplier, 1);

        private float DistanceToCharacter => Vector3.Distance(transform.position, characterController.transform.position);

        private float MovementMultiplier
        {
            get
            {
                switch (characterController.CurrentMovmentState)
                {
                    case MovementState.Standing:
                        return 1;
                    case MovementState.Walking:
                        return walkingMultiplier;
                    case MovementState.Sprinting:
                        return sprintingMultiplier;
                    default:
                        throw new Exception("Invalid movement state!");
                }
            }
        }

        private float FlashLightMultiplier => flashlight.IsTurnedOn ? flashLightMultiplier : 1;

        #endregion

        #region Methods

        private void Update()
        {
            if (CurrentCharacterVisibility == Visibility.NotVisible)
                LooseAwareness();
            else
                GainAwareness();
        }

        private void LooseAwareness()
        {
            CurrentAwareness = MAwareness.LooseAwareness(CurrentAwareness, awarenessLossPerSecond * Time.deltaTime);
        }

        private void GainAwareness()
        {
            CurrentAwareness = MAwareness.GainAwareness(CurrentAwareness, CurrentAwarenessGain * Time.deltaTime);
        }

        private void UpdateAwarenessLevel()
        {
            if (CurrentAwareness.Value >= minHuntingAwareness)
                CurrentAwarenessLevel = AwarenessLevel.Hunting;
            else if (CurrentAwareness.Value >= minSuspiciousAwareness)
                CurrentAwarenessLevel = AwarenessLevel.Suspicious;
            else
                CurrentAwarenessLevel = AwarenessLevel.Oblivious;
        }

        #endregion

    }

}