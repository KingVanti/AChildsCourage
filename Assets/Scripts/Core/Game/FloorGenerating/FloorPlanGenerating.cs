using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using AChildsCourage.Game.Floors.RoomPersistence;
using static AChildsCourage.Game.MFloorGenerating.MFloorLayout;
using static AChildsCourage.MFunctional;
using static AChildsCourage.Game.MFloorGenerating.MRoomChoosing;
using static AChildsCourage.Game.MFloorGenerating.MRoomPassageGenerating;
using static AChildsCourage.MRng;

namespace AChildsCourage.Game
{

    public static partial class MFloorGenerating
    {

        public static class MFloorPlanGenerating
        {

            public static Func<FloorLayout, IEnumerable<RoomData>, CreateRng, FloorPlan> GenerateFloorPlan =>
                (layout, roomData, rng) =>
                {
                    var allPassages = roomData.SelectMany(GetPassageVariations).ToImmutableHashSet();

                    return Take(layout.Rooms)
                           .Select(room => ChooseRoom(room, layout, allPassages, rng)).ToImmutableHashSet()
                           .Map(BuildFloorPlan);
                };

            private static Func<ImmutableHashSet<RoomPassagesInChunk>, FloorPlan> BuildFloorPlan =>
                passages =>
                    Take(passages)
                        .Select(CreateRoomPlan)
                        .Map(CreateFloorPlan);

            private static Func<RoomPassagesInChunk, RoomPlan> CreateRoomPlan =>
                passagesInChunk =>
                    new RoomPlan(passagesInChunk.Passages.Id, CreateTransform(passagesInChunk));

            private static Func<RoomPassagesInChunk, RoomTransform> CreateTransform =>
                passagesInChunk =>
                    new RoomTransform(
                        passagesInChunk.Chunk,
                        passagesInChunk.Passages.IsMirrored,
                        passagesInChunk.Passages.RotationCount);

            private static Func<IEnumerable<RoomPlan>, FloorPlan> CreateFloorPlan =>
                roomPlans =>
                    new FloorPlan(roomPlans.ToImmutableArray());

        }

    }

}