namespace AChildsCourage.Game.Pickups
{
    internal interface IItemPickupRepository
    {

        #region Methods

        ItemData GetNextItem(IRNG rng);

        ItemData GetSpecificItem(int id);

        #endregion

    }
}
