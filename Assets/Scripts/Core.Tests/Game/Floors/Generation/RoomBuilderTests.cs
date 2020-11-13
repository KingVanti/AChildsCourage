using Moq;
using NUnit.Framework;
using PADEAH.Tabs;

namespace AChildsCourage.Game.Floors.Generation
{

    [TestFixture]
    public class RoomBuilderTests
    {

        #region Tests

        [Test]
        public void When_A_Room_Is_Built_Then_All_Ground_Is_Placed()
        {
            // Given

            var roomTiles = new RoomTiles(
                new Tiles<GroundTile>(new GroundTile(new TilePosition(0, 0)), new GroundTile(new TilePosition(0, 0))),
                Tiles<DataTile>.None);
            var room = new Room(
                RoomType.Normal,
                roomTiles,
                ChunkPassages.None);

            var mockTileBuilder = new Mock<ITileBuilder>();

            var roombuilder = new RoomBuilder(mockTileBuilder.Object);

            // When

            roombuilder.Build(room, new ChunkPosition(0, 0));

            // Then

            mockTileBuilder.Verify(b => b.PlaceGround(It.IsAny<TilePosition>()), Times.Exactly(2), $"Incorrect number of ground placed!");
        }

        [Test]
        public void When_A_Room_Is_Built_Then_An_Event_Is_Raised()
        {
            // Given

            var room = Room.Empty;

            var mockTileBuilder = new Mock<ITileBuilder>();

            var roombuilder = new RoomBuilder(mockTileBuilder.Object);

            // When

            var raisedArgs = ListenFor
                .First<RoomBuiltEventArgs>()
                .From(roombuilder)
                .During(() => roombuilder.Build(room, new ChunkPosition(0, 0)));

            // Then

            Assert.That(raisedArgs, Is.Not.Null, "No was event raised!");
        }

        #endregion

    }

}