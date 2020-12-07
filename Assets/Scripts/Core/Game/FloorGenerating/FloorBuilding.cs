using System.Collections.Immutable;
using System.Linq;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.RoomPersistence;
using AChildsCourage.Game.Monsters.Navigation;
using static AChildsCourage.Game.Floors.MFloor;
using static AChildsCourage.MFunctional;
using static AChildsCourage.Game.Floors.MRoom;
using static AChildsCourage.Game.MChunkPosition;
using static AChildsCourage.MRng;

namespace AChildsCourage.Game
{

    internal static partial class MFloorGenerating
    {

        private static FloorBuilder EmptyFloorBuilder =>
            new FloorBuilder(
                ImmutableHashSet<Wall>.Empty,
                ImmutableHashSet<RoomBuilder>.Empty);

        private static RoomBuilder EmptyRoomBuilder(AoiIndex aoiIndex, ChunkPosition chunkPosition) =>
            new RoomBuilder(
                aoiIndex,
                ImmutableHashSet<GroundTile>.Empty,
                ImmutableHashSet<CouragePickup>.Empty,
                ImmutableHashSet<StaticObject>.Empty,
                chunkPosition);


        private static FloorBuilder BuildRoom(int roomIndex, FloorBuilder floorBuilder, TransformedRoomData transformedRoomData) =>
            Take(EmptyRoomBuilder((AoiIndex) roomIndex, transformedRoomData.ChunkPosition))
                .MapWith(BuildGround, transformedRoomData.GroundData)
                .MapWith(BuildStaticObjects, transformedRoomData.StaticObjectData)
                .MapWith(BuildCouragePickups, transformedRoomData.CouragePickupData)
                .Map(room => PlaceRoom(floorBuilder, room));

        internal static RoomBuilder BuildGround(RoomBuilder roomBuilder, ImmutableHashSet<GroundTileData> groundData) =>
            Take(groundData)
                .Select(data => new GroundTile(roomBuilder.AoiIndex, data.Position))
                .Aggregate(roomBuilder, PlaceGroundTile);

        private static RoomBuilder PlaceGroundTile(RoomBuilder room, GroundTile tile) =>
            new RoomBuilder(room.AoiIndex,
                            room.GroundTiles.Add(tile),
                            room.CouragePickups,
                            room.StaticObjects,
                            room.ChunkPosition);

        internal static RoomBuilder BuildStaticObjects(RoomBuilder roomBuilder, ImmutableHashSet<StaticObjectData> staticObjects) =>
            Take(staticObjects)
                .Select(data => new StaticObject(data.Position))
                .Aggregate(roomBuilder, PlaceStaticObject);

        private static RoomBuilder PlaceStaticObject(RoomBuilder room, StaticObject staticObject) =>
            new RoomBuilder(room.AoiIndex,
                            room.GroundTiles,
                            room.CouragePickups,
                            room.StaticObjects.Add(staticObject),
                            room.ChunkPosition);

        internal static RoomBuilder BuildCouragePickups(RoomBuilder roomBuilder, ImmutableHashSet<CouragePickupData> pickupData) =>
            Take(pickupData)
                .Select(data => new CouragePickup(data.Position, data.Variant))
                .Aggregate(roomBuilder, PlaceCouragePickup);

        private static RoomBuilder PlaceCouragePickup(RoomBuilder room, CouragePickup pickup) =>
            new RoomBuilder(room.AoiIndex,
                            room.GroundTiles,
                            room.CouragePickups.Add(pickup),
                            room.StaticObjects,
                            room.ChunkPosition);

        private static FloorBuilder PlaceRoom(FloorBuilder floor, RoomBuilder room) =>
            new FloorBuilder(floor.Walls,
                             floor.Rooms.Add(room));

        private static Floor BuildFloor(FloorBuilder floorBuilder, CreateRng rng) =>
            new Floor(floorBuilder.Walls,
                      ChooseCouragePickups(floorBuilder, rng).ToImmutableHashSet(),
                      floorBuilder.Rooms.Select(BuildRoom).ToImmutableHashSet(),
                      GetEndRoomChunkPosition(floorBuilder));

        private static ChunkPosition GetEndRoomChunkPosition(FloorBuilder floorBuilder) => floorBuilder.Rooms.Last().ChunkPosition;

        private static Room BuildRoom(RoomBuilder roomBuilder) =>
            new Room(roomBuilder.AoiIndex,
                     roomBuilder.GroundTiles,
                     roomBuilder.StaticObjects);

    }

}