using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AChildsCourage.Game.Player {
    public class Stamina : MonoBehaviour {

        #region Fields
        [Header("Stats")]
        [SerializeField] private float stamina = 100;
        [SerializeField] private float staminaRecoveryRate = 1;
        [SerializeField] private float staminaDrainRate = 2;
        [SerializeField] private float staminaDepletedCooldown = 5;

        private bool isSprinting = false;
        private bool isOnCooldown = false;

        [Header("Events")]
        public UnityEvent onStaminaDepleted;
        public UnityEvent onRefreshed;

        #endregion

        #region Methods

        private void Start() {
            StartCoroutine(Sprint());
        }

        public void StartSprinting() {
            isSprinting = true;
        }

        public void StopSprinting() {
            isSprinting = false;
        }

        private IEnumerator Sprint() {

            while (true) {

                if (!isOnCooldown) {

                    if (isSprinting && stamina >= 0) {
                        stamina = Mathf.MoveTowards(stamina, 0, staminaDrainRate * Time.deltaTime);

                        if(stamina <= 0.05f) {
                            StartCoroutine(Cooldown());
                            onStaminaDepleted?.Invoke();
                        }

                        yield return null;
                    }

                    if (!isSprinting && stamina <= 100) {
                        stamina = Mathf.MoveTowards(stamina, 100, staminaRecoveryRate * Time.deltaTime);
                        yield return null;
                    }

                }

                yield return null;

            }

        }

        private IEnumerator Cooldown() {
            isOnCooldown = true;
            yield return new WaitForSeconds(staminaDepletedCooldown);
            isOnCooldown = false;
            onRefreshed?.Invoke();
        }

        #endregion

    }

}
