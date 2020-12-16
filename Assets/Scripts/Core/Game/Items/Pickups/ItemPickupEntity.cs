using UnityEngine;

namespace AChildsCourage.Game.Items.Pickups
{

    public class ItemPickupEntity : MonoBehaviour
    {

        #region Properties
        public ItemId Id { get; private set; }
        #endregion

        #region Fields

#pragma warning disable 649

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private CanvasGroup tutorialInfo;
        [SerializeField] private int flashlightId;
        [SerializeField] private ItemIcon flashlightIcon;

#pragma warning restore 649

        #endregion

        #region Methods

        private void Start() {
            SetItem(flashlightId, flashlightIcon);
        }

        private void SetItem(int itemId, ItemIcon icon) {
            Id = new ItemId(itemId);
            SetIcon(icon);
        }

        public void SetItemData(ItemData itemData, ItemIcon icon)
        {
            Id = itemData.Id;
            gameObject.name = itemData.Name;

            SetIcon(icon);
        }

        private void SetIcon(ItemIcon icon)
        {
            spriteRenderer.sprite = icon.InactiveIcon;
            gameObject.transform.localScale = icon.Scale;
            spriteRenderer.transform.rotation = Quaternion.Euler(new Vector3(0, 0, icon.RotationAngles));
        }

        public void ShowInfo(bool status)
        {
            tutorialInfo.alpha = status ? 1 : 0;
        }

        #endregion

    }

}