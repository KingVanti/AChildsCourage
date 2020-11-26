using AChildsCourage.Game;
using AChildsCourage.Game.Items;
using System.Collections.Generic;
using UnityEngine;

public class ItemDataRepository
{

    private const string ResourcePath = "Items/";


    private static readonly Dictionary<ItemId, ItemData> cachedItemData = new Dictionary<ItemId, ItemData>();


    private static bool HasCache { get { return cachedItemData.Count > 0; } }


    public static ItemIdLoader GetItemIdLoader()
    {
        return GetItemIds;
    }

    private static IEnumerable<ItemId> GetItemIds()
    {
        if (!HasCache)
            FillCache();

        return cachedItemData.Keys;
    }


    public static ItemDataFinder GetItemDataFinder()
    {
        return GetItemDataBy;
    }

    private static ItemData GetItemDataBy(ItemId id)
    {
        if (!HasCache)
            FillCache();

        return cachedItemData[id];
    }


    private static void FillCache()
    {
        foreach (var asset in Resources.LoadAll<ItemDataAsset>(ResourcePath))
        {
            var data = asset.Data;

            cachedItemData.Add(data.Id, data);
        }
    }

}
