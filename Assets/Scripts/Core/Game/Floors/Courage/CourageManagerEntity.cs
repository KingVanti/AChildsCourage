using System;
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

        [Pub] public event EventHandler<CollectedCourageChangedEventArgs> OnCollectedCourageChanged;

        [Pub] public event EventHandler OnCourageDepleted;

#pragma warning disable 649

        [SerializeField] private int _maxNightCourage;

#pragma warning restore 649

        #endregion

        #region Properties

        public int CurrentNightCourage
        {
            get => _currentNightCourage;
            set
            {
                _currentNightCourage = value.Clamp(0, MaxNightCourage);
                OnCollectedCourageChanged?.Invoke(this, new CollectedCourageChangedEventArgs(CurrentNightCourage));

                if (CurrentNightCourage == 0) OnCourageDepleted?.Invoke(this, EventArgs.Empty);
            }
        }

        public int MaxNightCourage
        {
            get => _maxNightCourage;
            set => _maxNightCourage = value;
        }

        #endregion

        #region Methods

        public void Initialize() => CurrentNightCourage = 0;

        [Sub(nameof(CharControllerEntity.OnCouragePickedUp))]
        private void OnCouragePickedUp(object _, CouragePickedUpEventArgs eventArgs) => Add(eventArgs.Value);

        private void Add(int amount)
        {
            CurrentNightCourage += amount;

            if (CurrentNightCourage > MaxNightCourage) CurrentNightCourage = MaxNightCourage;
        }


        [Sub(nameof(CharControllerEntity.OnReceivedDamage))]
        private void OnCharReceivedDamage(object _, CharDamageReceivedEventArgs eventArgs) => Subtract(eventArgs.ReceivedDamage);

        private void Subtract(int amount) => CurrentNightCourage -= amount;


        public void GameLost() => OnCourageDepleted?.Invoke(this, EventArgs.Empty);

        #endregion

    }

}