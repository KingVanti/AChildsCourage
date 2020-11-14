using AChildsCourage.Game.Floors;
using System;

namespace AChildsCourage.Game
{

    public readonly struct ChunkPosition
    {

        #region Constants

        public const int ChunkTileSize = 41;

        #endregion

        #region Properties

        public int X { get; }

        public int Y { get; }

        #endregion

        #region Constructors

        public ChunkPosition(int x, int y)
        {
            X = x;
            Y = y;
        }

        #endregion

        #region Methods

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        #endregion

        #region Operators

        public static ChunkPosition operator +(ChunkPosition position, Passage direction)
        {
            switch (direction)
            {
                case Passage.North:
                    return new ChunkPosition(position.X, position.Y + 1);
                case Passage.East:
                    return new ChunkPosition(position.X + 1, position.Y);
                case Passage.South:
                    return new ChunkPosition(position.X, position.Y - 1);
                case Passage.West:
                    return new ChunkPosition(position.X - 1, position.Y);
            }

            throw new Exception("Invalid direction!");
        }

        #endregion

    }

}