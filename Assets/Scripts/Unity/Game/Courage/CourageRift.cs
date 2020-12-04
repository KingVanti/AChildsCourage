using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AChildsCourage.Game.Courage {
    public class CourageRift : MonoBehaviour {

        [SerializeField] private SpriteRenderer sr;
        [SerializeField] private CourageManager courageManager;
        [SerializeField] private Sprite[] riftStageSprites = new Sprite[5];

        private int currentStage = 0;
        private int threshold = 0;
        private int needed = 0;
        private int lastCourageStageCount = 2;

        public void SetRiftStats(int currentCourage, int neededCourage, int maxCourage) {
            sr.sprite = riftStageSprites[currentStage];
            needed = neededCourage;
            threshold = Mathf.RoundToInt(needed / riftStageSprites.Length);
        }

        public void UpdateStage(int currentCourage, int neededCourage, int maxCourage) {

            if(currentCourage >= lastCourageStageCount + threshold) {
                currentStage++;
                sr.sprite = riftStageSprites[currentStage];
                lastCourageStageCount = currentCourage;
            }

        }



    }

}

