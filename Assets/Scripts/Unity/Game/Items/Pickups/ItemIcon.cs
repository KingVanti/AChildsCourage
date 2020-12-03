using UnityEngine;

namespace AChildsCourage.Game.Items.Pickups
{

    [CreateAssetMenu(fileName = "Item icon", menuName = "A Child's Courage/Item icon")]
    public class ItemIcon : ScriptableObject
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private int itemId;
        [SerializeField] private Sprite inactiveIcon;
        [SerializeField] private Sprite activeIcon;
        [SerializeField] private Vector3 scale;
        [SerializeField] [Range(0, 360)] private float rotationAngles;

#pragma warning restore 649

        #endregion

        #region Properties

        public ItemId ItemId => (ItemId) itemId;

        public Sprite InactiveIcon => inactiveIcon;

        public Sprite ActiveIcon => activeIcon;

        public Vector3 Scale => scale;

        public float RotationAngles => rotationAngles;

        #endregion

    }

}