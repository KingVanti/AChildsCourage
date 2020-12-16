using System.Collections;
using UnityEngine;

namespace AChildsCourage.Game.Char {

    public class StaminaEntity : MonoBehaviour {

        #region Fields

        [Header("Events")]
        public Events.Empty onStaminaDepleted;
        public Events.Empty onRefreshed;

#pragma warning  disable 649

        [Header("Stats")]
        public float stamina = 100;
        [SerializeField] private float staminaDepletedCooldown;
        [SerializeField] private EnumArray<MovementState, float> staminaRates;
        [SerializeField] private float recoveredStaminaAmount;

#pragma warning restore 649

        private float staminaDrainRate = 1f;
        private bool isOnCooldown;

        #endregion

        #region Methods

        private void Start() => StartCoroutine(Sprint());

        public void SetStaminaDrainRate(MovementState movementState)
        {
            staminaDrainRate = staminaRates[movementState];
        }

        private IEnumerator Sprint() {

            while (true) {
                if (!isOnCooldown) {

                    stamina = stamina.Plus(staminaDrainRate * Time.deltaTime).Clamp(0, 100);

                    if (stamina <= 0.05f) {
                        StartCoroutine(Cooldown());
                        onStaminaDepleted?.Invoke();
                    }

                    yield return null;

                }

                yield return null;
            }
        }

        private IEnumerator Cooldown() {
            isOnCooldown = true;
            yield return new WaitForSeconds(staminaDepletedCooldown);
            isOnCooldown = false;
            RefreshStamina(recoveredStaminaAmount);
        }

        private void RefreshStamina(float recoveredStaminaAmount) {
            stamina = recoveredStaminaAmount;
            onRefreshed?.Invoke();
        }

        #endregion

    }

}