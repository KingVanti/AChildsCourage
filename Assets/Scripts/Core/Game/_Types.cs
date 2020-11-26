using AChildsCourage.Game.Items;
using AChildsCourage.Game.Persistance;
using System.Collections.Generic;

namespace AChildsCourage.Game
{

    public delegate RunData RunDataLoader();

    public delegate IEnumerable<ItemId> ItemIdLoader();

    public delegate void NightLoader(NightData data);

}