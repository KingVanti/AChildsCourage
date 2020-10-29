namespace AChildsCourage.Game.Floors.Generation
{

    public readonly struct RoomAtPosition
    {

        #region Properties

        public int RoomId { get; }

        public TilePosition Position { get; }

        #endregion

        #region Constructors

        internal RoomAtPosition(int roomId, TilePosition position)
        {
            RoomId = roomId;
            Position = position;
        }

        #endregion

    }

}