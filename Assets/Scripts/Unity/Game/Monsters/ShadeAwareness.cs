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

#pragma warning disable 649

        [SerializeField] private float awarenessLossPerSecond;
        [SerializeField] private float baseAwarenessGainPerSecond;
        [SerializeField] private float primaryVisionMultiplier;

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

        private float CurrentAwarenessGain => baseAwarenessGainPerSecond * PrimaryVisionMultiplier;

        private float PrimaryVisionMultiplier => CurrentCharacterVisibility == Visibility.Primary ? primaryVisionMultiplier : 1;

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
            currentAwareness = MAwareness.LooseAwareness(currentAwareness, awarenessLossPerSecond * Time.deltaTime);
        }

        private void GainAwareness()
        {
            currentAwareness = MAwareness.LooseAwareness(currentAwareness, CurrentAwarenessGain * Time.deltaTime);
        }

        #endregion

    }

}