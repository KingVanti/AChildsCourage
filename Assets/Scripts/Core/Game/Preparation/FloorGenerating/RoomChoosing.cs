using System.Collections.Generic;
using System.Linq;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.RoomPersistance;
using AChildsCourage.Game.NightLoading;

namespace AChildsCourage.Game
{

    internal static partial class FloorGenerating
    {

        private static RoomsForFloor ChooseRoomsFor(FloorPlan floorPlan, IEnumerable<RoomData> rooms)
        {
            var roomsInChunks = new RoomsForFloor();

            foreach (var roomPlan in floorPlan.Rooms)
            {
                var room = rooms.First(r => r.Id == roomPlan.RoomId);

                roomsInChunks.Add(new RoomForFloor(room.Content, roomPlan.Transform));
            }

            return roomsInChunks;
        }

    }

}