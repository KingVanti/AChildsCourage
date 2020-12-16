using System.Collections.Generic;
using System.Linq;
using AChildsCourage.Game.Floors.RoomPersistence;
using static AChildsCourage.Game.MFloorGenerating;
using static AChildsCourage.Game.MFloorGenerating.MRoomPassageGenerating;

namespace AChildsCourage.Game.Floors.TestGenerator
{

    internal class CompleteRoomLoader
    {

        private readonly RoomData[] allData;

        private readonly ChunkPassages[] basePassages = {new ChunkPassages(true, false, false, false), new ChunkPassages(true, true, false, false), new ChunkPassages(true, false, true, false), new ChunkPassages(true, true, true, false), new ChunkPassages(true, true, true, true)};


        internal CompleteRoomLoader()
        {
            var startRoom = new RoomData((RoomId) 0, RoomType.Start, ChunkPassages.All, RoomContentData.Empty);

            var id = 1;
            var query =
                from basePassage in basePassages
                from type in new[] {RoomType.Normal, RoomType.End}
                select new RoomData((RoomId) id++, type, basePassage, RoomContentData.Empty);

            allData = query.Prepend(startRoom)
                           .ToArray();
        }


        internal ChunkPassages GetPassagesFor(RoomPlan roomPlan) =>
            allData.First(d => d.Id == roomPlan.RoomId)
                   .Map(GetBasePassages)
                   .For(roomPlan.Transform.RotationCount, Rotate)
                   .DoIf(Mirror, roomPlan.Transform.IsMirrored)
                   .Map(p => p.Passages);


        internal IEnumerable<RoomData> All() => allData;

        internal RoomType GetRoomType(RoomPlan roomPlan) => allData.First(d => d.Id == roomPlan.RoomId).Type;

    }

}