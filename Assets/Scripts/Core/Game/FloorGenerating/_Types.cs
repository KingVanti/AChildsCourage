using System.Collections.Generic;
using System.Collections.Immutable;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.RoomPersistence;
using AChildsCourage.Game.Monsters.Navigation;
using static AChildsCourage.Game.MChunkPosition;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game
{

    internal static partial class MFloorGenerating
    {

        internal delegate IEnumerable<int> LoadItemIds();

        internal delegate TilePosition TransformTile(TilePosition position);

        internal delegate float CouragePickupWeightFunction(TilePosition position, ImmutableHashSet<TilePosition> taken);

        internal class ChunkTransform
        {

            internal int RotationCount { get; }

            internal bool IsMirrored { get; }

            internal TilePosition ChunkCorner { get; }

            internal TilePosition ChunkCenter { get; }


            internal ChunkTransform(int rotationCount, bool isMirrored, TilePosition chunkCorner, TilePosition chunkCenter)
            {
                RotationCount = rotationCount;
                IsMirrored = isMirrored;
                ChunkCorner = chunkCorner;
                ChunkCenter = chunkCenter;
            }

        }

        private class TransformedRoomData
        {

            public ImmutableHashSet<GroundTileData> GroundData { get; }

            public ImmutableHashSet<StaticObjectData> StaticObjectData { get; }

            public ImmutableHashSet<CouragePickupData> CouragePickupData { get; }

            public ChunkPosition ChunkPosition { get; }


            public TransformedRoomData(ImmutableHashSet<GroundTileData> groundTiles, ImmutableHashSet<StaticObjectData> staticObjectData, ImmutableHashSet<CouragePickupData> couragePickupData, ChunkPosition chunkPosition)
            {
                GroundData = groundTiles;
                StaticObjectData = staticObjectData;
                CouragePickupData = couragePickupData;
                ChunkPosition = chunkPosition;
            }

        }

        internal readonly struct FloorBuilder
        {

            public ImmutableHashSet<Wall> Walls { get; }

            public ImmutableHashSet<RoomBuilder> Rooms { get; }

            public FloorBuilder(ImmutableHashSet<Wall> walls, ImmutableHashSet<RoomBuilder> rooms)
            {
                Walls = walls;
                Rooms = rooms;
            }

        }

        internal readonly struct RoomBuilder
        {

            public AoiIndex AoiIndex { get; }

            public ImmutableHashSet<GroundTile> GroundTiles { get; }

            public ImmutableHashSet<CouragePickup> CouragePickups { get; }

            public ImmutableHashSet<StaticObject> StaticObjects { get; }

            public ChunkPosition ChunkPosition { get; }


            public RoomBuilder(AoiIndex aoiIndex, ImmutableHashSet<GroundTile> groundTiles, ImmutableHashSet<CouragePickup> couragePickups, ImmutableHashSet<StaticObject> staticObjects, ChunkPosition chunkPosition)
            {
                AoiIndex = aoiIndex;
                GroundTiles = groundTiles;
                CouragePickups = couragePickups;
                StaticObjects = staticObjects;
                ChunkPosition = chunkPosition;
            }

        }

    }

}