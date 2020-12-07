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
        [SerializeField] private TextMeshProUGUI courageCounterTextMesh;
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

        public void UpdateCourageCounter(int newValue, int neededValue)
        {
            courageCounterTextMesh.text = newValue + " / " + neededValue;
        }

        private IEnumerator FillLerp(float destination)
        {
            while (Math.Abs(courageBarFill.fillAmount - destination) > float.Epsilon)
            {
                courageBarFill.fillAmount = Mathf.MoveTowards(courageBarFill.fillAmount, destination, Time.deltaTime / 2.0f);
                yield return new WaitForEndOfFrame();
            }
        }

        #endregion

    }

}