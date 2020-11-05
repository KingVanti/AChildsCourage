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
                Deserialize(asset.GroundPositions),
                Deserialize(asset.WallPositions),
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