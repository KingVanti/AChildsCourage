﻿using AChildsCourage.Game.Floors.Persistance;
using System;

namespace AChildsCourage.Game.Floors.Generation
{

    public interface IRoomBuilder
    {

        #region Events

        event EventHandler<RoomBuiltEventArgs> OnRoomBuilt;

        #endregion

        #region Methods

        void Build(Room room, ChunkPosition chunkPosition);

        #endregion

    }

}