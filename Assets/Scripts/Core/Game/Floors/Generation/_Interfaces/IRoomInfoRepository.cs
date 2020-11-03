namespace AChildsCourage.Game.Floors.Generation
{

    public interface IRoomInfoRepository
    {

        #region Methods

        RoomInfo TryFindRoomFor(ChunkPassages passages);

        #endregion

    }

}