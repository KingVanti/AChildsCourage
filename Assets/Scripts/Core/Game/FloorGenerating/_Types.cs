using System.Collections.Generic;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.RoomPersistance;
using static AChildsCourage.Game.MTilePosition;

namespace AChildsCourage.Game
{

    internal static partial class FloorGenerating
    {

        internal delegate IEnumerable<int> LoadItemIds();

        internal delegate TilePosition TransformTile(TilePosition position);

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

        internal class FloorInProgress
        {

            internal HashSet<TilePosition> GroundPositions { get; } = new HashSet<TilePosition>();

            internal HashSet<TilePosition> WallPositions { get; } = new HashSet<TilePosition>();

            internal HashSet<TilePosition> CourageOrbPositions { get; } = new HashSet<TilePosition>();

            internal HashSet<TilePosition> CourageSparkPositions { get; } = new HashSet<TilePosition>();

            internal HashSet<TilePosition> ItemPickupPositions { get; } = new HashSet<TilePosition>();

        }

    }

}

namespace AChildsCourage.Game
{

    public class RoomForFloor
    {

        public RoomContentData Content { get; }

        public RoomTransform Transform { get; }


        public RoomForFloor(RoomContentData content, RoomTransform transform)
        {
            Content = content;
            Transform = transform;
        }

    }

    public class RoomsForFloor : List<RoomForFloor> { }


}