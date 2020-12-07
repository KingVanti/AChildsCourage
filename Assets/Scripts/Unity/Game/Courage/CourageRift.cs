using UnityEngine;
using static AChildsCourage.Game.Floors.MFloor;
using static AChildsCourage.Game.MChunkPosition;
using UnityEngine.Events;

namespace AChildsCourage.Game.Courage
{

    public class CourageRift : MonoBehaviour
    {
        #region Fields

#pragma warning disable 649
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private CourageManager courageManager;
        [SerializeField] private Sprite[] riftStageSprites = new Sprite[5];
        [SerializeField] private ParticleSystem courageCollectParticleSystem;
#pragma warning restore 649

        private int currentStage = 0;
        private int lastCourageStageCount = 0;
        private int needed = 0;
        private int threshold = 0;

        [Header("Events")]
        public UnityEvent OnRiftEntered;

        #endregion

        #region Methods


        public void OnFloorBuilt(Floor floor)
        {
            transform.position = GetChunkCenter(floor.EndRoomChunkPosition).ToVector3() + new Vector3(0.5f, 0.5f, 0);
        }


        public void SetRiftStats(int currentCourage, int neededCourage, int maxCourage)
        {
            spriteRenderer.sprite = riftStageSprites[currentStage];
            needed = neededCourage;
            threshold = Mathf.RoundToInt(needed / ((float)riftStageSprites.Length - 1));
        }

        public void UpdateStage(int currentCourage, int neededCourage, int maxCourage)
        {
            if (currentCourage < lastCourageStageCount + threshold)
                return;

            currentStage++;
            spriteRenderer.sprite = riftStageSprites[currentStage];
            lastCourageStageCount = currentCourage;

        }

        private void OnTriggerEnter2D(Collider2D collision) {

            if (collision.tag == "Player") {
                if (currentStage == 4) {
                    OnRiftEntered?.Invoke();
                }
            }

        }

        #endregion

    }

}