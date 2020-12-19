using UnityEngine;

namespace AChildsCourage.Game.Floors.Courage
{

    [CreateAssetMenu(fileName = "New courage-pickup appearance", menuName = "A Child's Courage/Courage-pickup appearance")]
    public class CouragePickupAppearance : ScriptableObject
    {

        [SerializeField] private int _value;
        [SerializeField] private string _courageName;
        [SerializeField] private CourageVariant _variant;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private Vector3 _scale;
        [SerializeField] private Texture2D _emission;
        [SerializeField] private float _lightOuterRadius;
        [SerializeField] private float _lightIntensity;


        public CourageVariant Variant => _variant;

        public int Value => _value;

        public string CourageName => _courageName;

        public Sprite Sprite => _sprite;

        public Vector3 Scale => _scale;

        public Texture2D Emission => _emission;

        public float LightOuterRadius => _lightOuterRadius;

        public float LightIntensity => _lightIntensity;

    }

}