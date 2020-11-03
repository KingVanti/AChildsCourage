namespace AChildsCourage.Game.Floors.Generation
{

    public interface IRoomInfoRepository
    {

        #region Properties

        RoomInfo StartRoom { get; }

        RoomInfo EndRoom { get; }

        #endregion

        #region Methods

        RoomInfo TryFindRoomFor(ChunkPassages passages);

        #endregion

    }

}