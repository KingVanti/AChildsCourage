using UnityEngine;


namespace AChildsCourage.Game.Courage {

    [CreateAssetMenu(fileName = "Courage", menuName = "A Child's Courage/Courage", order = 3)]
    public class CouragePickupData : ScriptableObject {

        #region Fields

#pragma warning disable 649
        [SerializeField] private int _value;
        [SerializeField] private int _id;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private Vector3 _scale;
#pragma warning restore 649

        #endregion

        #region Properties

        public int Id { get { return _id; } }

        public int Value { get { return _value; } }

        public Sprite Sprite { get { return _sprite; } }

        public Vector3 Scale { get { return _scale; } }

        #endregion



    }
}
