using System;
using UnityEngine;

namespace AChildsCourage.Game.Shade
{

    public readonly struct ShadeHead
    {

        public Vector2 Position { get; }

        public Vector2 Direction { get; }

        public Func<Vector2, Vector2, bool> ObstacleExistsBetween { get; }


        public ShadeHead(Vector2 position, Vector2 direction, Func<Vector2, Vector2, bool> obstacleExistsBetween)
        {
            Position = position;
            Direction = direction;
            ObstacleExistsBetween = obstacleExistsBetween;
        }

    }

}