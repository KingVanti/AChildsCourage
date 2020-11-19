using NUnit.Framework;
using System.Linq;
using static AChildsCourage.Game.NightManagement.Loading.CouragePositionChoosing;

namespace AChildsCourage.Game.NightManagement.Loading
{

    [TestFixture]
    public class CouragePositionChoosingTests
    {

        #region Tests

        [Test]
        public void The_Correct_Number_Of_Positions_Is_Chosen()
        {
            // Given

            var positions = new[]
            {
                new TilePosition(0, 0),
                new TilePosition(1, 0),
                new TilePosition(2, 0),
                new TilePosition(3, 0),
                new TilePosition(4, 0),
                new TilePosition(5, 0)
            };

            // When

            var chosen = ChooseCourageOrbPositions(positions, 2);

            // Then

            Assert.That(chosen.Count(), Is.EqualTo(2), "Incorrect number of positions calculated!");
        }

        [Test]
        public void The_Positions_Are_Ordered_By_Weight()
        {
            // Given

            var positions = new[]
            {
                new TilePosition(0, 0),
                new TilePosition(1, 0)
            };

            // When

            var chosen = ChooseCourageOrbPositions(positions, 2);

            // Then

            var expected = new[]
           {
                new TilePosition(1, 0),
                new TilePosition(0, 0)
            };
            Assert.That(chosen, Is.EqualTo(expected), "Positions incorrectly ordered!");
        }


        [Test]
        public void The_Position_With_The_Highest_Weight_Is_Chosen_Next()
        {
            // Given

            var positions = new[] { new TilePosition(1, 1), new TilePosition(2, 2) };

            // When

            var next = ChooseNext(positions, Enumerable.Empty<TilePosition>());

            // Then

            Assert.That(next, Is.EqualTo(new TilePosition(2, 2)), "Chose incorrect position!");
        }


        [Test]
        public void Courage_Orb_Weight_Is_Proportional_To_Distance_From_Origin()
        {
            // Given

            var position1 = new TilePosition(1, 0);
            var position2 = new TilePosition(2, 0);

            // When

            var weight1 = CalculateCourageOrbWeight(position1, Enumerable.Empty<TilePosition>());
            var weight2 = CalculateCourageOrbWeight(position2, Enumerable.Empty<TilePosition>());

            // Then

            Assert.That(weight1, Is.LessThan(weight2), "Weight 1 should be smaller than weight 2!");
        }

        [Test]
        public void Courage_Orb_Weight_Is_Proportional_To_Minimum_Distance_To_Other_Orbs()
        {
            // Given

            var position1 = new TilePosition(1, 0);
            var position2 = new TilePosition(1, 0);

            // When

            var weight1 = CalculateCourageOrbWeight(position1, new[] { new TilePosition(2, 0) });
            var weight2 = CalculateCourageOrbWeight(position2, new[] { new TilePosition(3, 0) });

            // Then

            Assert.That(weight1, Is.LessThan(weight2), "Weight 1 should be smaller than weight 2!");
        }


        [Test]
        public void Distance_From_Origin_Is_Caluclated_Correctly()
        {
            // Given

            var position = new TilePosition(2, 0);

            // When

            var distance = GetDistanceFromOrigin(position);

            // When

            Assert.That(distance, Is.EqualTo(2), "Incorrect distance calculated!");
        }


        [Test]
        public void Distance_Between_Positions_Is_Caluclated_Correctly()
        {
            // Given

            var p1 = new TilePosition(0, 0);
            var p2 = new TilePosition(2, 0);

            // When

            var distance = GetDistanceBetween(p1, p2);

            // When

            Assert.That(distance, Is.EqualTo(2), "Incorrect distance calculated!");
        }

        #endregion

    }

}