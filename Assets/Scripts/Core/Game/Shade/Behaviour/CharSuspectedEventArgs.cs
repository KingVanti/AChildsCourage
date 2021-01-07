using System;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    public class CharSuspectedEventArgs : EventArgs
    {

        public Vector2 Position { get; }


        public CharSuspectedEventArgs(Vector2 position) =>
            Position = position;

    }

}