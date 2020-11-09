using System;

namespace AChildsCourage.Game.Input {
    public class ItemButtonOnePressedEventArgs : EventArgs {

        public bool IsPresssed { get; }

        public ItemButtonOnePressedEventArgs(bool isPressed) {

            IsPresssed = isPressed;

        }
    }
}
