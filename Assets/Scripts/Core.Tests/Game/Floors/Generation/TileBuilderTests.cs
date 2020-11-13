using NUnit.Framework;
using PADEAH.Tabs;

namespace AChildsCourage.Game.Floors.Generation
{

    [TestFixture]
    public class TileBuilderTests
    {

        #region Tests

        [Test]
        public void When_A_Wall_Is_Placed_Then_An_Event_With_The_Correct_Information_Is_Raised()
        {
            // Given

            var tileBuilder = new TileBuilder();

            // When

            var wall = new Wall(new TilePosition(0, 0), WallType.Side);
            var eventArgs = ListenFor
                .First<WallPlacedEventArgs>()
                .From(tileBuilder)
                .During(() => tileBuilder.PlaceWall(wall));

            // Then

            Assert.That(eventArgs, Is.Not.Null, "No event was raised!");
            Assert.That(eventArgs.Wall.Position, Is.EqualTo(wall.Position), "Event raised with incorrect position!");
        }

        [Test]
        public void When_Ground_Is_Placed_Then_An_Event_With_The_Correct_Information_Is_Raised()
        {
            // Given

            var tileBuilder = new TileBuilder();

            // When

            var position = new TilePosition(0, 0);
            var eventArgs = ListenFor
                .First<GroundPlacedEventArgs>()
                .From(tileBuilder)
                .During(() => tileBuilder.PlaceGround(position));

            // Then

            Assert.That(eventArgs, Is.Not.Null, "No event was raised!");
            Assert.That(eventArgs.Position, Is.EqualTo(position), "Event raised with incorrect position!");
        }

        [Test]
        public void When_The_Tiles_For_A_Floor_Are_Built_Then_All_Ground_Tiles_Are_Placed()
        {
            // Given

            var tileBuilder = new TileBuilder();
            var floor = new Floor();
            floor.PlaceGround(new GroundTile(0, 0, 0, 0), new TileOffset(0, 0));

            // When

            var eventArgs =
                ListenFor
                .All<GroundPlacedEventArgs>()
                .From(tileBuilder)
                .During(() => tileBuilder.PlacesTilesFor(floor));

            // Assert

            Assert.That(eventArgs.Length, Is.EqualTo(1), "Incorrect number of tiles placed!");
        }

        [Test]
        public void When_The_Tiles_For_A_Floor_Are_Built_Then_All_Wall_Tiles_Are_Placed()
        {
            // Given

            var tileBuilder = new TileBuilder();
            var floor = new Floor();
            floor.PlaceGround(new GroundTile(0, 0, 0, 0), new TileOffset(0, 0));
            floor.GenerateWalls();

            // When

            var eventArgs =
                ListenFor
                .All<WallPlacedEventArgs>()
                .From(tileBuilder)
                .During(() => tileBuilder.PlacesTilesFor(floor));

            // Assert

            Assert.That(eventArgs.Length, Is.EqualTo(14), "Incorrect number of tiles placed!");
        }

        #endregion

    }

}