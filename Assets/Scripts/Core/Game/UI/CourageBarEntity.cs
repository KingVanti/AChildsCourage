using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AChildsCourage.Game.UI {

    public class CourageBarEntity : MonoBehaviour {

        #region Fields

#pragma warning disable 649
        [SerializeField] private Image courageBarFill;
        [SerializeField] private TextMeshProUGUI courageCounterCurrentTextMesh;
        [SerializeField] private TextMeshProUGUI courageCounterNeededTextMesh;
        [SerializeField] private Color32 textColor;
#pragma warning restore 649

        #endregion

        #region Methods

        public void UpdateCourage(int newValue, int maxValue) {
            UpdateCourageBar(newValue, maxValue);
            UpdateCourageCounter(newValue, maxValue);
        }

        private void UpdateCourageBar(int newValue, int maxValue) {
            var newFillAmount = MCustomMath.Map(newValue, 0, maxValue, 0, 1);
            StartCoroutine(FillLerp(newFillAmount));
        }

        private void UpdateCourageCounter(int newValue, int maxValue) {

            if (newValue >= maxValue) {
                courageCounterCurrentTextMesh.faceColor = textColor;
                courageCounterNeededTextMesh.faceColor = textColor;
            } else {
                courageCounterCurrentTextMesh.faceColor = new Color(1, 1, 1, 1);
                courageCounterNeededTextMesh.faceColor = new Color(1, 1, 1, 1);
            }

            courageCounterCurrentTextMesh.text = newValue.ToString();
            courageCounterNeededTextMesh.text = "/ " + maxValue;

        }

        private IEnumerator FillLerp(float destination) {

            while (Math.Abs(courageBarFill.fillAmount - destination) > float.Epsilon) {
                courageBarFill.fillAmount = Mathf.MoveTowards(courageBarFill.fillAmount, destination, Time.deltaTime / 4.0f);
                yield return new WaitForEndOfFrame();
            }

        }

        #endregion

    }

}