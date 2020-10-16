using System.IO;
using System.Linq;
using UnityEngine;

namespace AChildsCourage.Game.Floors.Persistance
{

    public class ResourceRoomRepository : IRoomRepository
    {

        #region Constants

        protected const string RoomResourcePath = "Rooms/";

        #endregion

        #region Methods

        public RoomData Load(int id)
        {
            var asset = GetRoomAsset(id);

            if (asset != null)
                return CreateRoomFrom(asset);
            else
                throw new FileNotFoundException($"Could not find room with the id {id}!");
        }

        private RoomData CreateRoomFrom(RoomAsset asset)
        {
            return new RoomData(
                asset.RoomShape,
                asset.RoomEntities);
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