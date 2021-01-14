using System;

namespace AChildsCourage.Game.Input {
    internal class RiftInteractInputEventArgs : EventArgs {

        public bool HasRiftInteractInput { get; }

        public RiftInteractInputEventArgs(bool hasRiftInteractInput) {
            HasRiftInteractInput = hasRiftInteractInput;
        }

    }

}
