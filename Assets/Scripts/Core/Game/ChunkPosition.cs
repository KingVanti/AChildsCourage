using AChildsCourage.Game.Floors.Generation;
using System;

namespace AChildsCourage.Game
{

    public readonly struct ChunkPosition
    {

        #region Constants

        public const int ChunkTileSize = 41;
        public const int PaddingThickness = 1;
        public const int MaxCoordinate = 5;

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

        internal TileOffset GetTileOffset()
        {
            return new TileOffset(
                ChunkTileSize * X + PaddingThickness,
                ChunkTileSize * Y + PaddingThickness);
        }


        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        #endregion

        #region Operators

        public static ChunkPosition operator +(ChunkPosition position, Passages direction)
        {
            switch (direction)
            {
                case Passages.North:
                    return new ChunkPosition(position.X, position.Y + 1);
                case Passages.East:
                    return new ChunkPosition(position.X + 1, position.Y);
                case Passages.South:
                    return new ChunkPosition(position.X, position.Y - 1);
                case Passages.West:
                    return new ChunkPosition(position.X - 1, position.Y);
            }

            throw new Exception("Invalid direction!");
        }

        public static bool operator ==(ChunkPosition p1, ChunkPosition p2)
        {
            return p1.X == p2.X && p1.Y == p2.Y;
        }

        public static bool operator !=(ChunkPosition p1, ChunkPosition p2)
        {
            return p1.X != p2.X || p1.Y != p2.Y;
        }

        #endregion

    }

}