using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace AChildsCourage.Game.Monsters.Navigation
{

    public static class MInvestigationHistory
    {

        #region Types

        public delegate InvestigationHistory AddInvestigation(InvestigationHistory history, CompletedInvestigation investigation);

        public delegate CompletedInvestigation? FindMostRecentAOIInvestigation(InvestigationHistory history, AOIIndex index);

        public class InvestigationHistory : IEnumerable<CompletedInvestigation>
        {

            public ImmutableList<CompletedInvestigation> CompletedInvestigations { get; }


            public InvestigationHistory(params CompletedInvestigation[] completedInvestigations) => CompletedInvestigations = ImmutableList.Create(completedInvestigations);

            public InvestigationHistory(ImmutableList<CompletedInvestigation> completedInvestigations) => CompletedInvestigations = completedInvestigations;


            public IEnumerator<CompletedInvestigation> GetEnumerator() => CompletedInvestigations.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        }

        #endregion

        #region Functions

        public static InvestigationHistory Add(this InvestigationHistory history, CompletedInvestigation investigation) => AddToHistory(history, investigation);

        public static AddInvestigation AddToHistory =>
            (history, investigation) =>
                new InvestigationHistory(history.CompletedInvestigations.Add(investigation));


        public static CompletedInvestigation? FindLastIn(this InvestigationHistory history, AOIIndex index) => FindInHistory(history, index);

        public static FindMostRecentAOIInvestigation FindInHistory =>
            (history, index) =>
                history.CompletedInvestigations.FindLast(i => i.AOIIndex == index);


        public static InvestigationHistory Empty => new InvestigationHistory();

        #endregion

    }

}