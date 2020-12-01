using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace AChildsCourage.Game.Monsters.Navigation
{

    public class InvestigationHistory : IEnumerable<CompletedInvestigation>
    {

        public delegate InvestigationHistory AddInvestigation(InvestigationHistory history, CompletedInvestigation investigation);

        public delegate CompletedInvestigation? FindMostRecentAOIInvestigation(InvestigationHistory history, AOIIndex index);


        private readonly ImmutableList<CompletedInvestigation> completedInvestigations;


        public static AddInvestigation Add => (history, investigation) => new InvestigationHistory(history.completedInvestigations.Add(investigation));

        public static FindMostRecentAOIInvestigation FindInvestigation => (history, index) => history.completedInvestigations.FindLast(i => i.AOIIndex == index);


        public static InvestigationHistory Empty => new InvestigationHistory();


        public InvestigationHistory(params CompletedInvestigation[] completedInvestigations) => this.completedInvestigations = ImmutableList.Create(completedInvestigations);

        private InvestigationHistory(ImmutableList<CompletedInvestigation> completedInvestigations) => this.completedInvestigations = completedInvestigations;


        public IEnumerator<CompletedInvestigation> GetEnumerator() => completedInvestigations.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }

}