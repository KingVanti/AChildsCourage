using System.Linq;
using NUnit.Framework;
using static AChildsCourage.Game.MFloorGenerating;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game
{

    [TestFixture]
    public class CouragePickupCreatingTests
    {
/*
        [Test]
        public void The_Correct_Number_Of_Orb_Pickups_Are_Chosen()
        {
            // Given

            var positions = new[] { new TilePosition(0, 0), new TilePosition(1, 0), new TilePosition(2, 0), new TilePosition(3, 0), new TilePosition(4, 0), new TilePosition(5, 0) };

            // When

            var chosen = ChooseCourageOrbs(positions, 2);

            // Then

            Assert.That(chosen.Count(), Is.EqualTo(2), "Incorrect number of positions calculated!");
        }

        [Test]
        public void The_Orb_Pickups_Are_Ordered_By_Weight()
        {
            // Given

            var positions = new[] { new TilePosition(0, 0), new TilePosition(1, 0) };

            // When

            var chosen = ChooseCourageOrbs(positions, 2);

            // Then

            var expected = new[] { new TilePosition(1, 0), new TilePosition(0, 0) };
            Assert.That(chosen.Select(p => p.Position), Is.EqualTo(expected), "Positions incorrectly ordered!");
        }


        [Test]
        public void The_Orb_Position_With_The_Highest_Weight_Is_Chosen_Next()
        {
            // Given

            var positions = new[] { new TilePosition(1, 1), new TilePosition(2, 2) };

            // When

            var next = ChooseNextOrbPosition(positions, Enumerable.Empty<TilePosition>());

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
        public void The_Correct_Number_Of_Spark_Pickups_Are_Chosen()
        {
            // Given

            var positions = new[] { new TilePosition(0, 0), new TilePosition(1, 0), new TilePosition(2, 0), new TilePosition(3, 0), new TilePosition(4, 0), new TilePosition(5, 0) };

            // When

            var chosen = ChooseCourageSparks(positions, 2);

            // Then

            Assert.That(chosen.Count(), Is.EqualTo(2), "Incorrect number of positions calculated!");
        }

        [Test]
        public void The_Spark_Pickups_Are_Ordered_By_Weight()
        {
            // Given

            var positions = new[] { new TilePosition(0, 0), new TilePosition(1, 0) };

            // When

            var chosen = ChooseCourageSparks(positions, 2);

            // Then

            var expected = new[] { new TilePosition(1, 0), new TilePosition(0, 0) };
            Assert.That(chosen.Select(p => p.Position), Is.EqualTo(expected), "Positions incorrectly ordered!");
        }


        [Test]
        public void The_Spark_Position_With_The_Highest_Weight_Is_Chosen_Next()
        {
            // Given

            var positions = new[] { new TilePosition(1, 1), new TilePosition(2, 2) };

            // When

            var next = ChooseNextSparkPosition(positions, Enumerable.Empty<TilePosition>());

            // Then

            Assert.That(next, Is.EqualTo(new TilePosition(2, 2)), "Chose incorrect position!");
        }


        [Test]
        public void Courage_Spark_Weight_Is_Proportional_To_Distance_From_Origin()
        {
            // Given

            var position1 = new TilePosition(1, 0);
            var position2 = new TilePosition(2, 0);

            // When

            var weight1 = CalculateCourageSparkWeight(position1, Enumerable.Empty<TilePosition>());
            var weight2 = CalculateCourageSparkWeight(position2, Enumerable.Empty<TilePosition>());

            // Then

            Assert.That(weight1, Is.LessThan(weight2), "Weight 1 should be smaller than weight 2!");
        }

        [Test]
        public void Courage_Spark_Weight_Is_Inversly_Proportional_To_Minimum_Distance_To_Other_Sparks()
        {
            // Given

            var position1 = new TilePosition(1, 0);
            var position2 = new TilePosition(1, 0);

            // When

            var weight1 = CalculateCourageSparkWeight(position1, new[] { new TilePosition(2, 0) });
            var weight2 = CalculateCourageSparkWeight(position2, new[] { new TilePosition(3, 0) });

            // Then

            Assert.That(weight1, Is.GreaterThan(weight2), "Weight 1 should be larger than weight 2!");
        }
*/
    }

}