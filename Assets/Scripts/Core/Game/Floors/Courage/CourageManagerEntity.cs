﻿using System;
using AChildsCourage.Game.Char;
using JetBrains.Annotations;
using UnityEngine;
using static AChildsCourage.M;

namespace AChildsCourage.Game.Floors.Courage
{

    public class CourageManagerEntity : MonoBehaviour
    {

        private const int BaseCourage = 0;

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
                currentCourage = value.Map(Clamp, 0, targetCourage);
                OnCollectedCourageChanged?.Invoke(this, new CollectedCourageChangedEventArgs(CompletionPercent));
            }
        }

        private float CompletionPercent => CurrentCourage / (float) targetCourage;


        [Sub(nameof(GameManager.OnSceneBecameVisible))] [UsedImplicitly]
        private void OnStartGame(object _1, EventArgs _2) =>
            CurrentCourage = BaseCourage;

        [Sub(nameof(CharControllerEntity.OnCouragePickedUp))] [UsedImplicitly]
        private void OnCouragePickedUp(object _, CouragePickedUpEventArgs eventArgs) =>
            AddCourage(courageValues[eventArgs.Variant]);

        private void AddCourage(int amount) =>
            CurrentCourage += amount;

    }

}