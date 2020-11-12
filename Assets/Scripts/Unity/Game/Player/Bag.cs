using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AChildsCourage.Game.Player {
    public class Bag : MonoBehaviour {

        #region Fields

        [SerializeField] List<GameObject> availableItems = new List<GameObject>();

        private int slotOneId;
        private int slotTwoId;

        private bool itemSlotOneFilled = false;
        private bool itemSlotTwoFilled = false;

        #endregion

        #region Methods

        public void ActivateItemOne() {

            if (itemSlotOneFilled) {
                availableItems[slotOneId].GetComponent<Item>().Toggle();
            }


        }

        public void ActivateItemTwo() {

            if (itemSlotTwoFilled) {
                availableItems[slotTwoId].GetComponent<Item>().Toggle();
            }

        }

        public void OnItemOnePickup(int itemId) {

            slotOneId = itemId;
            itemSlotOneFilled = true;

        }

        public void OnItemTwoPickup(int itemId) {

            slotTwoId = itemId;
            itemSlotTwoFilled = true;

        }

        public void OnItemSwapInventory() {
        }

        public void OnItemSwapGround() {
            throw new NotImplementedException();
        }

        IEnumerator Cooldown(float time) {

            yield return null;

        }

        #endregion

    }

}