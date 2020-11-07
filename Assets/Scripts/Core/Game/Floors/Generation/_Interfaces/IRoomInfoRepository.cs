using System.Collections.Generic;

namespace AChildsCourage.Game.Floors.Generation
{

    public interface IRoomInfoRepository
    {

        #region Properties

        RoomInfo StartRoom { get; }

        #endregion

        #region Methods

        IEnumerable<RoomInfo> FindFittingRoomsFor(ChunkPassageFilter filter, int remainingRoomCount);

        RoomInfo GetEndRoomFor(ChunkPassageFilter filter);

        #endregion

    }

}