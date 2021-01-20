using AChildsCourage.Game.Floors.RoomPersistence;

namespace AChildsCourage.Game.Floors.Gen
{

    internal readonly struct RoomInstance
    {

        internal static RoomInstance CreateRoomFromConfiguration(Chunk position, RoomConfiguration config) =>
            new RoomInstance(position, config.RoomId, config.RotationCount, config.IsMirrored);


        internal Chunk Position { get; }

        internal RoomId Id { get; }

        internal int RotationCount { get; }

        internal bool IsMirrored { get; }


        private RoomInstance(Chunk position, RoomId id, int rotationCount, bool isMirrored)
        {
            Position = position;
            Id = id;
            RotationCount = rotationCount;
            IsMirrored = isMirrored;
        }

    }

}