using AChildsCourage.Game.Floors;
using static AChildsCourage.Rng;

namespace AChildsCourage.Game.Courage
{

    public interface ICouragePickupRepository
    {

        #region Methods

        CouragePickupData GetCouragePickupData(CourageVariant variant);

        CouragePickupData GetRandomPickupData(CreateRng createRng);

        #endregion

    }

}