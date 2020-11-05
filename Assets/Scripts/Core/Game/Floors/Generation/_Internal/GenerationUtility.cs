using System.Collections.Generic;

namespace AChildsCourage.Game.Floors.Generation
{

    internal static class GenerationUtility
    {

        #region Fields

        private static PossibleConnection[] possibleConnections = new[]
        {
            new PossibleConnection(PassageDirection.North, PassageDirection.South),
            new PossibleConnection(PassageDirection.East, PassageDirection.West),
            new PossibleConnection(PassageDirection.South, PassageDirection.North),
            new PossibleConnection(PassageDirection.West, PassageDirection.East)
        };

        #endregion

        #region Methods

        internal static bool CanConnect(Passage p1, Passage p2)
        {
            return IndicesMatch(p1, p2) && DirectionsMatch(p1, p2);
        }

        private static bool IndicesMatch(Passage p1, Passage p2)
        {
            return p1.Index == p2.Index;
        }

        private static bool DirectionsMatch(Passage p1, Passage p2)
        {
            foreach (var connection in possibleConnections)
                if (p1.Direction == connection.First && p2.Direction == connection.Second)
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