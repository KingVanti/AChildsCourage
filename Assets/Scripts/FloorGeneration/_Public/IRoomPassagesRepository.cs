namespace AChildsCourage.Game.Floors
{

    public interface IRoomPassagesRepository
    {

        #region Methods

        FilteredRoomPassages GetRooms(RoomPassageFilter filter);

        #endregion

    }

}