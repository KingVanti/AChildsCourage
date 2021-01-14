using System;

namespace AChildsCourage.Game.Char {
    public class RiftEscapeEventArgs : EventArgs {

        public bool IsEscapingThroughRift { get; }

        public RiftEscapeEventArgs(bool isEscapingThroughRift) {
            IsEscapingThroughRift = isEscapingThroughRift;
        }

    }


}