namespace AChildsCourage.Game.Monsters.Navigation
{

    public readonly struct MonsterState
    {

        

        public static MonsterState Create(EntityPosition position, InvestigationHistory history)
        {
            return  new MonsterState(position, history);
        }
        

        private EntityPosition Position { get; }

        private InvestigationHistory InvestigationHistory { get; }

        
        private MonsterState(EntityPosition position, InvestigationHistory investigationHistory)
        {
            Position = position;
            InvestigationHistory = investigationHistory;
        }
        
    }

}