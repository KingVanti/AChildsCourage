using System.Linq;
using Ninject.Extensions.Unity;
using UnityEngine;
using static ItemDataRepository;

namespace AChildsCourage.Game.Items.Pickups
{

    [UseDi]
    public class ItemPickupSpawnerEntity : MonoBehaviour
    {

        #region Properties

        [AutoInject] internal FindItemData FindItemData { private get; set; }

        #endregion

        #region Fields

#pragma warning disable 649

        [SerializeField] private GameObject pickupPrefab;
        [SerializeField] private ItemIcon[] itemIcons;

#pragma warning restore 649

        #endregion

        #region Methods

        public void SpawnPickupFor(ItemId itemId, Vector3 position)
        {
            var pickupEntity = SpawnItemPickup(position);
            var itemData = FindItemData(itemId);
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