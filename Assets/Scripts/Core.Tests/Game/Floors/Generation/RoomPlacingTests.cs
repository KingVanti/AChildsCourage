using NUnit.Framework;
using System.Linq;
using System.Numerics;
using static AChildsCourage.Game.Floors.FloorGeneration;

namespace AChildsCourage.Game.Floors
{

    [TestFixture]
    public class RoomPlacingTests
    {

        #region Tests

        [Test]
        public void Get_Correct_Surrounding_Positions()
        {
            // Given

            var position = new ChunkPosition(0, 0);

            // When

            var surrounding = position.GetSurroundingPositions().ToArray();

            // Then

            Assert.That(surrounding.Length, Is.EqualTo(4), "Wrong number of positions!");
            Assert.That(surrounding.Distinct().Count(), Is.EqualTo(surrounding.Length), "Positions should be destinct!");

            for (var i = 0; i < 4; i++)
            {
                var p = surrounding[i];

                var diff = new Vector2(p.X - position.X, p.Y - position.Y);

                Assert.That(diff.Length(), Is.EqualTo(1), "Distance of position to origin should be 1!");
            }
        }

        [Test]
        public void When_A_Room_Is_Placed_Then_It_Is_Added_To_The_FloorBuilder()
        {
            // Given

            var builder = new FloorPlanBuilder();

            // When

            var position = new ChunkPosition(0, 0);
            var room = new RoomPassages();
            builder.PlaceRoom(position, room);

            // Then

            Assert.That(builder.RoomsByChunks.ContainsKey(position), "Room was not placed!");
            Assert.That(builder.RoomsByChunks[position], Is.EqualTo(room), "Incorrect room was placed!");
        }

        [Test]
        public void When_A_Room_Is_Placed_Then_Its_Position_Is_Removed_From_Reserved_Chunks()
        {
            // Given

            var position = new ChunkPosition(0, 0);

            var builder = new FloorPlanBuilder();
            builder.ReservedChunks.Add(position);

            // When

            builder.PlaceRoom(position, new RoomPassages());

            // Then

            Assert.That(builder.ReservedChunks.Contains(position), Is.False, "Position was not removed!");
        }

        [Test]
        public void When_A_Room_Is_Placed_Then_The_Surrounding_Positions_Are_Reserved()
        {
            // Given

            var position = new ChunkPosition(0, 0);

            var builder = new FloorPlanBuilder();

            // When

            builder.PlaceRoom(position, new RoomPassages(0, ChunkPassages.All));

            // Then

            var expectedReserved = new[]
            {
                new ChunkPosition(0, 1),
                new ChunkPosition(1, 0),
                new ChunkPosition(0, -1),
                new ChunkPosition(-1, 0)
            };
            Assert.That(builder.ReservedChunks, Is.EqualTo(expectedReserved), "Incorrect positions were reserved!");
        }


        [Test]
        public void A_Position_Cannot_Be_Reserved_If_It_Is_Already_Reserved()
        {
            // Given

            var position = new ChunkPosition(0, 0);

            var builder = new FloorPlanBuilder();
            builder.ReservedChunks.Add(position);

            // When

            var canReserve = builder.CanReserve(position);

            // Then

            Assert.That(canReserve, Is.False, "Should not be able to reserve position that is already reserved!");
        }

        [Test]
        public void A_Position_Cannot_Be_Reserved_If_It_Is_Already_Occupied()
        {
            // Given

            var position = new ChunkPosition(0, 0);

            var builder = new FloorPlanBuilder();
            builder.RoomsByChunks.Add(position, new RoomPassages());

            // When

            var canReserve = builder.CanReserve(position);

            // Then

            Assert.That(canReserve, Is.False, "Should not be able to reserve position that is already occupied!");
        }

        [Test]
        public void A_Position_Cannot_Be_Reserved_If_It_Is_Not_Connected_To_Any_Room()
        {
            // Given

            var position = new ChunkPosition(0, 0);

            var builder = new FloorPlanBuilder();

            // When

            var canReserve = builder.CanReserve(position);

            // Then

            Assert.That(canReserve, Is.False, "Should not be able to reserve position that is not connected to any room!");
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

        #endregion

    }

}