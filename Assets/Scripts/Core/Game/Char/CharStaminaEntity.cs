using System;
using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using static AChildsCourage.M;

namespace AChildsCourage.Game.Char
{

    public class CharStaminaEntity : MonoBehaviour
    {

        [Pub] public event EventHandler<CharStaminaChangedEventArgs> OnStaminaChanged;

        [Pub] public event EventHandler OnStaminaRefreshed;

        [Header("Stats")]
        [SerializeField] private float maxStamina;
        [SerializeField] private float staminaRechargeCooldown;
        [SerializeField] private float refreshStamina;
        [SerializeField] private EnumArray<MovementState, float> staminaChangeRates;

        private float stamina;
        private float currentChangeRate;
        private bool isOnCooldown;


        private float Stamina
        {
            get => stamina;
            set
            {
                stamina = value;
                OnStaminaChanged?.Invoke(this, new CharStaminaChangedEventArgs(Stamina));

                if (HasNoStamina) StartCoolDown();
            }
        }

        private bool HasNoStamina => Stamina == 0;


        [Sub(nameof(SceneManagerEntity.OnSceneLoaded))] [UsedImplicitly]
        private void OnSceneLoaded(object _1, EventArgs _2)
        {
            Stamina = maxStamina;
            currentChangeRate = staminaChangeRates[MovementState.Standing];

            StartCoroutine(ContinuallyUpdateStamina());
        }

        [Sub(nameof(CharControllerEntity.OnMovementStateChanged))] [UsedImplicitly]
        private void OnMovementStateChanged(object _, MovementStateChangedEventArgs eventArgs) =>
            UpdateDrainRate(eventArgs.Current);

        private void UpdateDrainRate(MovementState movementState) =>
            currentChangeRate = staminaChangeRates[movementState];

        private IEnumerator ContinuallyUpdateStamina()
        {
            while (true)
            {
                if (!isOnCooldown) UpdateStamina();
                yield return null;
            }
        }

        private void UpdateStamina() =>
            Stamina = Stamina
                      .Map(Plus, currentChangeRate * Time.deltaTime)
                      .Map(Clamp, 0f, 100f);

        private void StartCoolDown()
        {
            isOnCooldown = true;
            Invoke(nameof(CompleteCooldown), staminaRechargeCooldown);
        }

        private void CompleteCooldown()
        {
            isOnCooldown = false;
            RefreshStamina();
        }

        private void RefreshStamina()
        {
            Stamina = refreshStamina;
            OnStaminaRefreshed?.Invoke(this, EventArgs.Empty);
        }

    }

}