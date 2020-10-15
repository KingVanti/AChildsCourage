using UnityEngine;

namespace AChildsCourage.Game.FloorGeneration.Persistance
{

    [CreateAssetMenu(menuName = "A Child's Courage/Room", fileName = "New Room")]
    public class RoomAsset : ScriptableObject
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private int _id;
        [SerializeField] private SerializableRoomShape _roomShape;
        [SerializeField] private SerializableRoomEntities _roomEntities;

#pragma warning restore 649

        #endregion

        #region Properties

        public int Id { get { return _id; } set { _id = value; } }

        public RoomShape RoomShape
        {
            get { return _roomShape?.ToRoomShape(); }
            set { _roomShape = value != null ? SerializableRoomShape.From(value) : null; }
        }

        public RoomEntities RoomEntities
        {
            get { return _roomEntities?.ToRoomEntities(); }
            set { _roomEntities = value != null ? SerializableRoomEntities.From(value) : null; }
        }

        #endregion

    }

}