using AChildsCourage.Game.Floors;
using UnityEngine;

namespace AChildsCourage.Game.Courage
{

    [CreateAssetMenu(fileName = "Courage", menuName = "A Child's Courage/Courage", order = 3)]
    public class CouragePickupData : ScriptableObject
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private int value;
        [SerializeField] private string courageName;
        [SerializeField] private CourageVariant variant;
        [SerializeField] private Sprite sprite;
        [SerializeField] private Vector3 scale;
        [SerializeField] private Texture2D emission;
        [SerializeField] private float lightOuterRadius;
        [SerializeField] private float lightIntensity;

#pragma warning restore 649

        #endregion

        #region Properties

        public CourageVariant Variant => variant;

        public int Value => value;

        public string CourageName => courageName;

        public Sprite Sprite => sprite;

        public Vector3 Scale => scale;

        public Texture2D Emission => emission;

        public float LightOuterRadius => lightOuterRadius;

        public float LightIntensity => lightIntensity;

        #endregion

    }

}