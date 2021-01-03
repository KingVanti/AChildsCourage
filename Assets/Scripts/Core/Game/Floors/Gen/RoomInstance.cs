using AChildsCourage.Game.Floors.RoomPersistence;
using static AChildsCourage.Game.MChunkPosition;

namespace AChildsCourage.Game.Floors.Gen
{

    public readonly struct RoomInstance
    {

        public static RoomInstance CreateRoomFromConfiguration(ChunkPosition position, RoomConfiguration config) =>
            new RoomInstance(position, config.RoomId, config.RotationCount, config.IsMirrored);


        public ChunkPosition Position { get; }

        public RoomId Id { get; }

        public int RotationCount { get; }

        public bool IsMirrored { get; }


        private RoomInstance(ChunkPosition position, RoomId id, int rotationCount, bool isMirrored)
        {
            Position = position;
            Id = id;
            RotationCount = rotationCount;
            IsMirrored = isMirrored;
        }

    }

}