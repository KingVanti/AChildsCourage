﻿using AChildsCourage.Game.Floors.Generation;
using System;

namespace AChildsCourage.Game
{

    public readonly struct ChunkPosition
    {

        #region Constants

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

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        #endregion

        #region Operators

        public static ChunkPosition operator +(ChunkPosition position, PassageDirection direction)
        {
            switch (direction)
            {
                case PassageDirection.North:
                    return new ChunkPosition(position.X, position.Y + 1);
                case PassageDirection.East:
                    return new ChunkPosition(position.X + 1, position.Y);
                case PassageDirection.South:
                    return new ChunkPosition(position.X, position.Y - 1);
                case PassageDirection.West:
                    return new ChunkPosition(position.X - 1, position.Y);
            }

            throw new Exception("Invalid direction!");
        }

        #endregion

    }

}