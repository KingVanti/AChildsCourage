using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.RoomPersistance;
using System.Collections.Generic;

namespace AChildsCourage.Game.NightManagement.Loading
{

    internal delegate RoomsForFloor RoomLoader(FloorPlan floorPlan);

    internal delegate FloorBuilder RoomBuilder(FloorBuilder builder, RoomForFloor room);

    internal delegate TilePosition TileTransformer(TilePosition position);

    internal delegate FloorBuilder ContentBuilder(FloorBuilder builder, RoomContentData content, TileTransformer transformer);

    internal delegate FloorBuilder GroundBuilder(FloorBuilder builder, GroundTileData[] tiles, TileTransformer transformer);

    internal delegate FloorBuilder CourageBuilder(FloorBuilder builder, CouragePickupData[] pickups, TileTransformer transformer);

    internal delegate IEnumerable<Wall> WallGenerator(FloorBuilder builder);

    internal delegate IEnumerable<TilePosition> CouragePositionChooser(FloorBuilder builder);

    internal delegate Floor FloorCreator(FloorBuilder builder);


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

    internal class FloorBuilder
    {

        internal HashSet<TilePosition> GroundPositions { get; }

        internal HashSet<TilePosition> WallPositions { get; }

        internal HashSet<TilePosition> CourageOrbPositions { get; }

        internal HashSet<TilePosition> CourageSparkPositions { get; }


        internal FloorBuilder()
        {
            GroundPositions = new HashSet<TilePosition>();
            WallPositions = new HashSet<TilePosition>();
            CourageOrbPositions = new HashSet<TilePosition>();
            CourageSparkPositions = new HashSet<TilePosition>();
        }

    }

}