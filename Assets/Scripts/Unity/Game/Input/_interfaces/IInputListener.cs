using System;

namespace AChildsCourage.Game.Input
{

    public interface IInputListener
    {

        event EventHandler<MousePositionChangedEventArgs> OnMousePositionChanged;
        event EventHandler<MoveDirectionChangedEventArgs> OnMoveDirectionChanged;
        event EventHandler<ItemButtonOnePressedEventArgs> OnItemButtonOnePressed;
        event EventHandler<ItemButtonTwoPressedEventArgs> OnItemButtonTwoPressed;

    }

}
