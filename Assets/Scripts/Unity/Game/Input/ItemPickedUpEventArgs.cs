using System;

namespace AChildsCourage.Game.Input
{

    internal class ItemPickedUpEventArgs : EventArgs
    {

        #region Properties

        internal int SlotId { get; set; }

        #endregion

        #region Constructors

        internal ItemPickedUpEventArgs(int slotId)
        {
            SlotId = slotId;
        }

        #endregion

    }

}