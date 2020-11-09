using UnityEngine;

namespace AChildsCourage.Game.Pickups {

    [CreateAssetMenu(fileName = "Item", menuName = "A Child's Courage/ItemData", order = 2)]
    public class ItemData : ScriptableObject {

        #region Fields

#pragma warning disable 649
        [SerializeField] private int _id;
        [SerializeField] private string _itemName;
        [SerializeField] private Sprite _sprite;
        [SerializeField] private Vector3 _scale;
        [Range(0,360)]
        [SerializeField] private float _rotation;
#pragma warning restore 649

        #endregion

        #region Properties

        public int Id { get { return _id; } }
        public string ItemName { get { return _itemName; } }
        public Sprite Sprite { get { return _sprite; } }
        public Vector3 Scale { get { return _scale; } }
        public float Rotation { get { return _rotation; } }

        #endregion


    }

}