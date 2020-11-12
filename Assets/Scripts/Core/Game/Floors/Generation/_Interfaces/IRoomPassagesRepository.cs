namespace AChildsCourage.Game.Floors.Generation
{

    public interface IRoomPassagesRepository
    {

        #region Methods

        FilteredRoomPassages GetStartRooms();

        FilteredRoomPassages GetNormalRooms(ChunkPassageFilter filter, int maxLooseEnds);

        FilteredRoomPassages GetEndRooms(ChunkPassageFilter filter);

        #endregion

    }

}