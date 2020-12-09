using UnityEngine;
using static AChildsCourage.Game.Floors.MFloor;
using static AChildsCourage.Game.MChunkPosition;
using UnityEngine.Events;

using static AChildsCourage.MCustomMath;
using Ninject.Extensions.Unity;
using Appccelerate.EventBroker;
using System;

namespace AChildsCourage.Game.Courage {

    [UseDi]
    public class CourageRift : MonoBehaviour {
        #region Fields

#pragma warning disable 649
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private CourageManager courageManager;
        [SerializeField] private Sprite[] riftStageSprites = new Sprite[5];
        [SerializeField] private ParticleSystem riftParticleSystem;
#pragma warning restore 649

        private int currentStage = 0;
        private int needed = 0;
        private int threshold = 0;
        private int[] stageThresholds = new int[5];
        private int courage = 0;
        private int lastCourageCount = 0;

        [Header("Events")]
        public UnityEvent onRiftEntered;

        [EventPublication(nameof(OnPlayerWin))]
        public event EventHandler OnPlayerWin;

        #endregion

        #region Methods

        public void OnFloorBuilt(Floor floor) {
            transform.position = GetChunkCenter(floor.EndRoomChunkPosition).ToVector3() + new Vector3(0.5f, 0.5f, 0);
        }

        public void SetRiftStats(int currentCourage, int neededCourage, int maxCourage) {
            spriteRenderer.sprite = riftStageSprites[currentStage];
            needed = neededCourage;
            threshold = Mathf.RoundToInt(needed / ((float)riftStageSprites.Length - 1));

            for (int i = 0; i < stageThresholds.Length; i++) {
                stageThresholds[i] = threshold * i;
            }

            courage = currentCourage;

        }

        public void UpdateStage(int currentCourage, int neededCourage, int maxCourage) {

            lastCourageCount = courage;
            courage = currentCourage;
            UpdateParticleSystem();

            if (lastCourageCount > courage) {

                if (courage >= stageThresholds[currentStage])
                    return;

                if (courage < 0) {
                    currentStage = stageThresholds[0];
                } else {
                    currentStage--;
                }

            } else {

                if (courage >= needed) {
                    currentStage = stageThresholds.Length - 1;
                } else {

                    if (courage < stageThresholds[currentStage + 1])
                        return;

                    currentStage++;
                }
            }

            spriteRenderer.sprite = riftStageSprites[currentStage];

        }

        private void UpdateParticleSystem() {

            var emission = riftParticleSystem.emission;

            float newRateOverTime = Map(courage, 0f, needed, 2f, 20f);
            emission.rateOverTime = newRateOverTime;

        }


        private void OnTriggerEnter2D(Collider2D collision) {

            if (collision.CompareTag("Player")) {
                if (currentStage == 4) {
                    OnPlayerWin?.Invoke(this, EventArgs.Empty);
                    onRiftEntered?.Invoke();
                }
            }

        }

        #endregion

    }

}