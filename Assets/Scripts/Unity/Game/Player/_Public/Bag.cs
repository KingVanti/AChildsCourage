using AChildsCourage.Game.Pickups;
using Ninject.Extensions.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AChildsCourage.Game.Player
{
    public class Bag : MonoBehaviour {

        #region Fields

#pragma warning disable 649

        [SerializeField] private List<GameObject> availableItems = new List<GameObject>();
        [SerializeField] private GameObject pickupPrefab;
        [SerializeField] private Transform pickupContainer;

#pragma warning restore 649

        private Item[] currentItems = new Item[2];
        private float[] currentItemCooldown = new float[2];

        public CooldownEvent cooldownEvent;
        public ItemDataEvent itemUsedEvent;
        public UnityEvent itemDroppedEvent;
        public UnityEvent itemSwappedEvent;
        public UnityEvent itemPickUpEvent;

        #endregion

        #region Properties

        [AutoInject] internal IItemPickupRepository pickupRepository { private get; set; }

        #endregion

        #region Methods

        public void UseItem(int usedSlotId) {

            if (currentItems[usedSlotId] != null && currentItemCooldown[usedSlotId] == 0) {
                currentItems[usedSlotId].Toggle();
                StartCoroutine(Cooldown(usedSlotId));
                itemUsedEvent?.Invoke(pickupRepository.GetSpecificItem(currentItems[usedSlotId].Id));
            }

        }

        public void PickUpItem(int slotId, int itemId) {

            if (currentItems[slotId] == null) {
                currentItems[slotId] = availableItems[itemId].GetComponent<Item>();
                itemPickUpEvent?.Invoke();
            } else {
                GameObject droppedItem = Instantiate(pickupPrefab, transform.position, Quaternion.identity, pickupContainer);
                droppedItem.GetComponent<ItemPickup>().SetItemData(pickupRepository.GetSpecificItem(currentItems[slotId].Id));
                itemDroppedEvent?.Invoke();
                currentItems[slotId] = availableItems[itemId].GetComponent<Item>();
                itemPickUpEvent?.Invoke();
            }

        }

        public void OnItemSwapInventory() {

            if (currentItemCooldown[0] != 0 || currentItemCooldown[1] != 0) {
                // Maybe do an animation when trying to use?? 
                Debug.Log("Can't Swap items when on cooldown!");
            } else {
                Item tempItem = currentItems[0];
                currentItems[0] = currentItems[1];
                currentItems[1] = tempItem;
                itemSwappedEvent?.Invoke();
            }

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
        public class CooldownEvent : UnityEvent<int, float, float> { }

        [Serializable]
        public class ItemDataEvent : UnityEvent<ItemData> { }

        #endregion

    }

}