using NUnit.Framework;

namespace AChildsCourage.Game.Floors
{

    [TestFixture]
    public class TileTransformationTests
    {

        #region Tests

        [Test]
        public void Position_Is_Correctly_Transformed_Around_Chunk_Center()
        {
            // Given

            var position = new TilePosition(-1, 1);

            // When

            var offset = position.OffsetAround(new TilePosition(21, 21));

            // Then

            Assert.That(offset, Is.EqualTo(new TilePosition(20, 22)), "Position incorrectly offset!");
        }


        [Test]
        public void Position_Is_Correctly_YMirrored_Around_Chunk_Center()
        {
            // Given

            var position = new TilePosition(22, 10);

            // When

            var mirrored = position.YMirrorOver(new TilePosition(21, 21));

            // Then

            Assert.That(mirrored, Is.EqualTo(new TilePosition(22, 32)), "Position incorrectly mirrored!");
        }


        [Test]
        public void Position_Is_Correctly_Rotated_Around_Chunk_Center()
        {
            // Given

            var position = new TilePosition(22, 22);

            // When

            var rotated = position.RotateClockwiseAround(new TilePosition(21, 21));

            // Then

            Assert.That(rotated, Is.EqualTo(new TilePosition(22, 20)), "Position incorrectly cotated!");
        }


        [Test]
        public void Creating_A_Ground_Tile_With_A_New_Position_Changes_Its_Position()
        {
            // Given

            var tile = new GroundTile(0, 0, 0, 0);

            // When

            var newtile = tile.With(new TilePosition(1, 1));

            // When

            Assert.That(newtile.Position, Is.EqualTo(new TilePosition(1, 1)), "Position should change!");
        }

        [Test]
        public void Creating_A_Ground_Tile_With_A_New_Position_Does_Not_Change_Its_Other_Properties()
        {
            // Given

            var tile = new GroundTile(0, 0, 0, 0);

            // When

            var newtile = tile.With(new TilePosition(1, 1));

            // When

            Assert.That(newtile.DistanceToWall, Is.EqualTo(tile.DistanceToWall), "Distance to wall should not change!");
            Assert.That(newtile.AOIIndex, Is.EqualTo(tile.AOIIndex), "AOI index should not change!");
        }

        #endregion

    }

}