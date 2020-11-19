using UnityEngine;


namespace AChildsCourage.Game.Courage
{

    [CreateAssetMenu(fileName = "Courage", menuName = "A Child's Courage/Courage", order = 3)]
    public class CouragePickupData : ScriptableObject {

        #region Fields

#pragma warning disable 649
        [SerializeField] private int _value;
        [SerializeField] private string _courageName;
        [SerializeField] private CourageVariant _variant;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private Vector3 _scale;
        [SerializeField] private Texture2D _emission;
#pragma warning restore 649

        #endregion

        #region Properties

        public CourageVariant Variant { get { return _variant; } }

        public int Value { get { return _value; } }

        public string CourageName { get { return _courageName; } }

        public Sprite Sprite { get { return _sprite; } }

        public Vector3 Scale { get { return _scale; } }

        public Texture2D Emission { get { return _emission; } }

        #endregion



    }
}
