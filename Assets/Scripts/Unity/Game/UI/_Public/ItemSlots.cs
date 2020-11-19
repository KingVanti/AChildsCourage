using AChildsCourage.Game.Pickups;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using static AChildsCourage.CustomMathModule;

namespace AChildsCourage.Game.UI
{

    public class ItemSlots : MonoBehaviour {

        #region Constants

        private const int MaxFillAmount = 1;

        #endregion

        #region Fields

#pragma warning disable 649

        [SerializeField] private Image[] itemSlotsImages = new Image[2];
        [SerializeField] private List<Sprite> availableItemSprites = new List<Sprite>();
        [SerializeField] private Image[] itemCooldownFills = new Image[2];

#pragma warning restore 649

        #endregion

        #region Methods

        public void AddItem(int slotId, int itemId) {
            itemSlotsImages[slotId].sprite = availableItemSprites[itemId];
        }

        public void SwapItems() {

            Sprite tempSprite = itemSlotsImages[0].sprite;
            itemSlotsImages[0].sprite = itemSlotsImages[1].sprite;
            itemSlotsImages[1].sprite = tempSprite;

        }


        public void UpdateCooldown(int slotId, float currentCooldown, float Cooldown) {

            itemCooldownFills[slotId].fillAmount = Map(currentCooldown, 0, Cooldown, 0, MaxFillAmount);

            if (itemCooldownFills[slotId].fillAmount < 1) {
                itemSlotsImages[slotId].color = new Color(itemSlotsImages[slotId].color.r, itemSlotsImages[slotId].color.g, itemSlotsImages[slotId].color.b, 0.5f);
            } else {
                itemSlotsImages[slotId].color = new Color(itemSlotsImages[slotId].color.r, itemSlotsImages[slotId].color.g, itemSlotsImages[slotId].color.b, 1);
                itemCooldownFills[slotId].fillAmount = 0;
            }

        }

        #endregion

    }

}
