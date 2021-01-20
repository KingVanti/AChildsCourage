using AChildsCourage.Game.Char;
using AChildsCourage.Game.Floors.Courage;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static AChildsCourage.M;
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
        [SerializeField] private Animation pickupAnimation;
        [SerializeField] private Material glossMaterial;

        private float completionPercent;


        private float FillPercent
        {
            get => courageBarFill.fillAmount;
            set => courageBarFill.fillAmount = value.Map(Clamp, 0f, 1f);
        }

        private float CompletionPercent
        {
            get => completionPercent;
            set
            {
                completionPercent = value;
                TextColor = value >= HundredPercent ? textColor : defaultTextColor;
                Text = $"{Mathf.FloorToInt(value * 100)}%";
                if (value >= HundredPercent)
                    courageBarFill.material = glossMaterial;
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

        [Sub(nameof(CharControllerEntity.OnCouragePickedUp))]
        private void OnCouragePickedUp(object _, CouragePickedUpEventArgs eventArgs)
        {
            if (eventArgs.Variant == CourageVariant.Spark) pickupAnimation.PlayQueued("Spark", QueueMode.CompleteOthers);

            if (eventArgs.Variant == CourageVariant.Orb) pickupAnimation.PlayQueued("Orb", QueueMode.CompleteOthers);
        }

        private void PlayFillAnimation(AnimationEvent fillAnimation)
        {
            fillAnimation.floatParameter = CompletionPercent;
            UpdateBarFill(CompletionPercent);
        }


        private void UpdateBarFill(float percent) =>
            this.StartOnly(() => TimeLerp(new Range<float>(FillPercent, percent),
                                          t => FillPercent = t,
                                          barFillTime));

    }

}