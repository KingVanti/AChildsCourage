using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using AChildsCourage.Game.Floors.RoomPersistence;
using AChildsCourage.Game.Shade.Navigation;
using static AChildsCourage.Game.Floors.MFloor;
using static AChildsCourage.MFunctional;
using static AChildsCourage.Game.Floors.MRoom;
using static AChildsCourage.Game.MFloorGenerating.MFloorBuilder;
using static AChildsCourage.Game.MFloorGenerating.MWallGenerating;
using static AChildsCourage.Game.MFloorGenerating.MCouragePickupFiltering;
using static AChildsCourage.Game.MFloorGenerating.MRoomBuilder;
using static AChildsCourage.Game.MFloorGenerating.MRoomTransforming;
using static AChildsCourage.Game.MFloorGenerating.MRuneFiltering;
using static AChildsCourage.MRng;

namespace AChildsCourage.Game
{

    public static partial class MFloorGenerating
    {

        public static class MFloorBuilding
        {

            internal static Func<FloorPlan, IEnumerable<RoomData>, CreateRng, Floor> BuildFloor =>
                (floorPlan, roomData, rng) =>
                    TransformRooms(floorPlan, roomData)
                        .Map(BuildRooms)
                        .Map(GenerateWalls)
                        .MapWith(CreateFloor, rng);

            private static Func<IEnumerable<TransformedRoomData>, FloorBuilder> BuildRooms =>
                rooms =>
                    rooms.AggregateI(EmptyFloorBuilder, BuildRoom);

            private static FloorBuilder BuildRoom(int roomIndex, FloorBuilder floorBuilder, TransformedRoomData transformedRoomData) =>
                Take(EmptyRoomBuilder((AoiIndex) roomIndex, transformedRoomData.RoomType, transformedRoomData.ChunkPosition))
                    .MapWith(BuildGround, transformedRoomData.GroundData)
                    .MapWith(BuildStaticObjects, transformedRoomData.StaticObjectData)
                    .MapWith(BuildCouragePickups, transformedRoomData.CouragePickupData)
                    .MapWith(BuildRunes, transformedRoomData.RuneData)
                    .Map(room => PlaceRoom(floorBuilder, room));

            private static Floor CreateFloor(FloorBuilder floorBuilder, CreateRng rng) =>
                new Floor(floorBuilder.Walls,
                          ChooseCouragePickups(floorBuilder, rng).ToImmutableHashSet(),
                          floorBuilder.Rooms.Select(BuildRoom).ToImmutableHashSet(),
                          ChooseRunes(floorBuilder, rng).ToImmutableHashSet(),
                          GetEndRoomChunkPosition(floorBuilder));

            private static Room BuildRoom(RoomBuilder roomBuilder) =>
                new Room(roomBuilder.AoiIndex,
                         roomBuilder.GroundTiles,
                         roomBuilder.StaticObjects);

        }

    }

}