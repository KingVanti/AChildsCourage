using System.Collections;
using UnityEngine;


namespace AChildsCourage.Game.Courage {
    public class CouragePickup : MonoBehaviour {

        #region Fields

#pragma warning disable 649
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private CouragePickupData testPickup;
#pragma warning restore 649

        private int value;
        private int id;

        #endregion

        #region Methods

        /// <summary>
        /// REMOVE LATER
        /// </summary>
        private void Start() {
            SetCourageData(testPickup);
        }

        public void SetCourageData(CouragePickupData courageData) {

            id = courageData.Id;
            value = courageData.Value;
            spriteRenderer.sprite = courageData.Sprite;
            spriteRenderer.transform.localScale = courageData.Scale;

        }

        #endregion

    }

}
