using System;
using System.Collections;
using Appccelerate.EventBroker;
using Ninject.Extensions.Unity;
using UnityEngine;
using UnityEngine.Events;

namespace AChildsCourage.Game.Courage
{

    [UseDi]
    public class CourageManager : MonoBehaviour
    {

        #region Fields

        private int _currentNightCourage;
        private int _neededNightCourage;

#pragma warning disable 649

        [SerializeField] private int _maxNightCourage;
        [SerializeField] private float messageTime;
        [SerializeField] private CanvasGroup messageCanvas;

#pragma warning restore 649

        [Header("Events")] public CourageChangedEvent OnCourageChanged;
        public CourageChangedEvent OnInitialize;
        public UnityEvent OnCourageDepleted;
        public UnityEvent onCourageNotEnoughStarted;
        public UnityEvent onCourageNotEnoughCompleted;
        public CanCollectCourageEvent OnCouragePickupableChanged;

        [EventPublication(nameof(OnPlayerLose))]
        public event EventHandler OnPlayerLose;

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

                if (CurrentNightCourage + AvailableNightCourage < NeededNightCourage) StartCoroutine(CourageLoss());
            }
        }

        public int MaxNightCourage
        {
            get => _maxNightCourage;
            set => _maxNightCourage = value;
        }

        public int NeededNightCourage => Mathf.CeilToInt((float) MaxNightCourage / 100 * 72.5f);

        public int AvailableNightCourage { get; set; }

        public int OverfilledNightCourage
        {
            get
            {
                if (CurrentNightCourage > NeededNightCourage) return CurrentNightCourage - NeededNightCourage;
                return 0;
            }
        }

        #endregion

        #region Methods

        public void Initialize()
        {
            OnInitialize?.Invoke(CurrentNightCourage, NeededNightCourage, MaxNightCourage);
            AvailableNightCourage = MaxNightCourage;
            CurrentNightCourage = 1 + OverfilledNightCourage;
        }

        public void Add(CouragePickupEntity pickedUpCourage)
        {
            CurrentNightCourage += pickedUpCourage.Value;

            if (CurrentNightCourage > MaxNightCourage) CurrentNightCourage = MaxNightCourage;

            AvailableNightCourage -= pickedUpCourage.Value;
        }

        public void Subtract(int value)
        {
            CurrentNightCourage -= value;

            if (CurrentNightCourage >= 0) return;

            CurrentNightCourage = 0;
            OnCourageDepleted?.Invoke();
        }

        public void GameLost() => OnPlayerLose?.Invoke(this, EventArgs.Empty);

        private IEnumerator CourageLoss()
        {
            messageCanvas.alpha = 1;
            yield return new WaitForSeconds(messageTime);
            onCourageNotEnoughCompleted?.Invoke();
            GameLost();
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