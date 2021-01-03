using UnityEngine;
using UnityEngine.Serialization;
using static Newtonsoft.Json.JsonConvert;

namespace AChildsCourage.Game.Floors.RoomPersistence
{

    [CreateAssetMenu(menuName = "A Child's Courage/Room", fileName = "New Room")]
    public class RoomAsset : ScriptableObject
    {

        [FormerlySerializedAs("_id")] [SerializeField]
        private int id;
        [FormerlySerializedAs("_type")] [SerializeField]
        private RoomType type;
        [SerializeField] [TextArea(10, 15)] private string passageJson;
        [SerializeField] [TextArea(10, 40)] private string contentJson;

        public RoomId Id => (RoomId) id;

        public RoomType Type => type;

        public ChunkPassages Passages
        {
            get => DeserializeObject<ChunkPassages>(passageJson);
            set => passageJson = SerializeObject(value);
        }

        public SerializedRoomContent Content
        {
            get => DeserializeObject<SerializedRoomContent>(contentJson);
            set => contentJson = SerializeObject(value);
        }

    }

}