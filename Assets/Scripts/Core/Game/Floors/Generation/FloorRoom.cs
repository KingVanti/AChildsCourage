using AChildsCourage.Game.Floors.Persistance;

namespace AChildsCourage.Game.Floors.Generation
{
    public class FloorRoom
    {

        #region Properties

        public ChunkPosition Position { get; }

        public RoomData RoomData { get; }

        #endregion

        #region Constructors

        public FloorRoom(ChunkPosition position, RoomData roomData)
        {
            Position = position;
            RoomData = roomData;
        }

        #endregion

    }
}