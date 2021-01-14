using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using static AChildsCourage.Range;

namespace AChildsCourage.Game.Floors
{

    public class RuneLight : MonoBehaviour
    {

        [SerializeField] private Range<float> intensityRange;

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
                Intensity = t.Remap(0, 1, intensityRange.Max, 100);

            StartCoroutine(Lerping.TimeLerp(ApplyT, 0.75f, FadeOut));
        }

        private void FadeOut()
        {
            void ApplyT(float t) =>
                Intensity = t.RemapSquared(0, 1, 100, intensityRange.Min);

            StartCoroutine(Lerping.TimeLerp(ApplyT, 2f));
        }

    }

}