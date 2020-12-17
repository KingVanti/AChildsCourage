using System.Linq;
using AChildsCourage.Infrastructure;
using UnityEngine;
using static AChildsCourage.ItemDataRepo;

namespace AChildsCourage.Game.Items.Pickups
{
    
    public class ItemPickupSpawnerEntity : MonoBehaviour
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private GameObject pickupPrefab;
        [SerializeField] private ItemIcon[] itemIcons;

        [FindService] private FindItemData findItemData;

#pragma warning restore 649

        #endregion

        #region Methods

        public void SpawnPickupFor(ItemId itemId, Vector3 position)
        {
            var pickupEntity = SpawnItemPickup(position);
            var itemData = findItemData(itemId);
            var icon = itemIcons.First(i => i.ItemId == itemData.Id);

            pickupEntity.SetItemData(itemData, icon);
        }

        private ItemPickupEntity SpawnItemPickup(Vector3 position)
        {
            var itemGameObject = Instantiate(pickupPrefab, position, Quaternion.identity, transform);
            return itemGameObject.GetComponent<ItemPickupEntity>();
        }

        #endregion

    }

}