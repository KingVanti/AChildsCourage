using AChildsCourage.Game.NightManagement.Loading;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AChildsCourage.Game.Floors
{

    public class ResourceRoomRepository : IRoomRepository
    {

        #region Constants

        protected const string RoomResourcePath = "Rooms/";

        #endregion

        #region Methods

        public RoomsForFloor LoadRoomsFor(FloorPlan floorPlan)
        {
            var assets = LoadAssets();

            return GetFloorRooms(assets, floorPlan.Rooms);
        }

        private IEnumerable<RoomAsset> LoadAssets()
        {
            return Resources.LoadAll<RoomAsset>(RoomResourcePath);
        }

        private RoomsForFloor GetFloorRooms(IEnumerable<RoomAsset> assets, IEnumerable<RoomPlan> roomPlans)
        {
            var roomsInChunks = new RoomsForFloor();

            foreach (var roomPlan in roomPlans)
            {
                var room = GetRoom(assets, roomPlan.RoomId);
                var roomInChunk = new RoomInChunk(room, roomPlan.Transform);

                roomsInChunks.Add(roomInChunk);
            }

            return roomsInChunks;
        }

        private Room GetRoom(IEnumerable<RoomAsset> assets, int id)
        {
            var asset = assets.First(a => a.Id == id);

            return asset.Room;
        }

        #endregion

    }

}