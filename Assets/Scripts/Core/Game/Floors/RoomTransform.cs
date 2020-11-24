namespace AChildsCourage.Game.Floors
{

    public readonly struct RoomTransform
    {

        #region Properties

        public ChunkPosition Position { get; }

        public bool IsMirrored { get; }

        public int RotationCount { get; }

        #endregion

        #region Constructors

        public RoomTransform(ChunkPosition position, bool isMirrored, int rotationCount)
        {
            Position = position;
            IsMirrored = isMirrored;
            RotationCount = rotationCount;
        }

        #endregion

    }

}