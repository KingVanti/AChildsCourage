using System;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    public class CharSuspectedEventArgs : EventArgs
    {

        internal Vector2 Position { get; }


        internal CharSuspectedEventArgs(Vector2 position) =>
            Position = position;

    }

}