using System.Collections;
using UnityEngine;


namespace AChildsCourage.Game.Courage {
    public class CouragePickup : MonoBehaviour {

        #region Fields

#pragma warning disable 649
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private CouragePickupData testPickup;
        [SerializeField] private CouragePickupData testPickup2;
#pragma warning restore 649

        private int value = 0;
        private CourageVariant variant;
        private string courageName = "";

        #endregion

        #region Methods

        /// <summary>
        /// REMOVE LATER
        /// </summary>
        private void Start() {

            float rand = Random.Range(0f, 1f);
            Debug.Log(rand);

            if (rand > 0.2f) {
                SetCouragePickupData(testPickup2);
            }
            else{
                SetCouragePickupData(testPickup);
            }

        }

        public void SetCouragePickupData(CouragePickupData courageData) {

            variant = courageData.Variant;
            value = courageData.Value;
            spriteRenderer.sprite = courageData.Sprite;
            spriteRenderer.transform.localScale = courageData.Scale;
            spriteRenderer.material.SetTexture("_Emission", courageData.Emission);
            courageName = courageData.CourageName;

        }

        #endregion

    }

}
