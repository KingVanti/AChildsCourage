namespace AChildsCourage.Game.Floors.RoomPersistence
{

    internal class RoomData
    {

        internal RoomId Id { get; }

        internal RoomType Type { get; }

        internal ChunkPassages Passages { get; }

        internal SerializedRoomContent Content { get; }


        internal RoomData(RoomId id, RoomType type, ChunkPassages passages, SerializedRoomContent content)
        {
            Id = id;
            Type = type;
            Passages = passages;
            Content = content;
        }

    }

}