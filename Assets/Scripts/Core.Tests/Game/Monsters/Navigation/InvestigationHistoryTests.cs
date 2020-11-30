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

    }

}