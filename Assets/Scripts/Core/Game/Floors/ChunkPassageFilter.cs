namespace AChildsCourage.Game.Floors
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

        public bool Matches(ChunkPassages passages) =>
            Matches(north, passages.HasNorth) &&
            Matches(east, passages.HasEast) &&
            Matches(south, passages.HasSouth) &&
            Matches(west, passages.HasWest);

        private static bool Matches(PassageFilter filter, bool hasPassage) =>
            filter == PassageFilter.Open ||
            hasPassage && filter == PassageFilter.Passage ||
            !hasPassage && filter == PassageFilter.NoPassage;


        public int FindLooseEnds(ChunkPassages passages) =>
            (IsLooseEnd(north, passages.HasNorth) ? 1 : 0) +
            (IsLooseEnd(east, passages.HasEast) ? 1 : 0) +
            (IsLooseEnd(south, passages.HasSouth) ? 1 : 0) +
            (IsLooseEnd(west, passages.HasWest) ? 1 : 0);

        private static bool IsLooseEnd(PassageFilter filter, bool hasPassage) => filter == PassageFilter.Open && hasPassage;

        #endregion

    }

}