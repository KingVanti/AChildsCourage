using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.RoomPersistance;
using AChildsCourage.Game.NightLoading;
using AChildsCourage.Game.Persistance;

namespace AChildsCourage.Game
{

    [Singleton]
    internal class NightManager : INightManager
    {

        #region Fields

        private readonly IRunStorage runStorage;
        private readonly NightLoader nightLoader;

        #endregion

        #region Constructors

        public NightManager(IRunStorage runStorage, IRoomPassagesRepository roomPassagesRepository, RoomLoader roomLoader, IFloorRecreator floorRecreator)
        {
            this.runStorage = runStorage;

            nightLoader = NightLoading.NightLoading.Make(roomPassagesRepository, roomLoader, floorRecreator);
        }

        public NightManager(IRunStorage runStorage, NightLoader nightLoader)
        {
            this.runStorage = runStorage;
            this.nightLoader = nightLoader;
        }

        #endregion

        #region Methods

        public void PrepareNight()
        {
            var nightData = GetCurrentNightData();

            nightLoader(nightData);
        }

        private NightData GetCurrentNightData()
        {
            var runData = runStorage.LoadCurrent();

            return runData.CurrentNight;
        }

        #endregion

    }

}