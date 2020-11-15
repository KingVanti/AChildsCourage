using System;
using System.Collections.Generic;

namespace AChildsCourage.Game.Floors
{

    public static partial class FloorGenerationModule
    {

        #region Methods

        internal static IEnumerable<ChunkPosition> GetSurroundingPositions(ChunkPosition position)
        {
            yield return new ChunkPosition(position.X, position.Y + 1);
            yield return new ChunkPosition(position.X + 1, position.Y);
            yield return new ChunkPosition(position.X, position.Y - 1);
            yield return new ChunkPosition(position.X - 1, position.Y);
        }


        internal static ChunkPosition MoveToAdjacentChunk(ChunkPosition position, Passage direction)
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


        internal static Passage Invert(this Passage passage)
        {
            switch (passage)
            {
                case Passage.North:
                    return Passage.South;
                case Passage.East:
                    return Passage.West;
                case Passage.South:
                    return Passage.North;
                case Passage.West:
                    return Passage.East;
            }

            throw new Exception("Invalid direction");
        }

        #endregion

    }

}