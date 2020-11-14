using AChildsCourage.Game.Floors;
using System;

namespace AChildsCourage.Game
{

    public readonly struct ChunkPosition
    {

        #region Constants

        public const int ChunkTileSize = 41;
        public const int PaddingThickness = 1;

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


        public override bool Equals(object obj)
        {
            return obj is ChunkPosition position &&
                   X == position.X &&
                   Y == position.Y;
        }


        public override int GetHashCode()
        {
            int hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
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