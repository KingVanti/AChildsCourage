using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AChildsCourage.Game.Player {
    public class Bag : MonoBehaviour {

        #region Fields

        [SerializeField] List<GameObject> availableItems = new List<GameObject>();

        private Item[] currentItems = new Item[2];
        private float[] currentItemCooldown = new float[2];

        public CooldownEvent cooldownEvent;
        public ItemUsedEvent itemUsedEvent;

        #endregion

        #region Methods

        public void UseItem(int usedSlotId) {

            if (currentItems[usedSlotId] != null && currentItemCooldown[usedSlotId] == 0) {
                currentItems[usedSlotId].Toggle();
                StartCoroutine(Cooldown(usedSlotId));
                itemUsedEvent?.Invoke(currentItems[usedSlotId].Id);
            }

        }

        public void PickUpItem(int slotId, int itemId) {

            availableItems[itemId].GetComponent<Item>().IsInBag = true;
            currentItems[slotId] = availableItems[itemId].GetComponent<Item>();

        }

        public void OnItemSwapInventory() {
        }

        public void OnItemSwapGround() {
            throw new NotImplementedException();
        }


        IEnumerator Cooldown(int usedSlotId) {
             
            while (currentItemCooldown[usedSlotId] < currentItems[usedSlotId].Cooldown) {

                currentItemCooldown[usedSlotId] = Mathf.MoveTowards(currentItemCooldown[usedSlotId], currentItems[usedSlotId].Cooldown, Time.deltaTime);
                cooldownEvent?.Invoke(usedSlotId, currentItemCooldown[usedSlotId], currentItems[usedSlotId].Cooldown);
                yield return null;

            }

            currentItemCooldown[usedSlotId] = 0;

        }

        #endregion

        #region Subclasses

        [Serializable]
        public class CooldownEvent : UnityEvent<int,float,float> { }

        [Serializable]
        public class ItemUsedEvent : UnityEvent<int> { }

        #endregion

    }

}