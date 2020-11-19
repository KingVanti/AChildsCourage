using UnityEngine;

namespace AChildsCourage.Game.Pickups
{
    public class ItemPickup : MonoBehaviour {

        #region Fields

#pragma warning disable 649
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private ItemData testItem;
        [SerializeField] private CanvasGroup tutorialInfo;
#pragma warning restore 649

        #endregion

        #region Properties

        public int Id { get; private set; }

        #endregion

        #region Methods

        // TEMP METHOD DELETE LATER
        private void Start() {
            SetItemData(testItem);
        }

        public void SetItemData(ItemData itemData) {

            Id = itemData.Id;
            gameObject.name = itemData.ItemName;
            spriteRenderer.sprite = itemData.Sprite;
            gameObject.transform.localScale = itemData.Scale;
            spriteRenderer.transform.rotation = Quaternion.Euler(new Vector3(0,0,itemData.Rotation));

        }

        public void ShowInfo(bool status) {
            tutorialInfo.alpha =  status ? 1 : 0;
        }

        #endregion

    }
}
