using System.Collections.Generic;

namespace AChildsCourage.Game.Monsters.Navigation
{

    public delegate InvestigationInProgress StartInvestigation(FloorAOIs aois, MonsterState monsterState);

    internal delegate AOI ChooseNextAOI(FloorAOIs aois, MonsterState monsterState);

    public delegate InvestigationInProgress ProgressInvestigation(InvestigationInProgress investigation, IEnumerable<TilePosition> investigatedPositions);

    public delegate bool InvestigationIsComplete(InvestigationInProgress investigation);

    public delegate CompletedInvestigation CompleteInvestigation(InvestigationInProgress investigation);

}