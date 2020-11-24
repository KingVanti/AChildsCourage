using AChildsCourage.Game.Floors;
using Ninject;
using Ninject.Parameters;
using static AChildsCourage.F;

namespace AChildsCourage.Game.NightManagement.Loading
{

    internal static class NightLoading
    {

        internal static NightLoader GetDefault(IRoomPassagesRepository roomPassagesRepository, IRoomRepository roomRepository, IFloorRecreator floorRecreator, IKernel kernel)
        {
            return data =>
            {

                RNGSource rngSource = seed => kernel.Get<IRNG>(new ConstructorArgument("seed", seed));
                var floorPlanGenerator = FloorPlanGenerating.GetDefault(roomPassagesRepository, rngSource);

                RoomLoader roomLoader = plan => roomRepository.LoadRoomsFor(plan);
                var floorGenerator = FloorGenerating.GetDefault(roomLoader);

                var nightRecreator = NightRecreating.GetDefault(floorRecreator);

                Load(data, floorPlanGenerator, floorGenerator, nightRecreator);
            };
        }


        internal static void Load(NightData nightData, FloorPlanGenerator floorPlanGenerator, FloorGenerator floorGenerator, NightRecreator nightRecreator) =>
          Take(nightData.Seed)
          .Map(floorPlanGenerator.Invoke)
          .Map(floorGenerator.Invoke)
          .Do(nightRecreator.Invoke);

    }

}