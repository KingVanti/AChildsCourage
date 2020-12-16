using static AChildsCourage.MRng;

namespace AChildsCourage.Game.Floors.Courage
{

    public interface ICouragePickupRepository
    {

        #region Methods

        CouragePickupData GetCouragePickupData(CourageVariant variant);

        CouragePickupData GetRandomPickupData(CreateRng createRng);

        #endregion

    }

}