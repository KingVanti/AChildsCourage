using System.Linq;
using AChildsCourage.Game.Floors.RoomPersistance;
using AChildsCourage.Game.Persistance;
using static AChildsCourage.Game.MNightPreparation;
using static AChildsCourage.RNG;

namespace AChildsCourage.Game
{

    [Singleton]
    internal class NightManager : INightManager
    {

        #region Methods

        public void PrepareNight()
        {
            loadRunData()
                .Map(d => MRunData.StartNight(d, New()))
                .Do(prepareNight.Invoke);
        }

        #endregion

        #region Fields

        private readonly LoadRunData loadRunData;
        private readonly PrepareNight prepareNight;

        #endregion

        #region Constructors

        public NightManager(LoadRunData loadRunData, LoadItemIds loadItemIds, LoadRoomData loadRoomData, IFloorRecreator floorRecreator)
        {
            this.loadRunData = loadRunData;
            var roomData = loadRoomData().ToArray();
            var itemIds = loadItemIds();

            var generateFloor = FloorGenerating.Make(itemIds, roomData);
            var recreateNight = NightRecreating.Make(floorRecreator);

            prepareNight = PrepareNightWithRandomFloor(roomData, generateFloor, recreateNight);
        }

        public NightManager(LoadRunData loadRunData, PrepareNight prepareNight)
        {
            this.loadRunData = loadRunData;
            this.prepareNight = prepareNight;
        }

        #endregion

    }

}