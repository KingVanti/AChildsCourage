using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace AChildsCourage.Game.Shade.Navigation
{

    public readonly struct InvestigationHistory : IEnumerable<CompletedInvestigation>
    {

        public static InvestigationHistory AddToHistory(CompletedInvestigation investigation, InvestigationHistory history) =>
            new InvestigationHistory(history.CompletedInvestigations.Add(investigation));

        public static CompletedInvestigation? FindInHistory(AoiIndex index, InvestigationHistory history) =>
            history.CompletedInvestigations.FindLast(i => i.AoiIndex == index);

        public static InvestigationHistory EmptyInvestigationHistory => new InvestigationHistory(ImmutableList<CompletedInvestigation>.Empty);


        public ImmutableList<CompletedInvestigation> CompletedInvestigations { get; }


        public InvestigationHistory(params CompletedInvestigation[] completedInvestigations) => CompletedInvestigations = ImmutableList.Create(completedInvestigations);

        public InvestigationHistory(ImmutableList<CompletedInvestigation> completedInvestigations) => CompletedInvestigations = completedInvestigations;


        public IEnumerator<CompletedInvestigation> GetEnumerator() => CompletedInvestigations.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }

}