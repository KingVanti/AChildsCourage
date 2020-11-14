namespace AChildsCourage.Game.Pickups
{
    public interface IItemPickupRepository {

        ItemData GetNextItem(IRNG rng);

    }
}
