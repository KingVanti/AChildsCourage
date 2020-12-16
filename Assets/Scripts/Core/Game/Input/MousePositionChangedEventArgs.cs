using System;
using UnityEngine;

namespace AChildsCourage.Game.Input
{

    internal class MousePositionChangedEventArgs : EventArgs
    {

        #region Properties

        internal Vector2 MousePosition { get; }

        #endregion

        #region Constructors

        internal MousePositionChangedEventArgs(Vector2 mousePosition) => MousePosition = mousePosition;

        #endregion

    }

}