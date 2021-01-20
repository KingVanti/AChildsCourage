using System;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    public class CharSpottedEventArgs : EventArgs
    {

        internal Vector2 Position { get; }


        internal CharSpottedEventArgs(Vector2 position) =>
            Position = position;

    }

}