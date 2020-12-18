using System;
using System.Collections;
using AChildsCourage.Game.Char;
using AChildsCourage.Infrastructure;
using UnityEngine;

namespace AChildsCourage.Game.Floors.Courage
{

    public class CourageManagerEntity : MonoBehaviour
    {

        #region Fields

        private int _currentNightCourage;
        private int _neededNightCourage;

#pragma warning disable 649

        [SerializeField] private int _maxNightCourage;
        [SerializeField] private float messageTime;
        [SerializeField] private CanvasGroup messageCanvas;

#pragma warning restore 649

        [Header("Events")]
        public CourageEvents.CourageChanged OnCourageChanged;
        public CourageEvents.CourageChanged OnInitialize;
        public Events.Empty OnCourageDepleted;
        public Events.Bool OnCouragePickupableChanged;

        [Pub] public event EventHandler OnCharLose;

        #endregion

        #region Properties

        public int CurrentNightCourage
        {
            get => _currentNightCourage;
            set
            {
                _currentNightCourage = value;
                OnCourageChanged?.Invoke(CurrentNightCourage, MaxNightCourage);
                OnCouragePickupableChanged?.Invoke(CurrentNightCourage >= MaxNightCourage);
            }
        }

        public int MaxNightCourage
        {
            get => _maxNightCourage;
            set => _maxNightCourage = value;
        }


        #endregion

        #region Methods

        public void Initialize()
        {
            OnInitialize?.Invoke(CurrentNightCourage, MaxNightCourage);
            CurrentNightCourage = 0;
        }
        
        [Sub(nameof(CharControllerEntity.OnCouragePickedUp))]
        public void Add(object _, CouragePickedUpEventArgs eventArgs)
        {
            CurrentNightCourage += eventArgs.Value;

            if (CurrentNightCourage > MaxNightCourage) CurrentNightCourage = MaxNightCourage;
        }

        public void Subtract(int value)
        {
            CurrentNightCourage -= value;

            if (CurrentNightCourage >= 0) return;

            CurrentNightCourage = 0;
            OnCourageDepleted?.Invoke();
        }

        public void GameLost() => OnCharLose?.Invoke(this, EventArgs.Empty);

        #endregion

    }

}