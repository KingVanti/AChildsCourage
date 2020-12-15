﻿using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace AChildsCourage.Game.Courage
{
    public class CourageManager : MonoBehaviour
    {

        #region Fields

        private int _currentNightCourage;
        private int _neededNightCourage;
        private int _availableNightCourage;
        [SerializeField] private int _maxNightCourage;

        [SerializeField] private float messageTime;
        [SerializeField] private CanvasGroup messageCanvas;

        public CourageChangedEvent OnCourageChanged;
        public CourageChangedEvent OnInitialize;
        public UnityEvent OnCourageDepleted;
        public UnityEvent onCourageNotEnoughStarted;
        public UnityEvent onCourageNotEnoughCompleted;
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

                if(CurrentNightCourage + AvailableNightCourage < NeededNightCourage) {
                    StartCoroutine(CourageLoss());
                }

            }
        }

        public int MaxNightCourage { get => _maxNightCourage; set => _maxNightCourage = value; }

        public int NeededNightCourage => Mathf.CeilToInt((float) MaxNightCourage / 100 * 72.5f);

        public int AvailableNightCourage { get => _availableNightCourage; set => _availableNightCourage = value; }

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
            OnInitialize?.Invoke(CurrentNightCourage, NeededNightCourage, MaxNightCourage);
            CurrentNightCourage = 1 + OverfilledNightCourage;
            AvailableNightCourage = MaxNightCourage;
        }

        public void Add(CouragePickupEntity pickedUpCourage)
        {
            CurrentNightCourage += pickedUpCourage.Value;

            if (CurrentNightCourage > MaxNightCourage)
                CurrentNightCourage = MaxNightCourage;

            AvailableNightCourage -= pickedUpCourage.Value;
        }

        public void Subtract(int value)
        {
            CurrentNightCourage -= value;

            if (CurrentNightCourage >= 0)
                return;
            
            CurrentNightCourage = 0;
            OnCourageDepleted?.Invoke();
        }

        IEnumerator CourageLoss() {

            messageCanvas.alpha = 1;
            yield return new WaitForSeconds(messageTime);
            onCourageNotEnoughCompleted?.Invoke();

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