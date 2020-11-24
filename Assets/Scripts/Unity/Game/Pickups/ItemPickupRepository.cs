using Castle.Core.Internal;
using System.Collections.Generic;
using UnityEngine;
using static AChildsCourage.RNG;

namespace AChildsCourage.Game.Pickups
{
    internal class ItemPickupRepository : IItemPickupRepository
    {

        #region Constants

        private const string ItemDataResourcePath = "Items/";

        #endregion

        #region Fields

        private List<ItemData> availableItems = new List<ItemData>();

        #endregion

        #region Constructors

        public ItemPickupRepository()
        {
            availableItems.AddRange(Resources.LoadAll<ItemData>(ItemDataResourcePath));
        }

        #endregion

        #region Methods

        public ItemData GetNextItem(RNGSource rng)
        {

            if (!availableItems.IsNullOrEmpty())
            {
                ItemData nextItem = availableItems.GetRandom(rng);
                availableItems.Remove(nextItem);
                return nextItem;
            }
            else
            {
                throw new System.Exception("Item list is empty!");
            }

        }

        public ItemData GetSpecificItem(int id)
        {
            return availableItems[id];
        }

        #endregion

    }
}
