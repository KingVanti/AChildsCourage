using AChildsCourage.Game.Floors.Generation;
using UnityEngine;

namespace AChildsCourage.Game.Floors.Persistance
{

    [CreateAssetMenu(menuName = "A Child's Courage/Room", fileName = "New Room")]
    public class RoomAsset : ScriptableObject
    {

        #region Fields

        [SerializeField] private int _id;
        [SerializeField] private RoomType type;
        [SerializeField] private SerializableRoomTiles _roomTiles;
        [SerializeField] private SerializablePassages _passages;

        #endregion

        #region Properties

        public int Id { get { return _id; } }

        public RoomType Type { get { return type; } }

        public RoomTiles RoomTiles { get { return _roomTiles.Deserialize(); } set { _roomTiles = new SerializableRoomTiles(value); } }

        public ChunkPassages Passages { get { return _passages.Deserialize(); } set { _passages = new SerializablePassages(value); } }

        #endregion

    }

}