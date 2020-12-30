using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using AChildsCourage.Game.Floors.RoomPersistence;
using static AChildsCourage.Game.MFloorGenerating.MFloorLayout;
using static AChildsCourage.F;
using static AChildsCourage.Game.MFloorGenerating.MRoomChoosing;
using static AChildsCourage.Game.MFloorGenerating.MRoomPassageGenerating;
using static AChildsCourage.MRng;

namespace AChildsCourage.Game
{

    public static partial class MFloorGenerating
    {

        public static class MFloorPlanGenerating
        {

            public static FloorPlan GenerateFloorPlan(FloorLayout layout, IEnumerable<RoomData> roomData, CreateRng rng)
            {
                var allPassages = roomData.SelectMany(GetPassageVariations).ToImmutableHashSet();

                return Take(layout.Rooms)
                       .Select(room => ChooseRoom(room, layout, allPassages, rng)).ToImmutableHashSet()
                       .Map(BuildFloorPlan);
            }

            private static FloorPlan BuildFloorPlan(ImmutableHashSet<RoomPassagesInChunk> passages) =>
                Take(passages)
                    .Select(CreateRoomPlan)
                    .Map(CreateFloorPlan);

            private static RoomPlan CreateRoomPlan(RoomPassagesInChunk passagesInChunk) =>
                new RoomPlan(passagesInChunk.Passages.Id, CreateTransform(passagesInChunk));

            private static RoomTransform CreateTransform(RoomPassagesInChunk passagesInChunk) =>
                new RoomTransform(passagesInChunk.Chunk,
                                  passagesInChunk.Passages.IsMirrored,
                                  passagesInChunk.Passages.RotationCount);

            private static FloorPlan CreateFloorPlan(IEnumerable<RoomPlan> roomPlans) =>
                new FloorPlan(roomPlans.ToImmutableArray());

        }

    }

}