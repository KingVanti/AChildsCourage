using System.Collections.Immutable;

namespace AChildsCourage.Game.Monsters.Navigation
{

    internal class InvestigationInProgress
    {

        private AOI AOI { get; }

        private TilePosition TargetPosition { get; }

        private ImmutableList<TilePosition> InvestigatedPositions { get; }

    }

}