using Moq;
using NUnit.Framework;

namespace AChildsCourage.Game.Floors.Generation
{

    [TestFixture]
    public class RoomBuilderTests 
    {

        #region Data

        private static TilePosition TestPosition { get; } = new TilePosition(1, 1);

        private static RoomBuildingSession TestSession { get { return new RoomBuildingSession(); } }

        #endregion

        #region Tests

        [Test]
        public void Given_Any_RoomBuilder_When_Floor_Is_Placed_Then_A_Floor_Tile_Is_Placed()
        {
            // Given

            var mockTilePlacer = new Mock<ITilePlacer>();
            var builder = new RoomBuilder(mockTilePlacer.Object);

            // When

            builder.PlaceFloor(TestPosition, TestSession);

            // Then

            mockTilePlacer.Verify(p => p.Place(TileType.Floor, TestPosition));
        }

        #endregion

    }

}