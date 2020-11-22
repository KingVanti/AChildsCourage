using AChildsCourage.Game.Floors;

namespace AChildsCourage.Game.Courage
{

    public interface ICouragePickupRepository
    {

        #region Methods

        CouragePickupData GetCouragePickupData(CourageVariant variant);

        CouragePickupData GetRandomPickupData(IRNG rng);

        #endregion

    }

}
