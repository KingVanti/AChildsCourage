using AChildsCourage.Game.Floors.Persistance;

namespace AChildsCourage.Game.Floors.Generation
{
    public class FloorRoom
    {

        #region Properties

        public ChunkPosition Position { get; }

        public RoomTiles Tiles { get; }

        #endregion

        #region Constructors

        public FloorRoom(ChunkPosition position, RoomTiles tiles)
        {
            Position = position;
            Tiles = tiles;
        }

        #endregion

    }
}