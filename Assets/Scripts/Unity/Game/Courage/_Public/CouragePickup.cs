using System.Collections;
using UnityEngine;


namespace AChildsCourage.Game.Courage {
    public class CouragePickup : MonoBehaviour {

        #region Fields

#pragma warning disable 649
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private CouragePickupData testPickup;
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
            SetCouragePickupData(testPickup);
        }

        public void SetCouragePickupData(CouragePickupData courageData) {

            variant = courageData.Variant;
            value = courageData.Value;
            spriteRenderer.sprite = courageData.Sprite;
            spriteRenderer.transform.localScale = courageData.Scale;
            courageName = courageData.CourageName;

        }

        #endregion

    }

}
