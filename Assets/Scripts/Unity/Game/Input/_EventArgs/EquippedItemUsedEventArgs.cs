using System;

namespace AChildsCourage.Game.Input
{
    public class EquippedItemUsedEventArgs : EventArgs {

        public int SlotId { get; set; }

        public EquippedItemUsedEventArgs(int slotId) {
            SlotId = slotId;
        }


    }

}
