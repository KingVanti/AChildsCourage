using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AChildsCourage.Game.Floors.RoomPersistance
{

    internal static class RoomDataLoading
    {

        internal const string RoomResourcePath = "Rooms/";


        internal static LoadRoomData Make()
        {
            return () =>
                LoadAssets()
                    .Select(ReadData);
        }

        private static IEnumerable<RoomAsset> LoadAssets()
        {
            return Resources.LoadAll<RoomAsset>(RoomResourcePath);
        }

        private static RoomData ReadData(RoomAsset asset)
        {
            return new RoomData(
                asset.Id,
                asset.Type,
                asset.Passages,
                asset.Content);
        }

    }

}