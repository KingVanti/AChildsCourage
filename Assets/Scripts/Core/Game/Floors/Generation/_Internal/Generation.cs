using System.Collections.Generic;

namespace AChildsCourage.Game.Floors.Generation
{

    internal static class Generation
    {

        #region Fields

        private static PossibleConnection[] possibleConnections = new[]
        {
            new PossibleConnection(Passage.North, Passage.South),
            new PossibleConnection(Passage.East, Passage.West),
            new PossibleConnection(Passage.South, Passage.North),
            new PossibleConnection(Passage.West, Passage.East)
        };

        #endregion

        #region Methods

        internal static bool CanConnect(Passage p1, Passage p2)
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