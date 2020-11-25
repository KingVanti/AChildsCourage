using AChildsCourage.Game.Floors.RoomPersistance;
using AChildsCourage.Game.NightLoading;

namespace AChildsCourage.Game
{

    [Singleton]
    internal class NightManager : INightManager
    {

        #region Fields

        private readonly RunDataLoader runDataLoader;
        private readonly NightLoader nightLoader;

        #endregion

        #region Constructors

        public NightManager(RunDataLoader runDataLoader, RoomDataLoader roomLoader, IFloorRecreator floorRecreator)
        {
            this.runDataLoader = runDataLoader;

            nightLoader = NightLoading.NightLoading.Make(roomLoader, floorRecreator);
        }

        public NightManager(RunDataLoader runDataLoader, NightLoader nightLoader)
        {
            this.runDataLoader = runDataLoader;
            this.nightLoader = nightLoader;
        }

        #endregion

        #region Methods

        public void PrepareNight()
        {
            runDataLoader()
                .Map(d => d.CurrentNight)
                .Do(nightLoader.Invoke);
        }

        #endregion

    }

}