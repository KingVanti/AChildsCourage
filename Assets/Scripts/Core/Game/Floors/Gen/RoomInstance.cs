using AChildsCourage.Game.Floors.RoomPersistence;

namespace AChildsCourage.Game.Floors.Gen
{

    public readonly struct RoomInstance
    {

        public static RoomInstance CreateRoomFromConfiguration(Chunk position, RoomConfiguration config) =>
            new RoomInstance(position, config.RoomId, config.RotationCount, config.IsMirrored);


        public Chunk Position { get; }

        public RoomId Id { get; }

        public int RotationCount { get; }

        public bool IsMirrored { get; }


        private RoomInstance(Chunk position, RoomId id, int rotationCount, bool isMirrored)
        {
            Position = position;
            Id = id;
            RotationCount = rotationCount;
            IsMirrored = isMirrored;
        }

    }

}