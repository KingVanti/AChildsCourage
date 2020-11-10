using System;

namespace AChildsCourage.Game.Input
{

    public interface IInputListener
    {

        event EventHandler<MousePositionChangedEventArgs> OnMousePositionChanged;
        event EventHandler<MoveDirectionChangedEventArgs> OnMoveDirectionChanged;
        event EventHandler<ItemButtonOneClickedEventArgs> OnItemButtonOneClicked;
        event EventHandler<ItemButtonTwoClickedEventArgs> OnItemButtonTwoClicked;
        event EventHandler<ItemButtonOneHeldEventArgs> OnItemButtonOneHeld;
        event EventHandler<ItemButtonTwoHeldEventArgs> OnItemButtonTwoHeld;
        

    }

}
