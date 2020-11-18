﻿using NUnit.Framework;
using static AChildsCourage.Game.Floors.FloorGeneration;

namespace AChildsCourage.Game.Floors
{

    [TestFixture]
    public class FloorPlanBuilderTests
    {

        #region Methods

        [Test]
        public void When_A_Builders_ReservedChunks_List_Is_Not_Empty_Then_It_Has_Reserved_Chunks_()
        {
            // Given

            var builder = new FloorPlanBuilder();
            builder.ReservedChunks.Add(new ChunkPosition(0, 0));

            // When

            var hasReserved = builder.HasReservedChunks();

            // Then

            Assert.That(hasReserved, Is.True, "Builder should have reserved chunks!");
        }

        [Test]
        public void When_A_Builders_ReservedChunks_List_Is_Empty_Then_It_Has_No_Reserved_Chunks_()
        {
            // Given

            var builder = new FloorPlanBuilder();

            // When

            var hasReserved = builder.HasReservedChunks();

            // Then

            Assert.That(hasReserved, Is.False, "Builder should have no reserved chunks!");
        }


        [Test]
        public void Room_Count_Is_Calculated_Correctly()
        {
            // Given

            var builder = new FloorPlanBuilder();

            builder.PlaceRoom(new ChunkPosition(0, 0), new RoomPassages());
            builder.PlaceRoom(new ChunkPosition(1, 0), new RoomPassages());

            // When

            var roomCount = builder.CountRooms();

            // Then

            Assert.That(roomCount, Is.EqualTo(2), "Room-count calculated incorrectly!");
        }


        [Test]
        public void When_There_Is_No_Room_At_A_Given_Chunk_Then_It_Is_Empty()
        {
            // Given

            var builder = new FloorPlanBuilder();

            // When

            var isEmpty = builder.IsEmpty(new ChunkPosition(0, 0));

            // Then

            Assert.That(isEmpty, Is.True, "The chunk should be empty!");
        }

        [Test]
        public void When_There_Is_A_Room_At_A_Given_Chunk_Then_It_Is_Not_Empty()
        {
            // Given

            var builder = new FloorPlanBuilder();
            builder.PlaceRoom(new ChunkPosition(0, 0), new RoomPassages());

            // When

            var isEmpty = builder.IsEmpty(new ChunkPosition(0, 0));

            // Then

            Assert.That(isEmpty, Is.False, "The chunk should not be empty!");
        }


        [Test]
        public void Reserved_Chunks_Are_Correctly_Identified()
        {
            // Given

            var position = new ChunkPosition(0, 0);

            var builder = new FloorPlanBuilder();
            builder.ReservedChunks.Add(position);

            // When

            bool isReserved = builder.HasReserved(position);

            // Then

            Assert.That(isReserved, Is.True, "Room should be reserved!");
        }


        [Test]
        public void Unreserved_Chunks_Are_Correctly_Identified()
        {
            // Given

            var builder = new FloorPlanBuilder();

            // When

            bool isReserved = builder.HasReserved(new ChunkPosition(0, 0));

            // Then

            Assert.That(isReserved, Is.False, "Room should not be reserved!");
        }

        #endregion

    }

}