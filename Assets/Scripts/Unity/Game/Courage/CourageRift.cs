﻿using UnityEngine;

namespace AChildsCourage.Game.Courage
{

    public class CourageRift : MonoBehaviour
    {

        [SerializeField] private SpriteRenderer sr;
        [SerializeField] private CourageManager courageManager;
        [SerializeField] private Sprite[] riftStageSprites = new Sprite[5];

        private int currentStage;
        private int lastCourageStageCount = 2;
        private int needed;
        private int threshold;

        public void SetRiftStats(int currentCourage, int neededCourage, int maxCourage)
        {
            sr.sprite = riftStageSprites[currentStage];
            needed = neededCourage;
            threshold = Mathf.RoundToInt(needed / (float) riftStageSprites.Length);
        }

        public void UpdateStage(int currentCourage, int neededCourage, int maxCourage)
        {
            if (currentCourage < lastCourageStageCount + threshold)
                return;

            currentStage++;
            sr.sprite = riftStageSprites[currentStage];
            lastCourageStageCount = currentCourage;
        }

    }

}