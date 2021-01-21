using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace AChildsCourage.Game.Floors.Courage.UI
{

    public class CourageRiftInfoText : MonoBehaviour
    {

        [SerializeField] private float fadeTime = 0.1f;
        [SerializeField] private string notEnoughCourageText;
        [SerializeField] private string enoughCourageText;

        [FindComponent] private CanvasGroup group;
        [FindComponent(ComponentFindMode.OnChildren)] private TextMeshProUGUI displayText;
        [FindComponent(ComponentFindMode.OnChildren)] private Image iconImage;

        private bool visible;


        private bool Visible
        {
            set
            {
                if (visible == value) return;

                visible = value;
                FadeToAlpha(visible ? 1 : 0);
            }
        }

        private bool ShowIcon
        {
            set => iconImage.enabled = value;
        }

        private string Text
        {
            set => displayText.text = value;
        }

        private float Alpha
        {
            get => group.alpha;
            set => group.alpha = value;
        }


        internal void ShowEnterRiftInfo()
        {
            ShowIcon = true;
            Text = enoughCourageText;
            Visible = true;
        }

        internal void ShowNeedMoreCourageInfo()
        {
            ShowIcon = false;
            Text = notEnoughCourageText;
            Visible = true;
        }

        internal void Hide() =>
            Visible = false;

        private void FadeToAlpha(float alpha)
        {
            var startAlpha = Alpha;

            void ApplyT(float t) =>
                Alpha = Mathf.Lerp(startAlpha, alpha, t);

            StartCoroutine(Lerping.TimeLerp(ApplyT, fadeTime));
        }

    }

}