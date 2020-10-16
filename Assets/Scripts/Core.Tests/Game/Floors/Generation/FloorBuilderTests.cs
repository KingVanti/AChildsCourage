using NUnit.Framework;
using PADEAH.TestUtility;

namespace AChildsCourage.Game.Floors.Generation
{

    [TestFixture]
    public class FloorBuilderTests
    {

        #region Tests

        [Test]
        public void Given_Any_Builder_When_Ground_Is_Placed_Then_An_Event_Is_Raised()
        {
            // Given

            var builder = new FloorBuilder();

            // When

            var position = new TilePosition(1, 1);
            var raisedArgs = builder.Capture<GroundPlacedEventArgs>(
                () => builder.PlaceGround(position, new RoomBuildingSession()));

            // Then

            Assert.That(raisedArgs, Is.Not.Null, "No event was raised!");
            Assert.That(raisedArgs.Position, Is.EqualTo(position), "Event raised with incorrect position!");
        }

        [Test]
        public void Given_Any_Builder_When_A_Wall_Is_Placed_Then_An_Event_Is_Raised()
        {
            // Given

            var builder = new FloorBuilder();

            // When

            var position = new TilePosition(1, 1);
            var raisedArgs = builder.Capture<WallPlacedEventArgs>(
                () => builder.PlaceWall(position, new RoomBuildingSession()));

            // Then

            Assert.That(raisedArgs, Is.Not.Null, "No event was raised!");
            Assert.That(raisedArgs.Position, Is.EqualTo(position), "Event raised with incorrect position!");
        }

        #endregion

    }

}