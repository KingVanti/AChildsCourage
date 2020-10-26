﻿using System;

namespace AChildsCourage.Game.Floors.Generation
{

    public class GroundPlacedEventArgs : EventArgs
    {

        #region Properties

        public TilePosition Position { get; }

        #endregion

        #region Constructors

        public GroundPlacedEventArgs(TilePosition position)
        {
            Position = position;
        }

        #endregion

    }
}