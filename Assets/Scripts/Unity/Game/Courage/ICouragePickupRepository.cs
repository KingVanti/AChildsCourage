using AChildsCourage.Game.Floors;
using static AChildsCourage.MRng;

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