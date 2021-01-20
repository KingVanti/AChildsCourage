using System;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    public readonly struct ShadeHead
    {

        internal Vector2 Position { get; }

        internal Vector2 Direction { get; }

        internal Func<Vector2, Vector2, bool> ObstacleExistsBetween { get; }


        internal ShadeHead(Vector2 position, Vector2 direction, Func<Vector2, Vector2, bool> obstacleExistsBetween)
        {
            Position = position;
            Direction = direction;
            ObstacleExistsBetween = obstacleExistsBetween;
        }

    }

}