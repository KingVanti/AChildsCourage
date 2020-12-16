using System;
using System.Collections;
using System.Collections.Generic;
using AChildsCourage.Game.Items;
using AChildsCourage.Game.Items.Pickups;
using Ninject.Extensions.Unity;
using UnityEngine;
using UnityEngine.Events;

namespace AChildsCourage.Game.Char
{

    public class BagEntity : MonoBehaviour
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private List<GameObject> availableItems = new List<GameObject>();

#pragma warning restore 649

        private readonly ItemBehaviourEntity[] currentItems = new ItemBehaviourEntity[2];
        private readonly float[] currentItemCooldown = new float[2];

        public CharEvents.Cooldown cooldownEvent;
        public CharEvents.ItemData itemUsedEvent;
        public Events.Empty itemDroppedEvent;
        public Events.Empty itemSwappedEvent;
        public Events.Empty itemPickUpEvent;

        #endregion

        #region Properties

        [AutoInject] internal FindItemData FindItemData { private get; set; }

        [AutoInject] private ItemPickupSpawnerEntity PickupSpawner { get; set; }

        #endregion

        #region Methods

        public void UseItem(int usedSlotId)
        {
            if (currentItems[usedSlotId] == null || currentItemCooldown[usedSlotId] != 0) return;

            currentItems[usedSlotId].Toggle();
            StartCoroutine(Cooldown(usedSlotId));
            itemUsedEvent?.Invoke(FindItemData(currentItems[usedSlotId]
                                                   .Id));
        }

        public void PickUpItem(int slotId, int itemId)
        {
            if (HasItemInSlot(slotId)) DropItem(slotId);

            PutItemInSlot(slotId, itemId);
        }

        private bool HasItemInSlot(int slotId) => currentItems[slotId] != null;

        private void DropItem(int slotId)
        {
            var itemId = currentItems[slotId]
                .Id;
            PickupSpawner.SpawnPickupFor(itemId, transform.position);

            itemDroppedEvent?.Invoke();
        }

        private void PutItemInSlot(int slotId, int itemId)
        {
            currentItems[slotId] = availableItems[itemId]
                .GetComponent<ItemBehaviourEntity>();
            itemPickUpEvent?.Invoke();
        }


        public void OnItemSwapInventory()
        {
            if (currentItemCooldown[0] != 0 || currentItemCooldown[1] != 0)

                // Maybe do an animation when trying to use?? 
                Debug.Log("Can't Swap items when on cooldown!");
            else
            {
                var tempItem = currentItems[0];
                currentItems[0] = currentItems[1];
                currentItems[1] = tempItem;
                itemSwappedEvent?.Invoke();
            }
        }


        private IEnumerator Cooldown(int usedSlotId)
        {
            while (currentItemCooldown[usedSlotId] <
                   currentItems[usedSlotId]
                       .Cooldown)
            {
                currentItemCooldown[usedSlotId] = Mathf.MoveTowards(currentItemCooldown[usedSlotId], currentItems[usedSlotId]
                                                                        .Cooldown, Time.deltaTime);
                cooldownEvent?.Invoke(usedSlotId, currentItemCooldown[usedSlotId], currentItems[usedSlotId]
                                          .Cooldown);
                yield return null;
            }

            currentItemCooldown[usedSlotId] = 0;
        }

        #endregion
        

    }

}