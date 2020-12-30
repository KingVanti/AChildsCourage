using System;
using UnityEngine;

namespace AChildsCourage.Game.Input
{

    internal class MousePositionChangedEventArgs : EventArgs
    {

        internal Vector2 MousePosition { get; }


        internal MousePositionChangedEventArgs(Vector2 mousePosition) => 
            MousePosition = mousePosition;

    }

}