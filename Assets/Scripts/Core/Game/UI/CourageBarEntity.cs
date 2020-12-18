using System;
using System.Collections;
using AChildsCourage.Game.Floors.Courage;
using AChildsCourage.Infrastructure;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AChildsCourage.Game.UI
{

    public class CourageBarEntity : MonoBehaviour
    {

        #region Fields

#pragma warning disable 649
        
        [SerializeField] private Image courageBarFill;
        [SerializeField] private TextMeshProUGUI courageCounterTextMesh;
        [SerializeField] private Color32 textColor;

        [FindInScene] private CourageManagerEntity courageManager;
        
#pragma warning restore 649

        #endregion

        #region Properties

        private int TotalToCollect => courageManager.MaxNightCourage;

        #endregion
        
        #region Methods

        [Sub(nameof(CourageManagerEntity.OnCollectedCourageChanged))]
        private void OnCollectedCourageChanged(object _, CollectedCourageChangedEventArgs eventArgs)
        {
            UpdateCourageBar(eventArgs.Collected);
            UpdateCourageCounter(eventArgs.Collected);
        }

        private void UpdateCourageBar(int newValue)
        {
            var newFillAmount = MCustomMath.Map(newValue, 0, TotalToCollect, 0, 1);
            StartCoroutine(FillLerp(newFillAmount));
        }

        private void UpdateCourageCounter(int newValue)
        {
            if (newValue >= TotalToCollect)
                courageCounterTextMesh.faceColor = textColor;
            else
                courageCounterTextMesh.faceColor = new Color(1, 1, 1, 1);

            courageCounterTextMesh.text = newValue + " / " + TotalToCollect;
        }

        private IEnumerator FillLerp(float destination)
        {
            while (Math.Abs(courageBarFill.fillAmount - destination) > float.Epsilon)
            {
                courageBarFill.fillAmount = Mathf.MoveTowards(courageBarFill.fillAmount, destination, Time.deltaTime / 4.0f);
                yield return new WaitForEndOfFrame();
            }
        }

        #endregion

    }

}