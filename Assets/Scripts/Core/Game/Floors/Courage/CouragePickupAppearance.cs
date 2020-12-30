using UnityEngine;

namespace AChildsCourage.Game.Floors.Courage
{

    [CreateAssetMenu(fileName = "New courage-pickup appearance", menuName = "A Child's Courage/Courage-pickup appearance")]
    public class CouragePickupAppearance : ScriptableObject
    {

        [SerializeField] private int value;
        [SerializeField] private float lightOuterRadius;
        [SerializeField] private float lightIntensity;
        [SerializeField] private Texture2D emission;
        [SerializeField] private Sprite sprite;
        [SerializeField] private CourageVariant variant;
        [SerializeField] private Vector3 scale;

        public int Value => value;

        public float LightOuterRadius => lightOuterRadius;

        public float LightIntensity => lightIntensity;
        
        public Texture2D Emission => emission;

        public Sprite Sprite => sprite;

        public CourageVariant Variant => variant;

        public Vector3 Scale => scale;

    }

}