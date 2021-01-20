using UnityEngine;

namespace AChildsCourage.Game.Floors
{

    internal readonly struct FloorDimensions
    {

        internal TilePosition LowerRight { get; }

        private TilePosition UpperLeft { get; }

        internal int Width => UpperLeft.X - LowerRight.X + 1;

        internal int Height => UpperLeft.Y - LowerRight.Y + 1;

        internal Vector2 Center => new Vector3(LowerRight.X + Width / 2f, LowerRight.Y + Height / 2f);


        internal FloorDimensions(TilePosition lowerRight, TilePosition upperLeft)
        {
            LowerRight = lowerRight;
            UpperLeft = upperLeft;
        }

    }

}