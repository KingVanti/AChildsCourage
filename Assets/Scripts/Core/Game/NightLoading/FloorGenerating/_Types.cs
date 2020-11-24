using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.RoomPersistance;
using System.Collections.Generic;

namespace AChildsCourage.Game.NightLoading
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


    internal static partial class FloorGenerating
    {

        internal delegate RoomsForFloor RoomLoader(FloorPlan floorPlan);

        internal delegate IEnumerable<int> ItemIdLoader();

        internal delegate TilePosition TileTransformer(TilePosition position);

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