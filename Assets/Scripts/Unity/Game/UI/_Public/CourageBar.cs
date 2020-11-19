using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AChildsCourage.Game.UI {
    public class CourageBar : MonoBehaviour {

        #region Fields

#pragma warning disable 649
        [SerializeField] private Image courageBarFill;
        [SerializeField] private TextMeshProUGUI courageCounterTextMesh;
#pragma warning restore 649

        #endregion

        #region Methods

        public void UpdateCourage(int newValue, int maxValue) {
            UpdateCourageBar(newValue, maxValue);
            UpdateCourageCounter(newValue, maxValue);
        }

        private void UpdateCourageBar(int newValue, int maxValue) {
            float newFillAmount = CustomMathModule.Map(newValue, 0, maxValue, 0, 1);
            StartCoroutine(FillLerp(newFillAmount));
            
        }

        private void UpdateCourageCounter(int newValue, int maxValue) {
            courageCounterTextMesh.text = newValue + " / " + maxValue;
        }

        IEnumerator FillLerp(float destination) {

            while(courageBarFill.fillAmount < destination) {
                courageBarFill.fillAmount = Mathf.Lerp(courageBarFill.fillAmount, destination, Time.deltaTime * 2);
                yield return new WaitForEndOfFrame();
            }

            
        }

        #endregion

    }

}