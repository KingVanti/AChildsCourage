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

            internal TileOffset TileOffset { get; }

            internal TilePositionTransformer(TileOffset tileOffset)
            {
                TileOffset = tileOffset;
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