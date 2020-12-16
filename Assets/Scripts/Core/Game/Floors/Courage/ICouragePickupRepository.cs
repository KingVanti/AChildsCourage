using static AChildsCourage.MRng;

namespace AChildsCourage.Game.Floors.Courage
{

    public interface ICouragePickupRepository
    {

        #region Methods

        CouragePickupAppearance GetCouragePickupData(CourageVariant variant);

        CouragePickupAppearance GetRandomPickupData(CreateRng createRng);

        #endregion

    }

}