using System.Collections.Generic;
using System.Collections.Immutable;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.RoomPersistance;
using AChildsCourage.Game.Monsters.Navigation;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game
{

    internal static partial class FloorGenerating
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

            public ImmutableHashSet<CouragePickupData> CouragePickupData { get; }


            public TransformedRoomData(ImmutableHashSet<GroundTileData> groundTiles, ImmutableHashSet<CouragePickupData> couragePickupData)
            {
                GroundData = groundTiles;
                CouragePickupData = couragePickupData;
            }

        }

        internal readonly struct FloorBuilder
        {
            
            public  ImmutableHashSet<Wall> Walls { get; }
            
            public ImmutableHashSet<RoomBuilder> Rooms { get; }
            
            
            public FloorBuilder(ImmutableHashSet<Wall> walls, ImmutableHashSet<RoomBuilder> rooms)
            {
                Walls = walls;
                Rooms = rooms;
            }

        }

        internal readonly struct RoomBuilder
        {

            public  AOIIndex AoiIndex { get; }
            
            public ImmutableHashSet<GroundTile> GroundTiles { get; }

            public ImmutableHashSet<CouragePickup> CouragePickups { get; }


            public RoomBuilder(AOIIndex aoiIndex, ImmutableHashSet<GroundTile> groundTiles, ImmutableHashSet<CouragePickup> couragePickups)
            {
                AoiIndex = aoiIndex;
                GroundTiles = groundTiles;
                CouragePickups = couragePickups;
            }

        }

    }

}