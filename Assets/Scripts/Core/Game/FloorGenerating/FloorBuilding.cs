using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.RoomPersistence;
using AChildsCourage.Game.Shade.Navigation;
using static AChildsCourage.Game.Floors.MFloor;
using static AChildsCourage.F;
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

            internal static Floor BuildFloor(FloorPlan floorPlan, IEnumerable<RoomData> roomData, CreateRng rng) =>
                TransformRooms(floorPlan, roomData)
                    .Map(BuildRooms)
                    .Map(GenerateWalls)
                    .Map(CreateFloor, rng);

            private static FloorBuilder BuildRooms(IEnumerable<TransformedRoomData> rooms) =>
                rooms.AggregateI(EmptyFloorBuilder, BuildRoom);

            private static FloorBuilder BuildRoom(int roomIndex, FloorBuilder floorBuilder, TransformedRoomData transformedRoomData) =>
                Take(EmptyRoomBuilder((AoiIndex) roomIndex, transformedRoomData.RoomType, transformedRoomData.ChunkPosition))
                    .Map(BuildGround, transformedRoomData.GroundData)
                    .Map(BuildStaticObjects, transformedRoomData.StaticObjectData)
                    .Map(BuildCouragePickups, transformedRoomData.CouragePickupData)
                    .Map(BuildRunes, transformedRoomData.RuneData)
                    .Map(room => PlaceRoom(floorBuilder, room));

            private static Floor CreateFloor(FloorBuilder floorBuilder, CreateRng rng) =>
                new Floor(floorBuilder.Walls,
                          ChooseCouragePickups(floorBuilder, rng).ToImmutableHashSet(),
                          floorBuilder.Rooms.Select(BuildRoom).ToImmutableHashSet(),
                          ChooseRunes(floorBuilder, rng).ToImmutableHashSet(),
                          GetEndRoomChunkPosition(floorBuilder));

            private static Room BuildRoom(RoomBuilder roomBuilder) =>
                new Room(roomBuilder.GroundTiles,
                         roomBuilder.StaticObjects);

        }

    }

}