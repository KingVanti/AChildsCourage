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

        public void UpdateCourageBar(int newValue, int maxValue) {
            courageBarFill.fillAmount = CustomMathModule.Map(newValue, 0, maxValue, 0, 1);
            courageCounterTextMesh.text = newValue + " / " + maxValue;
        }

        IEnumerator FillLerp() {
            yield return null;
        }

        #endregion

    }

}