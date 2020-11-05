using System;
using UnityEngine;

namespace AChildsCourage.Game.Input
{
    public class MousePositionChangedEventArgs : EventArgs
    {

        #region Properties

        public Vector2 MousePosition
        {
            get;
        }

        #endregion

        #region Constructors

        public MousePositionChangedEventArgs(Vector2 mousePosition)
        {
            MousePosition = mousePosition;
        }

        #endregion

    }

}