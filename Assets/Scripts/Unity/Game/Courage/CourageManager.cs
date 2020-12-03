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
        [SerializeField] private int _maxNightCourage;

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

        public int MaxNightCourage { get => _maxNightCourage; set => _maxNightCourage = value; }

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

        public void Initialize()
        {
            CurrentNightCourage = 1 + OverfilledNightCourage;
            OnInitialize?.Invoke(CurrentNightCourage, NeededNightCourage, MaxNightCourage);
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