using NUnit.Framework;
using System.Linq;
using static AChildsCourage.Game.Floors.FloorTilesBuilding;

namespace AChildsCourage.Game.Floors
{

    [TestFixture]
    public class TileBuildingTests
    {

        #region Tests

        [Test]
        public void When_A_Ground_Tile_Is_Placed_Then_Its_Position_Is_Added_To_The_List_Of_Ground_Positions()
        {
            // Given

            var builder = new FloorTilesBuilder(Enumerable.Empty<TilePosition>(), Enumerable.Empty<TilePosition>());

            // When

            var tile = new GroundTile(0, 0, 0, 0);
            builder.PlaceGround(tile);

            // Then

            Assert.That(builder.GroundPositions.Count, Is.EqualTo(1), "Tile was not added!");
            Assert.That(builder.GroundPositions.First(), Is.EqualTo(tile.Position), "Different tile was added!");
        }

        #endregion

    }

}