using System;
using UnityEngine;
using UnityEngine.Events;
using static AChildsCourage.Game.Monsters.MAwareness;

namespace AChildsCourage.Game.Monsters
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
        [SerializeField] private Transform characterTransform;

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

        public float CurrentAwareness => currentAwareness.Value;
        
        
        private float CurrentAwarenessGain => baseAwarenessGainPerSecond * PrimaryVisionMultiplier * DistanceMultiplier;

        private float PrimaryVisionMultiplier => CurrentCharacterVisibility == Visibility.Primary ? primaryVisionMultiplier : 1;

        private float DistanceMultiplier => DistanceToCharacter.Remap(minDistance, maxDistance, maxDistanceMultiplier, 1);
        
        private float DistanceToCharacter => Vector3.Distance(transform.position, characterTransform.position);
        
        #endregion

        #region Methods

        private void Update()
        {
            if (CurrentCharacterVisibility == Visibility.NotVisible)
                LooseAwareness();
            else
                GainAwareness();
            
            onAwarenessChanged.Invoke(CurrentAwareness);
        }

        private void LooseAwareness()
        {
            currentAwareness = MAwareness.LooseAwareness(currentAwareness, awarenessLossPerSecond * Time.deltaTime);
        }

        private void GainAwareness()
        {
            currentAwareness = MAwareness.GainAwareness(currentAwareness, CurrentAwarenessGain * Time.deltaTime);
        }

        #endregion

    }

}