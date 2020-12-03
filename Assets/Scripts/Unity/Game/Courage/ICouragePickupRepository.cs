using AChildsCourage.Game.Floors;
using static AChildsCourage.RNG;

namespace AChildsCourage.Game.Courage
{

    public interface ICouragePickupRepository
    {

        #region Methods

        CouragePickupData GetCouragePickupData(CourageVariant variant);

        CouragePickupData GetRandomPickupData(CreateRNG createRng);

        #endregion

    }

}