using System;

namespace AChildsCourage.Game.Input {
    public class ItemPickedUpEventArgs : EventArgs {

        public int SlotId { get; set; }

        public ItemPickedUpEventArgs(int slotId) {
            SlotId = slotId;
        }


    }

}
