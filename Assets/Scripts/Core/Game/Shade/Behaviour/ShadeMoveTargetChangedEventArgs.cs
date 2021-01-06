using System;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    public class ShadeMoveTargetChangedEventArgs : EventArgs
    {

        public Vector2? NewTargetPosition { get; }


        public ShadeMoveTargetChangedEventArgs(Vector2? newTargetPosition) =>
            NewTargetPosition = newTargetPosition;

    }

}