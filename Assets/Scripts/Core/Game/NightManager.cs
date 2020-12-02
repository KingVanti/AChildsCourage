using System.Linq;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.RoomPersistance;
using AChildsCourage.Game.Items;
using static AChildsCourage.Game.NightRecreating;
using static AChildsCourage.Game.Persistance.MRunData;
using static AChildsCourage.Game.FloorGenerating;
using static AChildsCourage.Game.MFloorPlanGenerating;

namespace AChildsCourage.Game
{

    [Singleton]
    internal class NightManager : INightManager
    {

        #region Constructors

        public NightManager(LoadRunData loadRunData, LoadItemIds loadItemIds, LoadRoomData loadRoomData, IFloorRecreator floorRecreator)
        {
            this.loadRunData = loadRunData;
            roomData = loadRoomData().ToArray();
            itemIds = loadItemIds().ToArray();
            recreateNight = Make(floorRecreator);

            floorPlanGenerationParameters = new GenerationParameters(
                roomData.SelectMany(d => d.GetPassageVariations()).ToArray());
        }

        #endregion

        #region Methods

        public void PrepareNightForCurrentRun()
        {
            loadRunData()
                .Map(runData => StartNight(runData, RNG.New()))
                .Map(nightData => GenerateFloorPlan(floorPlanGenerationParameters, RNG.FromSeed(nightData.Seed)))
                .Map(floorPlan => GenerateFloor(floorPlan, itemIds, roomData))
                .Do(recreateNight.Invoke);
        }

        #endregion

        #region Fields

        private readonly GenerationParameters floorPlanGenerationParameters;
        private readonly LoadRunData loadRunData;
        private readonly RoomData[] roomData;
        private readonly ItemId[] itemIds;
        private readonly RecreateNight recreateNight;

        #endregion

    }

}