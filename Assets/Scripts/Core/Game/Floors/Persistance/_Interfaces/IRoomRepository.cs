﻿using AChildsCourage.Game.Floors.Generation;

namespace AChildsCourage.Game.Floors.Persistance
{

    public interface IRoomRepository
    {

        #region Methods

        RoomsInChunks LoadRoomsFor(FloorPlan floorPlan);

        #endregion

    }

}