using System;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    public class CharLostEventArgs : EventArgs
    {

        private Vector2 LastKnownPosition { get; }

        private Vector2 LastKnownVelocity { get; }


        public CharLostEventArgs(Vector2 lastKnownPosition, Vector2 lastKnownVelocity)
        {
            LastKnownPosition = lastKnownPosition;
            LastKnownVelocity = lastKnownVelocity;
        }

    }

}