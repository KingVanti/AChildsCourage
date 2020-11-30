using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace AChildsCourage.Game.Monsters.Navigation
{

    public class InvestigationHistory : IEnumerable<CompletedInvestigation>
    {


        private readonly ImmutableList<CompletedInvestigation> completedInvestigations;

        public static InvestigationHistory Empty => throw new NotImplementedException();

        public IEnumerator<CompletedInvestigation> GetEnumerator() => completedInvestigations.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();


        public static InvestigationHistory Add(InvestigationHistory history, CompletedInvestigation investigation) => throw new NotImplementedException();

    }

}