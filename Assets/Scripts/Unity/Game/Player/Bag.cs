using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AChildsCourage.Game.Player {
    public class Bag {

        public Item[] item = new Item[2];

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