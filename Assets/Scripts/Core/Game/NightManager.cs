using System.Linq;
using AChildsCourage.Game.Floors.RoomPersistence;
using AChildsCourage.Game.Items;
using static AChildsCourage.Game.MNightRecreating;
using static AChildsCourage.Game.Persistence.MRunData;
using static AChildsCourage.Game.MFloorGenerating;
using static AChildsCourage.MRng;

namespace AChildsCourage.Game
{

    [Singleton]
    internal class NightManager : INightManager
    {

        private readonly GenerationParameters floorPlanGenerationParameters;
        private readonly ItemId[] itemIds;
        private readonly RecreateNight recreateNight;
        private readonly RoomData[] roomData;


        public NightManager(LoadRoomData loadRoomData, IFloorRecreator floorRecreator)
        {
            roomData = loadRoomData().ToArray();
            recreateNight = Make(floorRecreator);

            floorPlanGenerationParameters = new GenerationParameters(15);
        }


        public void PrepareNightForCurrentRun()
        {
            var nightData = NewRun.Map(runData => StartNight(runData, RandomRng()));
            var rng = RngFromSeed(nightData.Seed);

            GenerateFloor(rng, roomData, floorPlanGenerationParameters).Do(recreateNight.Invoke);
        }

    }

}