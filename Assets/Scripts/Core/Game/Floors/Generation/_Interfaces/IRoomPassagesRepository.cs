namespace AChildsCourage.Game.Floors.Generation
{

    public interface IRoomPassagesRepository
    {

        #region Properties

        RoomPassages StartRoom { get; }

        #endregion

        #region Methods

        FilteredRoomPassages FindFittingRoomsFor(ChunkPassageFilter filter, int remainingRoomCount);

        RoomPassages GetEndRoomFor(ChunkPassageFilter filter);

        #endregion

    }

}