using UnityEngine;

namespace AChildsCourage.Game.Floors.Courage
{

    [CreateAssetMenu(fileName = "New courage-pickup appearance", menuName = "A Child's Courage/Courage-pickup appearance")]
    public class CouragePickupAppearanceAsset : ScriptableObject
    {

        [SerializeField] private CourageVariant variant;
        [SerializeField] private float lightOuterRadius;
        [SerializeField] private float lightIntensity;
        [SerializeField] private Texture2D emission;
        [SerializeField] private Sprite sprite;
        [SerializeField] private Vector3 scale;

        public CourageVariant Variant => variant;

        public float LightOuterRadius => lightOuterRadius;

        public float LightIntensity => lightIntensity;

        public Texture2D Emission => emission;

        public Sprite Sprite => sprite;

        public Vector3 Scale => scale;

    }

}