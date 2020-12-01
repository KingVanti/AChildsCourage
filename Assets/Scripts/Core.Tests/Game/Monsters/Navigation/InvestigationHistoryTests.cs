using System;
using System.Linq;
using NUnit.Framework;
using static AChildsCourage.Game.Monsters.Navigation.InvestigationHistory;

namespace AChildsCourage.Game.Monsters.Navigation
{

    [TestFixture]
    public class InvestigationHistoryTests
    {

        [Test]
        public void Empty_InvestigationHistory_Has_No_Entries()
        {
            // Given

            var empty = Empty;

            // When

            var count = empty.Count();

            // Then

            Assert.That(count, Is.Zero, "History was not empty!");
        }


        [Test]
        public void Adding_An_Investigation_To_A_History_Increases_The_Count_By_One()
        {
            // Given

            var history = Empty;

            // When

            var added = Add(history, new CompletedInvestigation());

            // Then

            Assert.That(added.Count(), Is.EqualTo(1), "Investigation not added!");
        }
        
        [Test]
        public void Adding_An_Investigation_To_A_History_Adds_The_Correct_Investigation()
        {
            // Given

            var history = Empty;

            // When

            var investigation = new CompletedInvestigation((AOIIndex) 1, DateTime.Now);
            var added = Add(history, investigation);

            // Then

            Assert.That(added.Last(), Is.EqualTo(investigation), "Incorrect investigation added!");
        }

    }

}