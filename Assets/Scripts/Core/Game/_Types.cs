using System.Collections.Generic;
using AChildsCourage.Game.Items;
using AChildsCourage.Game.Persistance;

namespace AChildsCourage.Game
{

    public delegate RunData RunDataLoader();

    public delegate IEnumerable<ItemId> ItemIdLoader();

    public delegate void NightLoader(NightData data);

}