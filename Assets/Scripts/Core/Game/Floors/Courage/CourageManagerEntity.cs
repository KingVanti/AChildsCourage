using System;
using AChildsCourage.Game.Char;
using AChildsCourage.Infrastructure;
using UnityEngine;

namespace AChildsCourage.Game.Floors.Courage
{

    public class CourageManagerEntity : MonoBehaviour
    {

        private const int NoCourage = 0;
        private const int BaseCourage = 1;

        [Pub] public event EventHandler<CollectedCourageChangedEventArgs> OnCollectedCourageChanged;

        [Pub] public event EventHandler OnCourageDepleted;


        [SerializeField] private int targetCourage;

        private int currentCourage;
        private int neededNightCourage;

        private int CurrentCourage
        {
            get => currentCourage;
            set
            {
                currentCourage = value.Clamp(0, targetCourage);
                OnCollectedCourageChanged?.Invoke(this, new CollectedCourageChangedEventArgs(CurrentCourage, CompletionPercent));

                if (HasNoCourage) OnCourageDepleted?.Invoke(this, EventArgs.Empty);
            }
        }

        private float CompletionPercent => CurrentCourage / (float) targetCourage;

        private bool HasNoCourage => CurrentCourage == NoCourage;


        [Sub(nameof(SceneManagerEntity.OnSceneLoaded))]
        private void OnSceneLoaded(object _1, EventArgs _2) =>
            CurrentCourage = BaseCourage;

        [Sub(nameof(CharControllerEntity.OnCouragePickedUp))]
        private void OnCouragePickedUp(object _, CouragePickedUpEventArgs eventArgs) =>
            AddCourage(eventArgs.Value);

        private void AddCourage(int amount) =>
            CurrentCourage += amount;

        [Sub(nameof(CharControllerEntity.OnReceivedDamage))]
        private void OnCharReceivedDamage(object _, CharDamageReceivedEventArgs eventArgs) =>
            LooseCourage(eventArgs.ReceivedDamage);

        private void LooseCourage(int amount) =>
            CurrentCourage -= amount;

    }

}