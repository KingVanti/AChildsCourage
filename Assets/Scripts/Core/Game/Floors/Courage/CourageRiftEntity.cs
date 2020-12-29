using System;
using AChildsCourage.Infrastructure;
using UnityEngine;
using static AChildsCourage.Game.Floors.MFloor;
using static AChildsCourage.Game.MChunkPosition;
using static AChildsCourage.Game.MTilePosition;
using static AChildsCourage.MCustomMath;

namespace AChildsCourage.Game.Floors.Courage
{

    public class CourageRiftEntity : MonoBehaviour
    {

        [Pub] public event EventHandler OnCharEnteredRift;

        #region Properties

        private int TotalToCollect => courageManager.MaxNightCourage;

        #endregion

        #region Fields

        [SerializeField] private Sprite[] riftStageSprites = new Sprite[5];
        
        [FindComponent] private SpriteRenderer spriteRenderer;
        [FindComponent(ComponentFindMode.OnChildren)]
        private ParticleSystem riftParticleSystem;

        [FindInScene] private CourageManagerEntity courageManager;
        
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

        [Sub(nameof(SceneManagerEntity.OnSceneLoaded))]
        private void OnSceneLoaded(object _1, EventArgs _2) => SetThresholds();

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

            OnCharEnteredRift?.Invoke(this, EventArgs.Empty);
        }

        #endregion

    }

}