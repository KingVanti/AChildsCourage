using System.IO;
using System.Linq;
using UnityEngine;

namespace AChildsCourage.Game.FloorGeneration.Persistance
{

    public class ResourceRoomRepository : IRoomRepository
    {

        #region Constants

        protected const string RoomResourcePath = "Rooms/";

        #endregion

        #region Methods

        public Room Load(int id)
        {
            var asset = GetRoomAsset(id);

            if (asset != null)
                return new Room();
            else
                throw new FileNotFoundException($"Could not find room with the id {id}!");
        }


        public bool Contains(int id)
        {
            return GetRoomAsset(id) != null;
        }


        protected RoomAsset GetRoomAsset(int id)
        {
            return Resources.LoadAll<RoomAsset>(RoomResourcePath).FirstOrDefault(r => r.Id == id);
        }

        #endregion

    }

}