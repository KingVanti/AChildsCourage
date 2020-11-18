﻿using System.Collections;
using UnityEngine;


namespace AChildsCourage.Game.Courage {
    public class CouragePickup : MonoBehaviour {

        #region Fields

#pragma warning disable 649
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private float speed;
        [SerializeField] private CouragePickupData testPickup;
        [SerializeField] private CouragePickupData testPickup2;
#pragma warning restore 649

        private int _value = 0;
        private CourageVariant variant;
        private string courageName = "";

        #endregion

        #region Properties

        public int Value { get { return _value; } set { _value = value; } }

        #endregion

        #region Methods

        /// <summary>
        /// REMOVE LATER
        /// </summary>
        private void Start() {
            SetCouragePickupData(testPickup);
        }

        public void SetCouragePickupData(CouragePickupData courageData) {

            variant = courageData.Variant;
            Value = courageData.Value;
            spriteRenderer.sprite = courageData.Sprite;
            spriteRenderer.transform.localScale = courageData.Scale;
            spriteRenderer.material.SetTexture("_Emission", courageData.Emission);
            courageName = courageData.CourageName;

        }

        #endregion

    }

}