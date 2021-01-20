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


        internal CourageVariant Variant => variant;

        internal float LightOuterRadius => lightOuterRadius;

        internal float LightIntensity => lightIntensity;

        internal Texture2D Emission => emission;

        internal Sprite Sprite => sprite;

        internal Vector3 Scale => scale;

    }

}