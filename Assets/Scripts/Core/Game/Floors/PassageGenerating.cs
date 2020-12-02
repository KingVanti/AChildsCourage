using System.Collections.Generic;
using AChildsCourage.Game.Floors.RoomPersistance;

namespace AChildsCourage.Game.Floors
{

    public static class PassageGenerating
    {

        public static IEnumerable<RoomPassages> GetPassageVariations(this RoomData roomData) =>
            roomData
                .GetBasePassages()
                .GetVariations();

        public static RoomPassages GetBasePassages(this RoomData roomData) => new RoomPassages(roomData.Id, roomData.Passages, 0, false, roomData.Type);


        public static IEnumerable<RoomPassages> GetVariations(this RoomPassages room)
        {
            for (var _ = 0; _ < 4; _++)
            {
                yield return room;
                yield return room.Mirror();

                room = room.Rotate();
            }
        }

        public static RoomPassages Mirror(this RoomPassages passages) => new RoomPassages(passages.Id, passages.Passages.YMirrored, passages.RotationCount, true, passages.Type);

        public static RoomPassages Rotate(this RoomPassages passages) => new RoomPassages(passages.Id, passages.Passages.Rotated, passages.RotationCount + 1, passages.IsMirrored, passages.Type);

    }

}