using System;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    public class CharSpottedEventArgs : EventArgs
    {

        public Vector2 Position { get; }


        public CharSpottedEventArgs(Vector2 position) =>
            Position = position;

    }

}