using System;
using System.Collections.Generic;

namespace AChildsCourage.Game.Floors
{

    public static partial class FloorGeneration
    {

        internal static IEnumerable<ChunkPosition> GetSurroundingPositions(ChunkPosition position)
        {
            yield return new ChunkPosition(position.X, position.Y + 1);
            yield return new ChunkPosition(position.X + 1, position.Y);
            yield return new ChunkPosition(position.X, position.Y - 1);
            yield return new ChunkPosition(position.X - 1, position.Y);
        }


        internal static ChunkPosition MoveToAdjacentChunk(ChunkPosition position, PassageDirection direction)
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


        internal static PassageDirection Invert(this PassageDirection passage)
        {
            switch (passage)
            {
                case PassageDirection.North:
                    return PassageDirection.South;
                case PassageDirection.East:
                    return PassageDirection.West;
                case PassageDirection.South:
                    return PassageDirection.North;
                case PassageDirection.West:
                    return PassageDirection.East;
            }

            throw new Exception("Invalid direction");
        }

    }

}