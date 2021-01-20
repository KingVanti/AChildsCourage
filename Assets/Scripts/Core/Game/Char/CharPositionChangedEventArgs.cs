using System;
using UnityEngine;

namespace AChildsCourage.Game.Char
{

    public class CharPositionChangedEventArgs : EventArgs
    {

        public Vector2 NewPosition { get; }


        public CharPositionChangedEventArgs(Vector2 newPosition) => 
            NewPosition = newPosition;

    }

}