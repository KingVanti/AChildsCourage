using System.Collections.Generic;

namespace AChildsCourage.Game.Floors
{

    public static partial class FloorTilesBuilding
    {

        private delegate RoomsForFloor LoadRoomsFor(FloorPlan floorPlan);

        private class RoomsForFloor : List<RoomInChunk>
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

        internal class TilePositionTransformer
        {
           
            internal int RotationCount { get; }

            internal bool IsMirrored { get; }

            internal TilePosition ChunkCorner { get; }

            internal TilePosition ChunkCenter { get; }

            internal TilePositionTransformer(int rotationCount, bool isMirrored,TilePosition chunkCorner, TilePosition chunkCenter)
            {
                RotationCount = rotationCount;
                IsMirrored = isMirrored;
                ChunkCorner = chunkCorner;
                ChunkCenter = chunkCenter;
            }

        }

        internal class FloorTilesBuilder
        {

            internal HashSet<TilePosition> GroundPositions { get; }

            internal HashSet<TilePosition> WallPositions { get; }


            internal FloorTilesBuilder()
            {
                GroundPositions = new HashSet<TilePosition>();
                WallPositions = new HashSet<TilePosition>();
            }

            internal FloorTilesBuilder(IEnumerable<TilePosition> groundPositions, IEnumerable<TilePosition> wallPositions)
            {
                GroundPositions = new HashSet<TilePosition>(groundPositions);
                WallPositions = new HashSet<TilePosition>(wallPositions);
            }

        }

    }

}