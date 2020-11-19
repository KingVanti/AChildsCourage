using AChildsCourage.Game.NightManagement;
using AChildsCourage.Game.NightManagement.Loading;
using AChildsCourage.Game.Persistance;

namespace AChildsCourage.Game
{

    [Singleton]
    internal class NightManager : INightManager
    {

        #region Fields

        private readonly IRunStorage runStorage;
        private readonly INightLoader nightLoader;

        #endregion

        #region Constructors

        public NightManager(IRunStorage runStorage, INightLoader nightLoader)
        {
            this.runStorage = runStorage;
            this.nightLoader = nightLoader;
        }

        #endregion

        #region Methods

        public void PrepareNight()
        {
            var nightData = GetCurrentNightData();

            nightLoader.Load(nightData);
        }

        private NightData GetCurrentNightData()
        {
            var runData = runStorage.LoadCurrent();

            return runData.CurrentNight;
        }

        #endregion

    }

}