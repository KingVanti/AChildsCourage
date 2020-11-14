using Castle.Core.Internal;
using System.Collections.Generic;
using UnityEngine;

namespace AChildsCourage.Game.Pickups
{
    public class ItemPickupRepository : IItemPickupRepository {

        #region Constants

        protected const string ItemDataResourcePath = "Items/";

        #endregion

        private List<ItemData> availableItems = new List<ItemData>();

        #region Constructors

        public ItemPickupRepository() {

            availableItems.AddRange(Resources.LoadAll<ItemData>(ItemDataResourcePath));

        }

        #endregion

        #region Methods

        public ItemData GetNextItem(IRNG rng) {

            if (!availableItems.IsNullOrEmpty()) {
                ItemData nextItem = availableItems.GetRandom(rng);
                availableItems.Remove(nextItem);
                return nextItem;
            } else {
                throw new System.Exception("Item list is empty!");
            }

        }

        #endregion
    }
}
