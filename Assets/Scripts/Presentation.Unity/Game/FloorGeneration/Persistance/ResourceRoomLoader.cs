using System.IO;
using System.Linq;
using UnityEngine;

namespace AChildsCourage.Game.FloorGeneration.Persistance
{

    public class ResourceRoomLoader : IRoomLoader
    {

        #region Constants

        private const string RoomResourcePath = "Rooms/";

        #endregion

        #region Fields

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

        private RoomAsset GetRoomAsset(int id)
        {
            return Resources.LoadAll<RoomAsset>(RoomResourcePath).FirstOrDefault(r => r.Id == id);
        }

        #endregion

    }

}