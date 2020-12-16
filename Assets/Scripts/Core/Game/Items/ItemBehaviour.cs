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

        [SerializeField] [Range(0, 120)] private float cooldown;
        [SerializeField] private int id;

#pragma warning restore 649

        #endregion

        #region Properties

        public ItemId Id => (ItemId) id;

        public float Cooldown => cooldown;

        #endregion

    }

}