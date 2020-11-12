using Newtonsoft.Json;
using UnityEngine;

namespace AChildsCourage.Game.Floors.Persistance
{

    [CreateAssetMenu(menuName = "A Child's Courage/Room", fileName = "New Room")]
    public class RoomAsset : ScriptableObject
    {

        #region Fields

        [SerializeField] private int _id;
        [SerializeField] [HideInInspector] private string roomJson;

        #endregion

        #region Properties

        public int Id { get { return _id; } }

        public Room Room { get { return Read(); } set { Write(value); } }

        #endregion

        #region Methods

        private Room Read()
        {
            if(string.IsNullOrEmpty(roomJson))
            {
                var newRoom = Room.Empty;

                Write(newRoom);
                return newRoom;
            }

            return JsonConvert.DeserializeObject<Room>(roomJson);
        }

        private void Write(Room room)
        {
            roomJson = JsonConvert.SerializeObject(room);
        }

        #endregion

    }

}