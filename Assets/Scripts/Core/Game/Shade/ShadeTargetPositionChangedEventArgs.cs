using System;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    public class ShadeTargetPositionChangedEventArgs : EventArgs
    {

        public Vector2 NewTargetPosition { get; }


        public ShadeTargetPositionChangedEventArgs(Vector2 newTargetPosition) => NewTargetPosition = newTargetPosition;

    }

}