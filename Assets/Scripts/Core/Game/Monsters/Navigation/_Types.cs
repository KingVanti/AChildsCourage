namespace AChildsCourage.Game.Monsters.Navigation
{

    internal delegate InvestigationInProgress StartInvestigation(FloorAOIs aois, MonsterState monsterState);

    internal delegate AOI ChooseNextAOI(FloorAOIs aois, MonsterState monsterState);

    internal delegate InvestigationInProgress ProgressInvestigation(InvestigationInProgress investigation, TilePosition investigatedPosition);

    internal delegate bool InvestigationIsComplete(InvestigationInProgress investigation);

    internal delegate CompletedInvestigation CompleteInvestigation(InvestigationInProgress investigation);

}