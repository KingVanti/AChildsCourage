using System;
using System.Collections.Generic;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.RoomPersistence;
using AChildsCourage.Game.Items;
using static AChildsCourage.Game.Floors.MFloor;
using static AChildsCourage.MRng;

namespace AChildsCourage.Game
{

    internal static partial class MFloorGenerating
    {

        internal static Func<FloorPlan, IEnumerable<ItemId>, IEnumerable<RoomData>, CreateRng, Floor> GenerateFloor =>
            (floorPlan, itemIds, roomData, rng) =>
                ChooseRoomsFor(floorPlan, roomData)
                    .Map(BuildRooms)
                    .Map(GenerateWalls)
                    .MapWith(BuildFloor, rng);

        private static Func<IEnumerable<TransformedRoomData>, FloorBuilder> BuildRooms =>
            rooms =>
                rooms.AggregateI(EmptyFloorBuilder, BuildRoom);

    }

}