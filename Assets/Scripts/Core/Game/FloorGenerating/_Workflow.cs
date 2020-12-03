using System.Collections.Generic;
using System.Linq;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.RoomPersistance;
using AChildsCourage.Game.Items;
using static AChildsCourage.Game.Floors.MFloor;

namespace AChildsCourage.Game
{

    internal static partial class FloorGenerating
    {

        internal static Floor GenerateFloor(FloorPlan floorPlan, IEnumerable<ItemId> itemIds, IEnumerable<RoomData> roomData)
        {
            var roomIndex = 0;

            FloorBuilder AddRoomToFloorBuilder(FloorBuilder floor, TransformedRoomData room) => BuildRoom(floor, room, roomIndex++);

            return ChooseRoomsFor(floorPlan, roomData)
                   .Aggregate(EmptyFloorBuilder, AddRoomToFloorBuilder)
                   .Map(GenerateWalls)
                   .Map(BuildFloor);
        }

    }

}