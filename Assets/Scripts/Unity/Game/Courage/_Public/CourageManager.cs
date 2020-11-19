using System;
using UnityEngine;
using UnityEngine.Events;

namespace AChildsCourage.Game.Courage {

    public class CourageManager : MonoBehaviour {

        #region Fields

        private int _currentNightCourage;
        private int _startNightCourage;
        private int _neededNightCourage;

        public CourageChangedEvent OnCourageChanged;

        #endregion

        #region Properties

        public int CurrentNightCourage {
            get { return _currentNightCourage; }
            set {
                _currentNightCourage = value;
                OnCourageChanged?.Invoke(_currentNightCourage, MaxNightCourage);
            }
        }

        public int StartNightCourage {
            get { return _startNightCourage; }
            set { _startNightCourage = value; }
        }

        public int MaxNightCourage { get; set; } = 10;

        public int NeededNightCourage {
            get { return Mathf.CeilToInt((float)MaxNightCourage / 100 * 72.5f); }
        }

        public int OverfilledNightCourage {
            get {
                if (CurrentNightCourage > NeededNightCourage) {
                    return CurrentNightCourage - NeededNightCourage;
                } else {
                    return 0;
                }
            }
        }

        #endregion

        #region Methods

        public void Add(int value) {
            CurrentNightCourage += value;

            if(CurrentNightCourage > MaxNightCourage) {
                CurrentNightCourage = MaxNightCourage;
            }

            if (OverfilledNightCourage > 0)
                Debug.Log(OverfilledNightCourage);
        }

        public void Subtract(int value) {
            CurrentNightCourage -= value;

            if (CurrentNightCourage < 0) {
                CurrentNightCourage = 0;
            }

        }

        #endregion

        #region Subclasses

        [Serializable]
        public class CourageChangedEvent : UnityEvent<int, int> { }

        #endregion

    }
}
