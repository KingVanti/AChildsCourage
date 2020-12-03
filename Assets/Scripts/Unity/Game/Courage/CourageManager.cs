using System;
using UnityEngine;
using UnityEngine.Events;

namespace AChildsCourage.Game.Courage
{

    public class CourageManager : MonoBehaviour
    {

        #region Fields

        private int currentNightCourage;
        private int neededNightCourage;

        public CourageChangedEvent onCourageChanged;
        public CourageChangedEvent onInitialize;
        public UnityEvent onCourageDepleted;
        public CanCollectCourageEvent onCouragePickupableChanged;

        #endregion

        #region Properties

        public int CurrentNightCourage
        {
            get => currentNightCourage;
            set
            {
                currentNightCourage = value;
                onCourageChanged?.Invoke(CurrentNightCourage, NeededNightCourage, MaxNightCourage);
                onCouragePickupableChanged?.Invoke(CurrentNightCourage >= MaxNightCourage);
            }
        }

        public int StartNightCourage { get; set; }

        public int MaxNightCourage { get; set; } = 25;

        public int NeededNightCourage => Mathf.CeilToInt((float) MaxNightCourage / 100 * 72.5f);

        public int OverfilledNightCourage
        {
            get
            {
                if (CurrentNightCourage > NeededNightCourage)
                    return CurrentNightCourage - NeededNightCourage;
                return 0;
            }
        }

        #endregion

        #region Methods

        private void Start()
        {
            Initialize(MaxNightCourage);
        }

        public void Initialize(int maxNightCourage)
        {
            MaxNightCourage = maxNightCourage;

            StartNightCourage = StartNightCourage + OverfilledNightCourage;

            if (StartNightCourage < 1)
                StartNightCourage = 1;

            onInitialize?.Invoke(StartNightCourage, NeededNightCourage, MaxNightCourage);
        }

        public void Add(CouragePickupEntity pickedUpCourage)
        {
            CurrentNightCourage += pickedUpCourage.Value;

            if (CurrentNightCourage > MaxNightCourage)
                CurrentNightCourage = MaxNightCourage;
        }

        public void Subtract(int value)
        {
            CurrentNightCourage -= value;

            if (CurrentNightCourage < 0)
            {
                CurrentNightCourage = 0;
                onCourageDepleted?.Invoke();
            }
        }

        #endregion

        #region Subclasses

        [Serializable]
        public class CourageChangedEvent : UnityEvent<int, int, int> { }

        [Serializable]
        public class CanCollectCourageEvent : UnityEvent<bool> { }

        #endregion

    }

}