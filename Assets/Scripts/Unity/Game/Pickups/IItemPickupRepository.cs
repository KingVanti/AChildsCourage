using static AChildsCourage.RNG;

namespace AChildsCourage.Game.Pickups
{
    internal interface IItemPickupRepository
    {

        #region Methods

        ItemData GetNextItem(RNGSource rng);

        ItemData GetSpecificItem(int id);

        #endregion

    }
}
