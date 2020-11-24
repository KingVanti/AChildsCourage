namespace AChildsCourage.Game.Floors
{

    public readonly struct RoomPassageFilter
    {

        #region Properties

        public RoomType RoomType { get; }

        public int MaxLooseEnds { get; }

        public ChunkPassageFilter PassageFilter { get; }

        #endregion

        #region Constructors

        public RoomPassageFilter(RoomType roomType, int maxLooseEnds, ChunkPassageFilter passageFilter)
        {
            RoomType = roomType;
            MaxLooseEnds = maxLooseEnds;
            PassageFilter = passageFilter;
        }

        #endregion

    }

}