﻿using System;

namespace AChildsCourage.Game.Floors.Generation
{

    public readonly struct ChunkPassageFilter
    {

        #region Fields

        private readonly PassageFilter north;
        private readonly PassageFilter east;
        private readonly PassageFilter south;
        private readonly PassageFilter west;

        #endregion

        #region Constructors

        internal ChunkPassageFilter(PassageFilter north, PassageFilter east, PassageFilter south, PassageFilter west)
        {
            this.north = north;
            this.east = east;
            this.south = south;
            this.west = west;
        }

        #endregion

        #region Methods

        internal bool Matches(ChunkPassages passages)
        {
            return
                 Matches(north, passages.HasNorth) &&
                 Matches(east, passages.HasEast) &&
                 Matches(south, passages.HasSouth) &&
                 Matches(west, passages.HasWest);
        }

        private bool Matches(PassageFilter filter, bool hasPassage)
        {
            return
                filter == PassageFilter.Either ||
                (hasPassage && filter == PassageFilter.MustHave) ||
                (!hasPassage && filter == PassageFilter.MustNotHave);
        }


        internal int FindLooseEnds(ChunkPassages passages)
        {
            return
                 (IsLooseEnd(north, passages.HasNorth) ? 1 : 0) +
                 (IsLooseEnd(east, passages.HasEast) ? 1 : 0) +
                 (IsLooseEnd(south, passages.HasSouth) ? 1 : 0) +
                 (IsLooseEnd(west, passages.HasWest) ? 1 : 0);
        }

        private bool IsLooseEnd(PassageFilter filter, bool hasPassage)
        {
            return filter == PassageFilter.Either && hasPassage;
        }

        #endregion

    }

}