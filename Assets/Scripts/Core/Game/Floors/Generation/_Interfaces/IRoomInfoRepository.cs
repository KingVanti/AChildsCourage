using System.Collections.Generic;

namespace AChildsCourage.Game.Floors.Generation
{

    public interface IRoomInfoRepository
    {

        #region Properties

        RoomInfo StartRoom { get; }

        RoomInfo EndRoom { get; }

        #endregion

        #region Methods

        IEnumerable<RoomInfo> FindFittingRoomsFor(ChunkPassageFilter filter);

        #endregion

    }

}