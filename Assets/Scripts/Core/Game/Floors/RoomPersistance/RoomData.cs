using static AChildsCourage.Game.Floors.MChunkPassages;

namespace AChildsCourage.Game.Floors.RoomPersistence
{

    public class RoomData
    {

        public RoomId Id { get; }

        public RoomType Type { get; }

        public ChunkPassages Passages { get; }

        public RoomContentData Content { get; }

        
        public RoomData(RoomId id, RoomType type, ChunkPassages passages, RoomContentData content)
        {
            Id = id;
            Type = type;
            Passages = passages;
            Content = content;
        }

    }

}