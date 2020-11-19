using NUnit.Framework;
using static AChildsCourage.Game.NightManagement.Loading.TileTransforming;

namespace AChildsCourage.Game.NightManagement.Loading
{

    [TestFixture]
    public class TileTransformationTests
    {

        #region Tests

        [Test]
        public void Transforming_A_Position_Offsets_It()
        {
            // Given

            var position = new TilePosition(1, 1);
            var transform = new ChunkTransform(0, false, new TilePosition(0, 0), new TilePosition(20, 20));

            // When

            var transformed = Transform(position, transform);

            // Then 

            Assert.That(transformed, Is.EqualTo(new TilePosition(1, 1)), "Position not offset!");
        }

        [Test]
        public void Transforming_A_Position_Mirrors_It_Is_Mirroring_Is_Enabled()
        {
            // Given

            var position = new TilePosition(0, 1);
            var transform = new ChunkTransform(0, true, new TilePosition(0, 0), new TilePosition(20, 20));

            // When

            var transformed = Transform(position, transform);

            // Then 

            Assert.That(transformed, Is.EqualTo(new TilePosition(0, 39)), "Position not mirrored!");
        }

        [Test]
        public void Transforming_A_Position_Does_Not_Mirror_It_Is_Mirroring_Is_Not_Enabled()
        {
            // Given

            var position = new TilePosition(0, 1);
            var transform = new ChunkTransform(0, false, new TilePosition(0, 0), new TilePosition(20, 20));

            // When

            var transformed = Transform(position, transform);

            // Then 

            Assert.That(transformed, Is.EqualTo(new TilePosition(0, 1)), "Position mirrored!");
        }

        [Test]
        public void Rotating_A_Position_Rotates_It_If_Rotation_Count_Is_Greater_Than_0()
        {
            // Given

            var position = new TilePosition(0, 1);
            var transform = new ChunkTransform(1, false, new TilePosition(0, 0), new TilePosition(20, 20));

            // When

            var transformed = Transform(position, transform);

            // Then 

            Assert.That(transformed, Is.EqualTo(new TilePosition(1, 40)), "Position rotated incorrectly!");
        }

        [Test]
        public void Rotating_A_Position_Does_Not_Rotate_It_If_Rotation_Count_Is_0()
        {
            // Given

            var position = new TilePosition(0, 1);
            var transform = new ChunkTransform(0, false, new TilePosition(0, 0), new TilePosition(20, 20));

            // When

            var transformed = Transform(position, transform);

            // Then 

            Assert.That(transformed, Is.EqualTo(new TilePosition(0, 1)), "Position rotated!");
        }


        [Test]
        public void Position_Is_Correctly_Transformed_Around_Chunk_Center()
        {
            // Given

            var position = new TilePosition(-1, 1);

            // When

            var offset = OffsetAround(position, new TilePosition(21, 21));

            // Then

            Assert.That(offset, Is.EqualTo(new TilePosition(20, 22)), "Position incorrectly offset!");
        }


        [Test]
        public void Position_Is_Correctly_YMirrored_Around_Chunk_Center()
        {
            // Given

            var position = new TilePosition(22, 10);

            // When

            var mirrored = YMirrorOver(position, new TilePosition(21, 21));

            // Then

            Assert.That(mirrored, Is.EqualTo(new TilePosition(22, 32)), "Position incorrectly mirrored!");
        }


        [Test]
        public void Position_Is_Correctly_Rotated_Around_Chunk_Center()
        {
            // Given

            var position = new TilePosition(22, 22);

            // When

            var rotated = RotateClockwiseAround(position, new TilePosition(21, 21));

            // Then

            Assert.That(rotated, Is.EqualTo(new TilePosition(22, 20)), "Position incorrectly cotated!");
        }

        #endregion

    }

}