using AChildsCourage.Game.Floors;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

namespace AChildsCourage.Game.Courage
{
    public class CouragePickupEntity : MonoBehaviour {

        #region Fields

#pragma warning disable 649
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Light2D courageLight;
#pragma warning restore 649

        private int _value = 0;
        private CourageVariant _variant;
        private string courageName = "";

        #endregion

        #region Properties

        public int Value {
            get { return _value; }
            set { _value = value; }
        }

        public CourageVariant Variant {
            get { return _variant; }
            set { _variant = value; }
        }

        #endregion

        #region Methods

        public void SetCouragePickupData(CouragePickupData courageData) {

            _variant = courageData.Variant;
            Value = courageData.Value;
            spriteRenderer.sprite = courageData.Sprite;
            spriteRenderer.transform.localScale = courageData.Scale;
            spriteRenderer.material.SetTexture("_Emission", courageData.Emission);
            courageName = courageData.CourageName;
            courageLight.pointLightOuterRadius = courageData.LightOuterRadius;
            courageLight.intensity = courageData.LightIntensity;


        }

        #endregion

    }

}
