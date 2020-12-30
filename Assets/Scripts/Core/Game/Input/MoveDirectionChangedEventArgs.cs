using System;
using UnityEngine;

namespace AChildsCourage.Game.Input
{

    internal class MoveDirectionChangedEventArgs : EventArgs
    {

        internal Vector2 MoveDirection { get; }


        internal MoveDirectionChangedEventArgs(Vector2 moveDirection) =>
            MoveDirection = moveDirection;

    }

}