using System.Collections.Generic;
using AChildsCourage.Game;
using AChildsCourage.Game.Items;
using UnityEngine;

public class ItemDataRepository
{

    private const string ResourcePath = "Items/";


    private static readonly Dictionary<ItemId, ItemData> CachedItemData = new Dictionary<ItemId, ItemData>();


    private static bool HasCache => CachedItemData.Count > 0;


    public static LoadItemIds GetItemIdLoader()
    {
        return GetItemIds;
    }

    private static IEnumerable<ItemId> GetItemIds()
    {
        if (!HasCache)
            FillCache();

        return CachedItemData.Keys;
    }


    public static FindItemData GetItemDataFinder()
    {
        return GetItemDataBy;
    }

    private static ItemData GetItemDataBy(ItemId id)
    {
        if (!HasCache)
            FillCache();

        return CachedItemData[id];
    }


    private static void FillCache()
    {
        foreach (var asset in Resources.LoadAll<ItemDataAsset>(ResourcePath))
        {
            var data = asset.Data;

            CachedItemData.Add(data.Id, data);
        }
    }

}