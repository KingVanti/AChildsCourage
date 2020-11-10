using System;
using UnityEngine;

namespace AChildsCourage.Game.Player {
    public class Bag : MonoBehaviour {

        public Item[] carriedItems = new Item[2];

        public void OnItemPickup(Item item) {
            throw new NotImplementedException();
        }

        public void OnItemDrop(int index) {
            throw new NotImplementedException();
        }

        public void OnItemSwapInventory() {
            throw new NotImplementedException();
        }

        public void OnItemSwapGround() {
            throw new NotImplementedException();
        }

    }

}