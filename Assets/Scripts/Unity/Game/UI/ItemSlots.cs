using AChildsCourage.Game.Pickups;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AChildsCourage.Game.UI {
    public class ItemSlots : MonoBehaviour {

        [SerializeField] private Image[] itemSlotsImages = new Image[2];
        [SerializeField] private List<Sprite> availableItemImages = new List<Sprite>();
        [SerializeField] private Image[] itemCooldownFills = new Image[2];

        private int maxFill = 1;
        

        public void UpdateItems(int slotId, int itemId) {

            itemSlotsImages[slotId].sprite = availableItemImages[itemId];

            if (!itemSlotsImages[slotId].enabled) {
                itemSlotsImages[slotId].enabled = true;
            }

        }

        public void UpdateCooldown(int slotId, float currentCooldown, float Cooldown) {
            itemCooldownFills[slotId].fillAmount = Utils.Map(currentCooldown, 0, Cooldown, 0, maxFill);

            if(itemCooldownFills[slotId].fillAmount < 1) {
                itemSlotsImages[slotId].color = new Color(itemSlotsImages[slotId].color.r, itemSlotsImages[slotId].color.g, itemSlotsImages[slotId].color.b, 0.5f);
            } else {
                itemSlotsImages[slotId].color = new Color(itemSlotsImages[slotId].color.r, itemSlotsImages[slotId].color.g, itemSlotsImages[slotId].color.b, 1);
                itemCooldownFills[slotId].fillAmount = 0;
            }

        }


    }

}
