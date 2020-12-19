using AChildsCourage.Game.Floors;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace AChildsCourage.Game.Floors.Courage
{

    public class CouragePickupEntity : MonoBehaviour
    {
        
        private static readonly int emissionTextureKey = Shader.PropertyToID("_Emission");

        #region Methods

        public void SetCouragePickupData(CouragePickupAppearance courageAppearance)
        {
            Variant = courageAppearance.Variant;
            Value = courageAppearance.Value;
            spriteRenderer.sprite = courageAppearance.Sprite;
            spriteRenderer.transform.localScale = courageAppearance.Scale;
            spriteRenderer.material.SetTexture(emissionTextureKey, courageAppearance.Emission);
            courageName = courageAppearance.CourageName;
            courageLight.pointLightOuterRadius = courageAppearance.LightOuterRadius;
            courageLight.intensity = courageAppearance.LightIntensity;
        }

        #endregion

        #region Fields


        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Light2D courageLight;


        private string courageName = "";

        #endregion

        #region Properties

        public int Value { get; private set; }

        public CourageVariant Variant { get; private set; }

        #endregion

    }

}