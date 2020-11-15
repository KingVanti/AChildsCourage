using NUnit.Framework;
using System.Linq;
using static AChildsCourage.Game.Floors.FloorGenerationModule;

namespace AChildsCourage.Game.Floors
{

    [TestFixture]
    public class FloorPlanBuilderTests
    {

        #region Methods

        [Test]
        public void The_Builder_Needs_Rooms_While_The_Number_Of_Built_Rooms_Is_Smaller_Than_The_Goal_Room_Count()
        {
            // Given

            var builder = new FloorPlanBuilder();

            // When

            var needsMoreRooms =
                Enumerable.Range(0, GoalRoomCount - 1)
                .Select(i =>
                {
                    builder.PlaceRoom(new ChunkPosition(i, 0), new RoomPassages(0, ChunkPassages.None));

                    return builder.NeedsMoreRooms();
                });

            // Then

            Assert.That(needsMoreRooms, Is.All.True, "The builder should need rooms!");
        }

        [Test]
        public void The_Builder_Needs_No_Rooms_When_The_Number_Of_Built_Rooms_Is_Equal_To_The_Goal_Room_Count()
        {
            // Given

            var builder = new FloorPlanBuilder();

            for (int i = 0; i < GoalRoomCount; i++)
                builder.PlaceRoom(new ChunkPosition(i, 0), new RoomPassages(0, ChunkPassages.None));

            // When

            var needsMoreRooms = builder.NeedsMoreRooms();

            // Then

            Assert.That(needsMoreRooms, Is.False, "The builder should not need rooms!");
        }


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
        public void When_A_Builder_Has_No_Rooms_Built_Then_It_Is_In_The_StartRoom_Phase()
        {
            // Given

            var builder = new FloorPlanBuilder();

            // When

            var phase = builder.GetCurrentPhase();

            // Then

            Assert.That(phase, Is.EqualTo(GenerationPhase.StartRoom), "Builder should be in StartRoom phase!");
        }

        [Test]
        public void When_A_Builder_Has_All_But_One_Rooms_Built_Then_It_Is_In_The_EndRoom_Phase()
        {
            // Given

            var builder = new FloorPlanBuilder();
            for (int i = 0; i < GoalRoomCount - 1; i++)
                builder.PlaceRoom(new ChunkPosition(i, 0), new RoomPassages(0, ChunkPassages.None));

            // When

            var phase = builder.GetCurrentPhase();

            // Then

            Assert.That(phase, Is.EqualTo(GenerationPhase.EndRoom), "Builder should be in EndRoom phase!");
        }

        [Test]
        public void When_A_Builder_Is_Neither_In_The_StartRoom_Nor_EndRoom_Phase_Then_It_Is_In_The_NormalRooms_Phase()
        {
            // Given

            var builder = new FloorPlanBuilder();

            // When

            var phases =
                Enumerable.Range(1, GoalRoomCount - 2)
                .Select(i =>
                {
                    builder.PlaceRoom(new ChunkPosition(i, 0), new RoomPassages(0, ChunkPassages.None));
                    return builder.GetCurrentPhase();
                });

            // Then

            Assert.That(phases, Is.All.EqualTo(GenerationPhase.NormalRooms), "Builder should be in NormalRoom phase!");
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


        [Test]
        public void Chunks_With_No_Passages_Are_Detected_Correctly()
        {
            // Given

            var builder = new FloorPlanBuilder();

            // When

            var hasPassages = builder.AnyPassagesLeadInto(new ChunkPosition(0, 0));

            // Then

            Assert.That(hasPassages, Is.False, "No passages lead into the chunk!");
        }


        [Test]
        public void Chunks_With_Passages_Are_Detected_Correctly()
        {
            // Given

            var builder = new FloorPlanBuilder();
            builder.PlaceRoom(new ChunkPosition(0, -1), new RoomPassages(0, new ChunkPassages(true, false, false, false)));

            // When

            var hasPassages = builder.AnyPassagesLeadInto(new ChunkPosition(0, 0));

            // Then

            Assert.That(hasPassages, Is.True, "Passages lead into the chunk!");
        }


        [Test]
        public void When_A_FloorPlan_Is_Created_Then_It_Has_The_Same_Rooms_As_The_Builder()
        {
            // Given

            var position = new ChunkPosition(0, 0);
            var room = new RoomPassages(1, ChunkPassages.None);

            var builder = new FloorPlanBuilder();
            builder.PlaceRoom(position,room );

            // When

            var floorPlan = builder.GetFloorPlan();

            // Then

            Assert.That(floorPlan.Rooms.Length, Is.EqualTo(1), "Incorrect number of rooms!");
            Assert.That(floorPlan.Rooms.First().RoomId, Is.EqualTo(room.RoomId), "Room id incorrectly copied!");
            Assert.That(floorPlan.Rooms.First().Position, Is.EqualTo(position), "Position incorrectly copied!");
        }

        #endregion

    }

}