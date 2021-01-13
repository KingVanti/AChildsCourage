using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using static AChildsCourage.Range;

namespace AChildsCourage.Game.Floors
{

    public class RuneLight : MonoBehaviour
    {

        [SerializeField] private Range<float> intensityRange;

        [FindComponent] private new Light2D light;


        public void UpdateLight(RuneCharge charge) =>
            light.intensity = intensityRange.Map(Lerp, (float) charge);

    }

}