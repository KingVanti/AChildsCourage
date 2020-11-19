using AChildsCourage.Game.Floors;
using NUnit.Framework;
using PADEAH.Tabs;

namespace AChildsCourage.Game.NightManagement.Loading
{

    [TestFixture]
    public class FloorRectrator
    {

        #region Tests
        
        [Test]
        public void When_A_Wall_Is_Placed_Then_An_Event_With_The_Correct_Information_Is_Raised()
        {
            // Given

            var recreator = new FloorRecreator();

            // When

            var wall = new Wall(new TilePosition(0, 0), WallType.Side);
            var eventArgs = ListenFor
                .First<WallPlacedEventArgs>()
                .From(recreator)
                .During(() => recreator.PlaceWall(wall));

            // Then

            Assert.That(eventArgs, Is.Not.Null, "No event was raised!");
            Assert.That(eventArgs.Wall.Position, Is.EqualTo(wall.Position), "Event raised with incorrect position!");
        }

        [Test]
        public void When_Ground_Is_Placed_Then_An_Event_With_The_Correct_Information_Is_Raised()
        {
            // Given

            var recreator = new FloorRecreator();

            // When

            var position = new TilePosition(0, 0);
            var eventArgs = ListenFor
                .First<GroundPlacedEventArgs>()
                .From(recreator)
                .During(() => recreator.PlaceGround(position));

            // Then

            Assert.That(eventArgs, Is.Not.Null, "No event was raised!");
            Assert.That(eventArgs.Position, Is.EqualTo(position), "Event raised with incorrect position!");
        }
        
        #endregion

    }

}