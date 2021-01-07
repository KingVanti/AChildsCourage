using UnityEngine;

namespace AChildsCourage.Game.Floors
{

    public readonly struct FloorDimensions
    {

        public TilePosition LowerRight { get; }

        public TilePosition UpperLeft { get; }

        public int Width => UpperLeft.X - LowerRight.X + 1;

        public int Height => UpperLeft.Y - LowerRight.Y + 1;

        public Vector2 Center => new Vector3(LowerRight.X + Width / 2f, LowerRight.Y + Height / 2f);


        public FloorDimensions(TilePosition lowerRight, TilePosition upperLeft)
        {
            LowerRight = lowerRight;
            UpperLeft = upperLeft;
        }

    }

}