using NUnit.Framework;
using static AChildsCourage.Game.ChunkCollection;

namespace AChildsCourage.Game
{

    [TestFixture]
    public class ChunkCollectionTests
    {

        [Test]
        public void Empty_Collection_Has_Empty_Bounds()
        {
            // Given

            var collection = EmptyChunkCollection;

            // When

            var bounds = collection.Map(GetBounds);

            // Then

            Assert.That(bounds, Is.EqualTo(IntBounds.emptyBounds), "Bounds should be empty!");
        }

    }

}