using System.Collections.Generic;

namespace AChildsCourage.Game.Floors
{

    public static partial class FloorBuilding
    {

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
                this.TileOffset = tileOffset;
            }

        }

        internal class FloorBuilder
        {

            internal HashSet<TilePosition> GroundPositions { get; } = new HashSet<TilePosition>();

            internal HashSet<TilePosition> WallTilePositions { get; } = new HashSet<TilePosition>();

        }

    }

}