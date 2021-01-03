namespace AChildsCourage.Game.Floors.RoomPersistence
{

    public class RoomData
    {

        public RoomId Id { get; }

        public RoomType Type { get; }

        public ChunkPassages Passages { get; }

        public SerializedRoomContent Content { get; }


        public RoomData(RoomId id, RoomType type, ChunkPassages passages, SerializedRoomContent content)
        {
            Id = id;
            Type = type;
            Passages = passages;
            Content = content;
        }

    }

}