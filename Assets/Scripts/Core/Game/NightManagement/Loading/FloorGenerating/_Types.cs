using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.RoomPersistance;
using System.Collections.Generic;

namespace AChildsCourage.Game.NightManagement.Loading
{

    internal delegate RoomsForFloor RoomLoader(FloorPlan floorPlan);

    internal delegate FloorInProgress RoomBuilder(FloorInProgress floor, RoomForFloor room);

    internal delegate TilePosition TileTransformer(TilePosition position);

    internal delegate FloorInProgress ContentBuilder(FloorInProgress floor, RoomContentData content, TileTransformer transformer);

    internal delegate FloorInProgress GroundBuilder(FloorInProgress floor, GroundTileData[] tiles, TileTransformer transformer);

    internal delegate FloorInProgress CourageBuilder(FloorInProgress floor, CouragePickupData[] pickups, TileTransformer transformer);

    internal delegate IEnumerable<Wall> WallGenerator(FloorInProgress floor);

    internal delegate IEnumerable<TilePosition> CouragePositionChooser(FloorInProgress floor);

    internal delegate Floor FloorCreator(FloorInProgress floor);


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

        internal HashSet<TilePosition> GroundPositions { get; }

        internal HashSet<TilePosition> WallPositions { get; }

        internal HashSet<TilePosition> CourageOrbPositions { get; }

        internal HashSet<TilePosition> CourageSparkPositions { get; }


        internal FloorInProgress()
        {
            GroundPositions = new HashSet<TilePosition>();
            WallPositions = new HashSet<TilePosition>();
            CourageOrbPositions = new HashSet<TilePosition>();
            CourageSparkPositions = new HashSet<TilePosition>();
        }

    }

}