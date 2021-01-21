using System;
using UnityEngine;

namespace AChildsCourage.Game.Char
{

    public class CharPositionChangedEventArgs : EventArgs
    {

        internal Vector2 NewPosition { get; }


        internal CharPositionChangedEventArgs(Vector2 newPosition) =>
            NewPosition = newPosition;

    }

}