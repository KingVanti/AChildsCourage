namespace AChildsCourage.Game.Courage {

    internal interface ICouragePickupRepository {

        #region Methods

        CouragePickupData GetCouragePickupData(CourageVariant variant);

        CouragePickupData GetRandomPickupData(IRNG rng);

        #endregion

    }

}
