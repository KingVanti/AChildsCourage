using System.Collections.Generic;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.RoomPersistence;
using static AChildsCourage.F;

namespace AChildsCourage.Game
{

    public static partial class MFloorGenerating
    {

        public static class MRoomPassageGenerating
        {

            public static IEnumerable<RoomPassages> GetPassageVariations(RoomData roomData) =>
                Take(roomData)
                    .Map(GetBasePassages)
                    .Map(GetVariations);

            public static RoomPassages GetBasePassages(RoomData roomData) => new RoomPassages(roomData.Id, roomData.Passages, 0, false, roomData.Type);

            private static IEnumerable<RoomPassages> GetVariations(RoomPassages room)
            {
                for (var _ = 0; _ < 4; _++)
                {
                    yield return room;
                    yield return Mirror(room);

                    room = Rotate(room);
                }
            }

            public static RoomPassages Mirror(RoomPassages passages) => new RoomPassages(passages.Id, passages.Passages.YMirrored, passages.RotationCount, true, passages.Type);

            public static RoomPassages Rotate(RoomPassages passages) => new RoomPassages(passages.Id, passages.Passages.Rotated, passages.RotationCount + 1, passages.IsMirrored, passages.Type);

        }

    }

}