using System.Collections;
using UnityEngine;

namespace AChildsCourage.Game.Char
{

    public class StaminaEntity : MonoBehaviour
    {

        #region Fields

        [Header("Events")]
        public Events.Empty onStaminaDepleted;
        public Events.Empty onRefreshed;

#pragma warning  disable 649

        [Header("Stats")]
        public float stamina = 100;
        [SerializeField] private float staminaRecoveryRate = 1;
        [SerializeField] private float staminaDepletedCooldown = 5;
        [SerializeField] private float standingRate = 5;
        [SerializeField] private float walkingRate = 3;
        [SerializeField] private float sprintingRate = -5;

        private float staminaDrainRate = 1;

#pragma warning  restore 649

        private bool isSprinting;
        private bool isOnCooldown;

        #endregion

        #region Methods

        private void Start() => StartCoroutine(Sprint());

        public void StartSprinting() => isSprinting = true;

        public void StopSprinting() => isSprinting = false;

        public void SetStaminaDrainRate(MovementState movementState) {

            switch (movementState) {
                case MovementState.Sprinting:
                    staminaDrainRate = sprintingRate;
                    break;
                case MovementState.Walking:
                    staminaDrainRate = walkingRate;
                    break;
                case MovementState.Standing:
                    staminaDrainRate = standingRate;
                    break;
            }

        }

        private IEnumerator Sprint()
        {
            while (true)
            {
                if (!isOnCooldown)
                {
                    if (isSprinting && stamina >= 0)
                    {
                        stamina = Mathf.MoveTowards(stamina, 0, staminaDrainRate * Time.deltaTime);

                        if (stamina <= 0.05f)
                        {
                            StartCoroutine(Cooldown());
                            onStaminaDepleted?.Invoke();
                        }

                        yield return null;
                    }

                    if (!isSprinting && stamina <= 100)
                    {
                        stamina = Mathf.MoveTowards(stamina, 100, staminaRecoveryRate * Time.deltaTime);
                        yield return null;
                    }
                }

                yield return null;
            }
        }

        private IEnumerator Cooldown()
        {
            isOnCooldown = true;
            StopSprinting();
            yield return new WaitForSeconds(staminaDepletedCooldown);
            isOnCooldown = false;
            stamina = 15;
            onRefreshed?.Invoke();
        }

        #endregion

    }

}