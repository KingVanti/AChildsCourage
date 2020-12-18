using System;
using AChildsCourage.Infrastructure;
using UnityEngine;
using UnityEngine.Video;
using static AChildsCourage.Game.Floors.MFloor;
using static AChildsCourage.Game.MChunkPosition;
using static AChildsCourage.Game.MTilePosition;
using static AChildsCourage.MCustomMath;

namespace AChildsCourage.Game.Floors.Courage
{

    public class CourageRiftEntity : MonoBehaviour
    {

        #region Properties

        private int TotalToCollect => courageManager.MaxNightCourage;

        #endregion

        [Pub] public event EventHandler OnCharWin;

        #region Fields

        [Header("Events")]
        public Events.Empty onRiftEntered;

#pragma warning disable 649

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Sprite[] riftStageSprites = new Sprite[5];
        [SerializeField] private ParticleSystem riftParticleSystem;

        [FindInScene] private CourageManagerEntity courageManager;

#pragma warning restore 649

        private int currentStage;
        private readonly int[] stageThresholds = new int[5];
        private int lastCourageCount;

        #endregion

        #region Methods

        [Sub(nameof(FloorRecreatorEntity.OnFloorRecreated))]
        private void OnFloorRecreated(object _, FloorRecreatedEventArgs eventArgs) =>
            transform.position = GetEndRoomCenter(eventArgs.Floor);

        private static Vector2 GetEndRoomCenter(Floor floor) => floor.EndRoomChunkPosition
                                                                     .Map(GetCenter)
                                                                     .Map(GetTileCenter);

        private void Awake() => SetThresholds();

        private void SetThresholds()
        {
            var threshold = Mathf.RoundToInt(TotalToCollect / ((float) riftStageSprites.Length - 1));

            for (var i = 0; i < stageThresholds.Length; i++) stageThresholds[i] = threshold * i;
        }



        [Sub(nameof(CourageManagerEntity.OnCollectedCourageChanged))]
        private void OnCollectedCourageChanged(object _, CollectedCourageChangedEventArgs eventArgs) => UpdateStage(eventArgs.Collected);
        
        private void UpdateStage(int courage)
        {
            lastCourageCount = courage;
            UpdateParticleSystem(courage);

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
                if (courage >= TotalToCollect)
                    currentStage = stageThresholds.Length - 1;
                else
                {
                    if (courage < stageThresholds[currentStage + 1]) return;

                    currentStage++;
                }
            }

            spriteRenderer.sprite = riftStageSprites[currentStage];
        }

        private void UpdateParticleSystem(int courage)
        {
            var emission = riftParticleSystem.emission;

            var newRateOverTime = Map(courage, 0f, TotalToCollect, 2f, 20f);
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