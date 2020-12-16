using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AChildsCourage.Game.Floors.RoomPersistence
{

    internal static class RoomDataLoading
    {

        internal const string RoomResourcePath = "Rooms/";


        internal static LoadRoomData Make() =>
            () =>
                LoadAssets()
                    .Select(ReadData);

        private static IEnumerable<RoomAsset> LoadAssets() => Resources.LoadAll<RoomAsset>(RoomResourcePath);

        private static RoomData ReadData(RoomAsset asset) =>
            new RoomData(
                         asset.Id,
                         asset.Type,
                         asset.Passages,
                         asset.Content);

    }

}