using UnityEngine;

namespace AChildsCourage.Game.Items
{

    public abstract class ItemBehaviour : MonoBehaviour
    {

        #region Methods

        public abstract void Toggle();

        #endregion

        #region Fields

#pragma warning disable 649

        [SerializeField] [Range(0, 120)] private float _cooldown;
        [SerializeField] private int _id;

#pragma warning restore 649

        #endregion

        #region Properties

        public ItemId Id => (ItemId) _id;

        public float Cooldown
        {
            get => _cooldown;
            set => _cooldown = value;
        }

        public bool IsInBag { get; set; }

        #endregion

    }

}