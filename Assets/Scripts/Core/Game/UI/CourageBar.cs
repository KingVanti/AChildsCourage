using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AChildsCourage.Game.UI
{

    public class CourageBar : MonoBehaviour
    {

        #region Fields

#pragma warning disable 649
        [SerializeField] private Image courageBarFill;
        [SerializeField] private TextMeshProUGUI courageCounterCurrentTextMesh;
        [SerializeField] private TextMeshProUGUI courageCounterNeededTextMesh;
        [SerializeField] private Color32 textColor;
        [SerializeField] private Color fillColor;
#pragma warning restore 649

        #endregion

        #region Methods

        public void UpdateCourage(int newValue, int neededValue, int maxValue)
        {
            UpdateCourageBar(newValue, maxValue);
            UpdateCourageCounter(newValue, neededValue);
        }

        private void UpdateCourageBar(int newValue, int maxValue)
        {
            var newFillAmount = MCustomMath.Map(newValue, 0, maxValue, 0, 1);
            StartCoroutine(FillLerp(newFillAmount));
        }

        private void UpdateCourageCounter(int newValue, int neededValue)
        {
            if (newValue >= neededValue)
                courageCounterCurrentTextMesh.faceColor = textColor;
            else
                courageCounterCurrentTextMesh.faceColor = new Color(1, 1, 1, 1);

            courageCounterCurrentTextMesh.text = newValue.ToString();
            courageCounterNeededTextMesh.text = "/ " + neededValue;
        }

        private IEnumerator FillLerp(float destination)
        {
            while (Math.Abs(courageBarFill.fillAmount - destination) > float.Epsilon)
            {
                courageBarFill.fillAmount = Mathf.MoveTowards(courageBarFill.fillAmount, destination, Time.deltaTime / 4.0f);
                yield return new WaitForEndOfFrame();
            }

            UpdateCourageBarColor();
        }

        private void UpdateCourageBarColor() =>
            courageBarFill.color = new Color(
                                             MCustomMath.Map(courageBarFill.fillAmount, 0, 0.75f, 1, fillColor.r),
                                             MCustomMath.Map(courageBarFill.fillAmount, 0, 0.75f, 1, fillColor.g),
                                             MCustomMath.Map(courageBarFill.fillAmount, 0, 0.75f, 1, fillColor.b));

        #endregion

    }

}