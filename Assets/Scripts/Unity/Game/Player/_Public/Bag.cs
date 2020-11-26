using AChildsCourage.Game.Items;
using AChildsCourage.Game.Items.Pickups;
using Ninject.Extensions.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace AChildsCourage.Game.Player
{
    public class Bag : MonoBehaviour
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private ItemPickupSpawner pickupSpawner;
        [SerializeField] private List<GameObject> availableItems = new List<GameObject>();

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

        [AutoInject] internal ItemDataFinder FindItemData { private get; set; }

        #endregion

        #region Methods

        public void UseItem(int usedSlotId)
        {

            if (currentItems[usedSlotId] != null && currentItemCooldown[usedSlotId] == 0)
            {
                currentItems[usedSlotId].Toggle();
                StartCoroutine(Cooldown(usedSlotId));
                itemUsedEvent?.Invoke(FindItemData(currentItems[usedSlotId].Id));
            }

        }

        public void PickUpItem(int slotId, int itemId)
        {
            if (HasItemInSlot(slotId))
                DropItem(slotId);

            PutItemInSlot(slotId, itemId);
        }

        private bool HasItemInSlot(int slotId)
        {
            return currentItems[slotId] != null;
        }

        private void DropItem(int slotId)
        {
            var itemId = currentItems[slotId].Id;
            pickupSpawner.SpawnPickupFor(itemId, transform.position);

            itemDroppedEvent?.Invoke();
        }


        private void PutItemInSlot(int slotId, int itemId)
        {
            currentItems[slotId] = availableItems[itemId].GetComponent<Item>();
            itemPickUpEvent?.Invoke();
        }


        public void OnItemSwapInventory()
        {

            if (currentItemCooldown[0] != 0 || currentItemCooldown[1] != 0)
            {
                // Maybe do an animation when trying to use?? 
                Debug.Log("Can't Swap items when on cooldown!");
            }
            else
            {
                Item tempItem = currentItems[0];
                currentItems[0] = currentItems[1];
                currentItems[1] = tempItem;
                itemSwappedEvent?.Invoke();
            }

        }


        private IEnumerator Cooldown(int usedSlotId)
        {

            while (currentItemCooldown[usedSlotId] < currentItems[usedSlotId].Cooldown)
            {

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