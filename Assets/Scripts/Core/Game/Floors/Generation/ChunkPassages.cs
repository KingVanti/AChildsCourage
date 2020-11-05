namespace AChildsCourage.Game.Floors.Generation
{

    public readonly struct ChunkPassages
    {

        #region Static Properties

        public static ChunkPassages None { get { return new ChunkPassages(false, false, false, false); } }

        public static ChunkPassages All { get { return new ChunkPassages(true, true, true, true); } }

        #endregion

        #region Properties

        public Passages Passages { get; }


        public int Count { get { return (HasNorth ? 1 : 0) + (HasEast ? 1 : 0) + (HasSouth ? 1 : 0) + (HasWest ? 1 : 0); } }


        public bool HasNorth { get { return Has(Passages.North); } }

        public bool HasEast { get { return Has(Passages.East); } }

        public bool HasSouth { get { return Has(Passages.South); } }

        public bool HasWest { get { return Has(Passages.West); } }

        #endregion

        #region Constructors

        public ChunkPassages(bool hasNorth, bool hasEast, bool hasSouth, bool hasWest)
        {
            var passages = (Passages)0;

            if (hasNorth) passages |= Passages.North;
            if (hasEast) passages |= Passages.East;
            if (hasSouth) passages |= Passages.South;
            if (hasWest) passages |= Passages.West;

            Passages = passages;
        }

        public ChunkPassages(Passages passages)
        {
            Passages = passages;
        }

        #endregion

        #region Methods

        public bool Has(Passages passage)
        {
            return Passages.HasFlag(passage);
        }

        #endregion

    }

}