using AChildsCourage.Game.Shade.Navigation;
using static AChildsCourage.Game.MEntityPosition;
using static AChildsCourage.Game.MTilePosition;
using static AChildsCourage.Game.Shade.Navigation.MInvestigation;
using static AChildsCourage.MRng;

namespace AChildsCourage.Game.Shade
{

    public class InvestigationBehaviour
    {

        private readonly CreateRng rng = Random();

        private Investigation currentInvestigation;


        public TilePosition CurrentTargetTile { get; private set; }

        public bool InvestigationIsInProgress => !IsComplete(currentInvestigation);


        public void StartNewInvestigation(FloorState floorState, ShadeState shadeState)
        {
            currentInvestigation = StartNew(floorState, shadeState, rng);
            UpdateTargetTile(shadeState.Position);
        }

        private void UpdateTargetTile(EntityPosition position) => CurrentTargetTile = NextTarget(currentInvestigation, position);

        
        public void ProgressInvestigation(TilesInView tilesInView)
        {
            currentInvestigation = Progress(currentInvestigation, tilesInView);
        }

        
        public CompletedInvestigation CompleteInvestigation() => Complete(currentInvestigation);

    }

}