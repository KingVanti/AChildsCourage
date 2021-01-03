using System.Collections.Generic;
using AChildsCourage.Game.Shade.Navigation;
using static AChildsCourage.Game.Shade.Navigation.Investigation;
using static AChildsCourage.Rng;

namespace AChildsCourage.Game.Shade
{

    public class InvestigationBehaviour
    {

        private readonly CreateRng rng = RandomRng();


        private Investigation currentInvestigation;


        public TilePosition CurrentTargetTile { get; private set; }

        public bool InvestigationIsInProgress => !IsComplete(currentInvestigation);


        public void StartNewInvestigation(FloorState floorState, ShadeState shadeState)
        {
            currentInvestigation = StartNew(floorState, shadeState, rng);
            UpdateTargetTile(shadeState.Position);
        }

        private void UpdateTargetTile(EntityPosition position) =>
            CurrentTargetTile = NextTarget(currentInvestigation, position);

        public void ProgressInvestigation(ShadeState shadeState, IEnumerable<TilePosition> investigatedTiles)
        {
            currentInvestigation = Progress(currentInvestigation, investigatedTiles);
            UpdateTargetTile(shadeState.Position);
        }

        public CompletedInvestigation CompleteInvestigation() =>
            Complete(currentInvestigation);

    }

}