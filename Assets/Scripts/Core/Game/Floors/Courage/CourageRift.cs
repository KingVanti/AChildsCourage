using System;
using Appccelerate.EventBroker;
using Ninject.Extensions.Unity;
using UnityEngine;
using static AChildsCourage.Game.Floors.MFloor;
using static AChildsCourage.Game.MChunkPosition;
using static AChildsCourage.Game.MTilePosition;
using static AChildsCourage.MCustomMath;

namespace AChildsCourage.Game.Courage
{

    [UseDi]
    public class CourageRift : MonoBehaviour
    {

        [EventPublication(nameof(OnCharWin))]
        public event EventHandler OnCharWin;

        #region Fields

        [Header("Events")]
        public Events.Empty onRiftEntered;

#pragma warning disable 649
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private CourageManager courageManager;
        [SerializeField] private Sprite[] riftStageSprites = new Sprite[5];
        [SerializeField] private ParticleSystem riftParticleSystem;
#pragma warning restore 649

        private int currentStage;
        private int needed;
        private int threshold;
        private readonly int[] stageThresholds = new int[5];
        private int courage;
        private int lastCourageCount;

        #endregion

        #region Methods

        public void OnFloorBuilt(Floor floor) =>
            transform.position = GetEndRoomCenter(floor);

        private static Vector2 GetEndRoomCenter(Floor floor) => floor.EndRoomChunkPosition
                                                                     .Map(GetChunkCenter)
                                                                     .Map(GetTileCenter);

        public void SetRiftStats(int currentCourage, int neededCourage, int maxCourage)
        {
            spriteRenderer.sprite = riftStageSprites[currentStage];
            needed = neededCourage;
            threshold = Mathf.RoundToInt(needed / ((float) riftStageSprites.Length - 1));

            for (var i = 0; i < stageThresholds.Length; i++) stageThresholds[i] = threshold * i;

            courage = currentCourage;
        }

        public void UpdateStage(int currentCourage, int neededCourage, int maxCourage)
        {
            lastCourageCount = courage;
            courage = currentCourage;
            UpdateParticleSystem();

            if (lastCourageCount > courage)
            {
                if (courage >= stageThresholds[currentStage]) return;

                if (courage < 0)
                    currentStage = stageThresholds[0];
                else
                    currentStage--;
            }
            else
            {
                if (courage >= needed)
                    currentStage = stageThresholds.Length - 1;
                else
                {
                    if (courage < stageThresholds[currentStage + 1]) return;

                    currentStage++;
                }
            }

            spriteRenderer.sprite = riftStageSprites[currentStage];
        }

        private void UpdateParticleSystem()
        {
            var emission = riftParticleSystem.emission;

            var newRateOverTime = Map(courage, 0f, needed, 2f, 20f);
            emission.rateOverTime = newRateOverTime;
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag(EntityTags.Char) || currentStage != 4) return;

            OnCharWin?.Invoke(this, EventArgs.Empty);
            onRiftEntered?.Invoke();
        }

        #endregion

    }

}