using AChildsCourage.Game.Floors;
using System.Collections.Generic;

namespace AChildsCourage.Game.NightManagement.Loading
{

    internal delegate RoomsForFloor RoomLoader(FloorPlan floorPlan);

    internal delegate FloorBuilder RoomBuilder(FloorBuilder builder, RoomInChunk room);

    internal delegate TilePosition TileTransformer(TilePosition position);

    internal delegate FloorBuilder TileBuilder(FloorBuilder builder, RoomTiles tiles, TileTransformer transformer);

    internal delegate FloorBuilder GroundBuilder(FloorBuilder builder, Tiles<GroundTile> tiles, TileTransformer transformer);

    internal delegate FloorBuilder DataBuilder(FloorBuilder builder, Tiles<DataTile> tiles, TileTransformer transformer);

    internal delegate IEnumerable<Wall> WallGenerator(FloorBuilder builder);

    internal delegate IEnumerable<TilePosition> CouragePositionChooser(FloorBuilder builder);

    internal delegate Floor FloorCreator(FloorBuilder builder);

    public class RoomsForFloor : List<RoomInChunk>
    {

        public RoomsForFloor()
           : base() { }

        internal RoomsForFloor(IEnumerable<RoomInChunk> roomsInChunks)
            : base()
        {
            foreach (var roomInChunk in roomsInChunks)
                Add(roomInChunk);
        }

    }

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