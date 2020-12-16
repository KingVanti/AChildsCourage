using UnityEngine;
using static AChildsCourage.Game.Floors.MChunkPassages;
using static Newtonsoft.Json.JsonConvert;

namespace AChildsCourage.Game.Floors.RoomPersistence
{

    [CreateAssetMenu(menuName = "A Child's Courage/Room", fileName = "New Room")]
    public class RoomAsset : ScriptableObject
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private int _id;
        [SerializeField] private RoomType _type;
        [SerializeField] [TextArea(10, 15)] private string passageJson;
        [SerializeField] [TextArea(10, 40)] private string contentJson;
        [SerializeField] [HideInInspector] private string roomJson;

#pragma warning restore 649

        #endregion

        #region Properties

        public RoomId Id => (RoomId) _id;

        public RoomType Type => _type;

        public ChunkPassages Passages
        {
            get => DeserializeObject<ChunkPassages>(passageJson);
            set => passageJson = SerializeObject(value);
        }

        public RoomContentData Content
        {
            get => DeserializeObject<RoomContentData>(contentJson);
            set => contentJson = SerializeObject(value);
        }

        #endregion

    }

}