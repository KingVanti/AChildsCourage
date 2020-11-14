using System;
using System.Collections.Generic;

namespace AChildsCourage.Game.Floors.Generation
{

    internal static class Generation
    {

        #region Methods

        internal static TileOffset GetTileOffsetFor(ChunkPosition chunkPosition)
        {
            return new TileOffset(
                chunkPosition.X * ChunkPosition.ChunkTileSize,
                chunkPosition.Y * ChunkPosition.ChunkTileSize);
        }


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

        #endregion

    }

}