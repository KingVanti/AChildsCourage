using System;

namespace AChildsCourage.Game.Char
{

    public class RiftEscapeEventArgs : EventArgs
    {

        internal bool IsEscapingThroughRift { get; }

        internal RiftEscapeEventArgs(bool isEscapingThroughRift) =>
            IsEscapingThroughRift = isEscapingThroughRift;

    }

}