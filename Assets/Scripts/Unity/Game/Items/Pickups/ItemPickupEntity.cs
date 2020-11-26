using UnityEngine;

namespace AChildsCourage.Game.Items.Pickups
{
    public class ItemPickupEntity : MonoBehaviour
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private CanvasGroup tutorialInfo;

#pragma warning restore 649

        #endregion

        #region Properties

        public ItemId Id { get; private set; }

        #endregion

        #region Methods

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
