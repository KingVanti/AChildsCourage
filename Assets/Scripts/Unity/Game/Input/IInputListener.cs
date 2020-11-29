using System;

namespace AChildsCourage.Game.Input
{

    internal interface IInputListener
    {

        event EventHandler<MousePositionChangedEventArgs> OnMousePositionChanged;

        event EventHandler<MoveDirectionChangedEventArgs> OnMoveDirectionChanged;

        event EventHandler<EquippedItemUsedEventArgs> OnEquippedItemUsed;

        event EventHandler<ItemPickedUpEventArgs> OnItemPickedUp;

        event EventHandler<ItemSwappedEventArgs> OnItemSwapped;

    }

}