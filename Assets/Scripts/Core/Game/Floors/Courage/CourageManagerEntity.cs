using System;
using AChildsCourage.Game.Char;
using UnityEngine;

namespace AChildsCourage.Game.Floors.Courage
{

    public class CourageManagerEntity : MonoBehaviour
    {

        private const int NoCourage = 0;
        private const int BaseCourage = 1;

        [Pub] public event EventHandler<CollectedCourageChangedEventArgs> OnCollectedCourageChanged;


        [SerializeField] private int targetCourage;
        [SerializeField] private EnumArray<CourageVariant, int> courageValues;

        private int currentCourage;
        private int neededNightCourage;

        private int CurrentCourage
        {
            get => currentCourage;
            set
            {
                currentCourage = value.Clamp(0, targetCourage);
                OnCollectedCourageChanged?.Invoke(this, new CollectedCourageChangedEventArgs(CurrentCourage, CompletionPercent));
            }
        }

        private float CompletionPercent => CurrentCourage / (float) targetCourage;


        [Sub(nameof(SceneManagerEntity.OnSceneLoaded))]
        private void OnSceneLoaded(object _1, EventArgs _2) =>
            CurrentCourage = BaseCourage;

        [Sub(nameof(CharControllerEntity.OnCouragePickedUp))]
        private void OnCouragePickedUp(object _, CouragePickedUpEventArgs eventArgs) =>
            AddCourage(courageValues[eventArgs.Variant]);

        private void AddCourage(int amount) =>
            CurrentCourage += amount;
        
    }

}