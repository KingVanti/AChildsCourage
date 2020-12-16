using System;
using UnityEngine;

namespace AChildsCourage.Game.Input
{

    internal class MoveDirectionChangedEventArgs : EventArgs
    {

        #region Properties

        internal Vector2 MoveDirection { get; }

        #endregion

        #region Constructors

        internal MoveDirectionChangedEventArgs(Vector2 moveDirection) => MoveDirection = moveDirection;

        #endregion

    }

}