using AChildsCourage.Game.Floors;
using Ninject;
using Ninject.Parameters;

using static AChildsCourage.F;

namespace AChildsCourage.Game.NightManagement.Loading
{

    [Singleton]
    internal class NightLoader : INightLoader
    {

        #region Fields

        private readonly FloorPlanGenerator floorPlanGenerator;
        private readonly FloorGenerator floorGenerator;
        private readonly NightRecreator nightRecreator;

        #endregion

        #region Constructors

        public NightLoader(IRoomPassagesRepository roomPassagesRepository, IRoomRepository roomRepository, IFloorRecreator floorRecreator, IKernel kernel)
        {
            RNGSource rngSource = seed => kernel.Get<IRNG>(new ConstructorArgument("seed", seed));
            floorPlanGenerator = FloorPlanGenerating.GetDefault(roomPassagesRepository, rngSource);

            RoomLoader roomLoader = plan => roomRepository.LoadRoomsFor(plan);
            floorGenerator = FloorGenerating.GetDefault(roomLoader);

            nightRecreator = NightRecreating.GetDefault(floorRecreator);
        }

        #endregion

        #region Methods

        public void Load(NightData nightData) =>
            Pipe(nightData.Seed)
            .Into(floorPlanGenerator.Invoke)
            .Into(floorGenerator.Invoke)
            .Into(nightRecreator.Invoke);

        #endregion

    }

}