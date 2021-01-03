using AChildsCourage.Game.Floors.Courage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static AChildsCourage.Lerping;

namespace AChildsCourage.Game.UI
{

    public class CourageBarEntity : MonoBehaviour
    {

        private const float HundredPercent = 1;

        private static readonly Color defaultTextColor = new Color(1, 1, 1, 1);

        [SerializeField] private float barFillTime;
        [SerializeField] private Image courageBarFill;
        [SerializeField] private TextMeshProUGUI courageCounterTextMesh;
        [SerializeField] private Color textColor;


        private float FillPercent
        {
            get => courageBarFill.fillAmount;
            set => courageBarFill.fillAmount = value.Clamp(0, 1);
        }

        private float CompletionPercent
        {
            set
            {
                TextColor = value >= HundredPercent ? textColor : defaultTextColor;
                Text = $"{Mathf.FloorToInt(value * 100)}%";
                UpdateBarFill(value);
            }
        }

        private Color TextColor
        {
            set => courageCounterTextMesh.color = value;
        }

        private string Text
        {
            set => courageCounterTextMesh.text = value;
        }


        [Sub(nameof(CourageManagerEntity.OnCollectedCourageChanged))]
        private void OnCollectedCourageChanged(object _, CollectedCourageChangedEventArgs eventArgs) =>
            CompletionPercent = eventArgs.CompletionPercent;

        private void UpdateBarFill(float percent) =>
            this.StartOnly(() => TimeLerp(new Range<float>(FillPercent, percent),
                                          t => FillPercent = t,
                                          barFillTime));

    }

}