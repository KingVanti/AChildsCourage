using System;

namespace AChildsCourage.Game.Input {
    public class ItemButtonTwoPressedEventArgs : EventArgs {

        public bool IsPresssed { get; }

        public ItemButtonTwoPressedEventArgs(bool isPressed) {

            IsPresssed = isPressed;

        }

    }
}
