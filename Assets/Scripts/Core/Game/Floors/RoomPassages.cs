using AChildsCourage.Game.Floors.RoomPersistence;
using static AChildsCourage.Game.Floors.MChunkPassages;

namespace AChildsCourage.Game.Floors
{

    public class RoomPassages
    {

        #region Properties

        public RoomId Id { get; }

        public ChunkPassages Passages { get; }

        public int RotationCount { get; }

        public bool IsMirrored { get; }

        public RoomType Type { get; }

        #endregion

        #region Constructors
        
        public RoomPassages(RoomId id, ChunkPassages passages, int rotationCount = 0, bool isMirrored = false, RoomType type = RoomType.Normal)
        {
            Id = id;
            Passages = passages;
            Type = type;
            RotationCount = rotationCount;
            IsMirrored = isMirrored;
        }

        #endregion

    }

}