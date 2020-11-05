using System;
using UnityEngine;

namespace AChildsCourage.Game.Input
{
    public class MoveDirectionChangedEventArgs : EventArgs
    {

        #region Properties

        public Vector2 MoveDirection
        {
            get;
        }

        #endregion

        #region Constructors

        public MoveDirectionChangedEventArgs(Vector2 moveDirection)
        {
            MoveDirection = moveDirection;
        }

        #endregion

    }

}
