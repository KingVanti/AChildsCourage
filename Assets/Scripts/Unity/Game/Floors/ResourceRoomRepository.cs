using AChildsCourage.Game.Floors.RoomPersistance;
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
                var contentData = GetRoomContent(assets, roomPlan.RoomId);
                var roomInChunk = new RoomForFloor(contentData, roomPlan.Transform);

                roomsInChunks.Add(roomInChunk);
            }

            return roomsInChunks;
        }

        private RoomContentData GetRoomContent(IEnumerable<RoomAsset> assets, int id)
        {
            var asset = assets.First(a => a.Id == id);

            return asset.Content;
        }

        #endregion

    }

}