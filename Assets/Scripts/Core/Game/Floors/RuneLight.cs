using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using static AChildsCourage.M;
using static AChildsCourage.Range;

namespace AChildsCourage.Game.Floors
{

    public class RuneLight : MonoBehaviour
    {

        [SerializeField] private Range<float> intensityRange;
        [SerializeField] private float flashTime;
        [SerializeField] private float fadeOutTime;
        [SerializeField] private float flashIntensity;

        [FindComponent] private new Light2D light;

        private bool burnedOut;


        private float Intensity
        {
            get => light.intensity;
            set => light.intensity = value;
        }


        internal void UpdateLight(RuneCharge charge)
        {
            if (!burnedOut)
                Intensity = intensityRange.Map(Lerp, (float) charge);
        }

        public void Flash()
        {
            burnedOut = true;

            void ApplyT(float t) =>
                Intensity = t.Map(Remap, 0f, 1f, intensityRange.Max, flashIntensity);

            StartCoroutine(Lerping.TimeLerp(ApplyT, flashTime, FadeOut));
        }

        private void FadeOut()
        {
            void ApplyT(float t) =>
                Intensity = t.Map(RemapSquared, 0f, 1f, flashIntensity, intensityRange.Min);

            StartCoroutine(Lerping.TimeLerp(ApplyT, fadeOutTime));
        }

    }

}