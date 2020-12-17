using System.Collections.Generic;
using System.Linq;
using AChildsCourage.Infrastructure;
using UnityEngine;

namespace AChildsCourage.Game.Floors.RoomPersistence
{

    internal static class RoomDataRepo
    {

        public delegate IEnumerable<RoomData> LoadRoomData();


        private const string RoomResourcePath = "Rooms/";


        [Service]
        public static LoadRoomData FromAssets =>
            () => LoadAssets()
                .Select(ReadData);

        private static IEnumerable<RoomAsset> LoadAssets() => 
            Resources.LoadAll<RoomAsset>(RoomResourcePath);

        private static RoomData ReadData(RoomAsset asset) =>
            new RoomData(asset.Id, asset.Type,asset.Passages, asset.Content);

    }

}