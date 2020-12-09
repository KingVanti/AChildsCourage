using System.Collections.Immutable;
using System.Linq;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.RoomPersistence;
using AChildsCourage.Game.Shade.Navigation;
using static AChildsCourage.Game.MChunkPosition;
using static AChildsCourage.MFunctional;

namespace AChildsCourage.Game
{

    public static partial class MFloorGenerating
    {

        public static class MRoomBuilder
        {

            public static RoomBuilder EmptyRoomBuilder(AoiIndex aoiIndex, RoomType roomType, ChunkPosition chunkPosition) =>
                new RoomBuilder(
                    aoiIndex,
                    ImmutableHashSet<GroundTile>.Empty,
                    ImmutableHashSet<CouragePickup>.Empty,
                    ImmutableHashSet<StaticObject>.Empty,
                    roomType,
                    chunkPosition);

            public static RoomBuilder BuildGround(RoomBuilder roomBuilder, ImmutableHashSet<GroundTileData> groundData) =>
                Take(groundData)
                    .Select(data => new GroundTile(roomBuilder.AoiIndex, data.Position))
                    .Aggregate(roomBuilder, PlaceGroundTile);

            private static RoomBuilder PlaceGroundTile(RoomBuilder room, GroundTile tile) =>
                new RoomBuilder(room.AoiIndex,
                                room.GroundTiles.Add(tile),
                                room.CouragePickups,
                                room.StaticObjects,
                                room.RoomType,
                                room.ChunkPosition);

            public static RoomBuilder BuildStaticObjects(RoomBuilder roomBuilder, ImmutableHashSet<StaticObjectData> staticObjects) =>
                Take(staticObjects)
                    .Select(data => new StaticObject(data.Position))
                    .Aggregate(roomBuilder, PlaceStaticObject);

            private static RoomBuilder PlaceStaticObject(RoomBuilder room, StaticObject staticObject) =>
                new RoomBuilder(room.AoiIndex,
                                room.GroundTiles,
                                room.CouragePickups,
                                room.StaticObjects.Add(staticObject),
                                room.RoomType,
                                room.ChunkPosition);

            public static RoomBuilder BuildCouragePickups(RoomBuilder roomBuilder, ImmutableHashSet<CouragePickupData> pickupData) =>
                Take(pickupData)
                    .Select(data => new CouragePickup(data.Position, data.Variant))
                    .Aggregate(roomBuilder, PlaceCouragePickup);

            private static RoomBuilder PlaceCouragePickup(RoomBuilder room, CouragePickup pickup) =>
                new RoomBuilder(room.AoiIndex,
                                room.GroundTiles,
                                room.CouragePickups.Add(pickup),
                                room.StaticObjects,
                                room.RoomType,
                                room.ChunkPosition);

            public readonly struct RoomBuilder
            {

                public AoiIndex AoiIndex { get; }

                public ImmutableHashSet<GroundTile> GroundTiles { get; }

                public ImmutableHashSet<CouragePickup> CouragePickups { get; }

                public ImmutableHashSet<StaticObject> StaticObjects { get; }

                public RoomType RoomType { get; }

                public ChunkPosition ChunkPosition { get; }


                public RoomBuilder(AoiIndex aoiIndex, ImmutableHashSet<GroundTile> groundTiles, ImmutableHashSet<CouragePickup> couragePickups, ImmutableHashSet<StaticObject> staticObjects, RoomType roomType, ChunkPosition chunkPosition)
                {
                    AoiIndex = aoiIndex;
                    GroundTiles = groundTiles;
                    CouragePickups = couragePickups;
                    StaticObjects = staticObjects;
                    RoomType = roomType;
                    ChunkPosition = chunkPosition;
                }

            }

        }

    }

}