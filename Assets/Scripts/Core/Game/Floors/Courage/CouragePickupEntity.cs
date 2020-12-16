using AChildsCourage.Game.Floors;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace AChildsCourage.Game.Courage
{

    public class CouragePickupEntity : MonoBehaviour
    {

        #region Methods

        public void SetCouragePickupData(CouragePickupData courageData)
        {
            Variant = courageData.Variant;
            Value = courageData.Value;
            spriteRenderer.sprite = courageData.Sprite;
            spriteRenderer.transform.localScale = courageData.Scale;
            spriteRenderer.material.SetTexture(EmissionTextureKey, courageData.Emission);
            courageName = courageData.CourageName;
            courageLight.pointLightOuterRadius = courageData.LightOuterRadius;
            courageLight.intensity = courageData.LightIntensity;
        }

        #endregion

        #region Fields

#pragma warning disable 649
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Light2D courageLight;
#pragma warning restore 649

        private string courageName = "";
        private static readonly int EmissionTextureKey = Shader.PropertyToID("_Emission");

        #endregion

        #region Properties

        public int Value { get; set; }

        public CourageVariant Variant { get; set; }

        #endregion

    }

}