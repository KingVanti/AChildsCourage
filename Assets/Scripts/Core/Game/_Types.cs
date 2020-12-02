using System.Collections.Generic;
using AChildsCourage.Game.Items;
using static AChildsCourage.Game.Persistance.MRunData;

namespace AChildsCourage.Game
{

    public delegate RunData LoadRunData();

    public delegate IEnumerable<ItemId> LoadItemIds();

}