using System.Collections.Generic;
using AChildsCourage.Game.Items;
using AChildsCourage.Game.Persistance;

namespace AChildsCourage.Game
{

    public delegate RunData LoadRunData();

    public delegate IEnumerable<ItemId> LoadItemIds();

    public delegate void LoadNight(NightData data);

}