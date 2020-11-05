using System.Collections.Generic;

namespace AChildsCourage.Game.Floors.Generation
{

    internal static class GenerationUtility
    {

        #region Fields

        private static PossibleConnection[] possibleConnections = new[]
        {
            new PossibleConnection(Passages.North, Passages.South),
            new PossibleConnection(Passages.East, Passages.West),
            new PossibleConnection(Passages.South, Passages.North),
            new PossibleConnection(Passages.West, Passages.East)
        };

        #endregion

        #region Methods

        internal static bool CanConnect(Passages p1, Passages p2)
        {
            foreach (var connection in possibleConnections)
                if (p1 == connection.First && p2 == connection.Second)
                    return true;

            return false;
        }


        internal static IEnumerable<ChunkPosition> GetSurroundingPositions(ChunkPosition position)
        {
            yield return new ChunkPosition(position.X, position.Y + 1);
            yield return new ChunkPosition(position.X + 1, position.Y);
            yield return new ChunkPosition(position.X, position.Y - 1);
            yield return new ChunkPosition(position.X - 1, position.Y);
        }

        #endregion

    }

}