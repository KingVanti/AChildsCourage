using AChildsCourage.Game.Floors.Generation;
using System.Collections.Generic;
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

        public RoomsInChunks LoadRoomsFor(FloorPlan floorPlan)
        {
            var assets = LoadAssets();

            return GetFloorRooms(assets, floorPlan.Rooms);
        }

        private IEnumerable<RoomAsset> LoadAssets()
        {
            return Resources.LoadAll<RoomAsset>(RoomResourcePath);
        }

        private RoomsInChunks GetFloorRooms(IEnumerable<RoomAsset> assets, IEnumerable<RoomIdInChunk> roomsInChunks)
        {
            var floorRooms = new RoomsInChunks();

            foreach (var roomInChunk in roomsInChunks)
            {
                var roomTiles = GetRoom(assets, roomInChunk.RoomId);
                var room = new RoomInChunk(roomInChunk.Position, roomTiles);

                floorRooms.Add(room);
            }

            return floorRooms;
        }

        private Room GetRoom(IEnumerable<RoomAsset> assets, int id)
        {
            var asset = assets.First(a => a.Id == id);

            return asset.Room;
        }

        #endregion

    }

}