using NUnit.Framework;

namespace AChildsCourage.Game.Floors.Generation
{

    [TestFixture]
    public class ChunkGridTests
    {

        #region Tests

        [Test]
        public void RoomCount_Reflects_Current_Room_Count()
        {
            // Given

            var chunkGrid = new ChunkGrid();

            // When

            chunkGrid.Place(new RoomInfo(), new ChunkPosition(0, 0));
            chunkGrid.Place(new RoomInfo(), new ChunkPosition(1, 0));

            // Then

            Assert.That(chunkGrid.RoomCount, Is.EqualTo(2), "Incorrect room-count!");
        }


        [Test]
        public void Cannot_Place_Rooms_At_Occupied_Position()
        {
            // Given

            var chunkGrid = new ChunkGrid();
            chunkGrid.Place(new RoomInfo(), new ChunkPosition(0, 0));

            // When

            var canPlace = chunkGrid.CanPlaceAt(new ChunkPosition(0, 0));

            // Then

            Assert.That(canPlace, Is.False, "Should not be able to place!");
        }

        [Test]
        public void Cannot_Place_Rooms_In_Chunks_Without_Passages()
        {
            // Given

            var chunkGrid = new ChunkGrid();
            var passages = new ChunkPassages(null, new Passage(PassageDirection.East, PassageIndex.Second), null, null);
            chunkGrid.Place(new RoomInfo(0, passages), new ChunkPosition(0, 0));

            // When

            var canPlace = chunkGrid.CanPlaceAt(new ChunkPosition(2, 0));

            // Then

            Assert.That(canPlace, Is.False, "Should not be able to place!");
        }

        [Test]
        public void Can_Place_Rooms_At_Empty_Position_With_Passages()
        {
            // Given

            var chunkGrid = new ChunkGrid();
            var passages = new ChunkPassages(null, new Passage(PassageDirection.East, PassageIndex.Second), null, null);
            chunkGrid.Place(new RoomInfo(0, passages), new ChunkPosition(0, 0));

            // When

            var canPlace = chunkGrid.CanPlaceAt(new ChunkPosition(1, 0));

            // Then

            Assert.That(canPlace, Is.True, "Should be able to place!");
        }


        [Test]
        public void Can_Get_Correct_Passages_Into_Chunk()
        {
            // Given

            var chunkGrid = new ChunkGrid();

            var passages1 = new ChunkPassages(new Passage(PassageDirection.North, PassageIndex.Second), null, null, null);
            chunkGrid.Place(new RoomInfo(0, passages1), new ChunkPosition(0, -1));
            var passages2 = new ChunkPassages(null, null, null, new Passage(PassageDirection.West, PassageIndex.Second));
            chunkGrid.Place(new RoomInfo(1, passages2), new ChunkPosition(1, 0));

            // When

            var passages = chunkGrid.GetPassagesTo(new ChunkPosition(0, 0));

            // Then

            Assert.That(passages.Count, Is.EqualTo(2), "Incorrect passage count!");

            Assert.That(passages.HasNorth, Is.False, "North passage incorrectly found!");

            Assert.That(passages.HasEast, Is.True, "No east passage found!");
            Assert.That(passages.East.Value.Index, Is.EqualTo(PassageIndex.Second), "East passage-index incorrect!");

            Assert.That(passages.HasSouth, Is.True, "No south passage found!");
            Assert.That(passages.South.Value.Index, Is.EqualTo(PassageIndex.Second), "South passage-index incorrect!");

            Assert.That(passages.HasWest, Is.False, "West passage incorrectly found!");
        }

        #endregion

    }

}