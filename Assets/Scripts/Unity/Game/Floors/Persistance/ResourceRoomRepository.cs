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

        public FloorRooms LoadRoomsFor(FloorPlan floorPlan)
        {
            var assets = LoadAssets();

            return GetFloorRooms(assets, floorPlan.Rooms);
        }

        private IEnumerable<RoomAsset> LoadAssets()
        {
            return Resources.LoadAll<RoomAsset>(RoomResourcePath);
        }

        private FloorRooms GetFloorRooms(IEnumerable<RoomAsset> assets, IEnumerable<RoomInChunk> roomsInChunks)
        {
            var floorRooms = new FloorRooms();

            foreach (var roomInChunk in roomsInChunks)
            {
                var roomData = GetRoomWithId(assets, roomInChunk.RoomId);
                var room = new FloorRoom(roomInChunk.Position, roomData);

                floorRooms.Add(room);
            }

            return floorRooms;
        }

        private RoomData GetRoomWithId(IEnumerable<RoomAsset> assets, int id)
        {
            var asset = assets.First(a => a.Id == id);

            return CreateRoomFrom(asset);
        }

        private RoomData CreateRoomFrom(RoomAsset asset)
        {
            return new RoomData(
                Deserialize(asset.GroundPositions),
                Deserialize(asset.ItemPositions),
                Deserialize(asset.SmallCouragePositions),
                Deserialize(asset.BigCouragePositions));
        }

        private TilePosition[] Deserialize(Vector2Int[] positions)
        {
            return positions
                .Select(p => new TilePosition(p.x, p.y))
                .ToArray();
        }

        #endregion

    }

}