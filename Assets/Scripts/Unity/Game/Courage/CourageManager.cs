using System;
using UnityEngine;
using UnityEngine.Events;

namespace AChildsCourage.Game.Courage
{

    public class CourageManager : MonoBehaviour
    {

        #region Fields

        private int _currentNightCourage;
        private int _neededNightCourage;

        public CourageChangedEvent OnCourageChanged;
        public CourageChangedEvent OnInitialize;
        public UnityEvent OnCourageDepleted;
        public CanCollectCourageEvent OnCouragePickupableChanged;

        #endregion

        #region Properties

        public int CurrentNightCourage
        {
            get => _currentNightCourage;
            set
            {
                _currentNightCourage = value;
                OnCourageChanged?.Invoke(CurrentNightCourage, NeededNightCourage, MaxNightCourage);
                OnCouragePickupableChanged?.Invoke(CurrentNightCourage >= MaxNightCourage);
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

            StartNightCourage += OverfilledNightCourage;

            if (StartNightCourage < 1)
                StartNightCourage = 1;

            OnInitialize?.Invoke(StartNightCourage, NeededNightCourage, MaxNightCourage);
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
                OnCourageDepleted?.Invoke();
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