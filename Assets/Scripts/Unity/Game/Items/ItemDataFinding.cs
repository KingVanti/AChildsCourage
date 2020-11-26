using System.Collections.Generic;
using UnityEngine;

namespace AChildsCourage.Game.Items
{

    internal static class ItemDataFinding
    {

        private const string ResourcePath = "Items/";


        private static readonly Dictionary<ItemId, ItemData> cachedItemData = new Dictionary<ItemId, ItemData>();


        internal static ItemDataFinder Make()
        {
            return FindFor;
        }


        private static ItemData FindFor(ItemId id)
        {
            if (cachedItemData.Count == 0)
                FillCache();

            return cachedItemData[id];
        }


        private static void FillCache()
        {
            foreach(var asset in Resources.LoadAll<ItemDataAsset>(ResourcePath))
            {
                var data = asset.Data;

                cachedItemData.Add(data.Id, data);
            }
        }

    }

}