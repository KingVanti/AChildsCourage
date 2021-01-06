using System;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    public class ShadeLookTargetChangedEventArgs : EventArgs
    {

        public Vector2? NewTargetPosition { get; }


        public ShadeLookTargetChangedEventArgs(Vector2? newTargetPosition) =>
            NewTargetPosition = newTargetPosition;

    }

}