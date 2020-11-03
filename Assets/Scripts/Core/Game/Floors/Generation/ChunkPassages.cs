namespace AChildsCourage.Game.Floors.Generation
{

    public readonly struct ChunkPassages
    {
        
        #region Properties

        public ChunkPosition Position { get; }

        public Passage[] Passages { get; }

        #endregion

        #region Constructors

        public ChunkPassages(ChunkPosition position, Passage[] passages)
        {
            Position = position;
            Passages = passages;
        }

        #endregion

    }

}