using AChildsCourage.Game.Floors;

using static AChildsCourage.F;
using static AChildsCourage.Game.NightManagement.Loading.FloorPlanGeneratingUtility;

namespace AChildsCourage.Game.NightManagement.Loading
{

    internal static class FloorPlanGenerating
    {

        internal const int GoalRoomCount = 15;


        internal static FloorPlanGenerator GetDefault(IRoomPassagesRepository roomPassagesRepository, RNGSource rngSource)
        {
            return seed =>
            {
                var rng = rngSource(seed);
                var roomAdder = RoomAdding.GetDefault(roomPassagesRepository, rng);
                var floorPlanCreator = FloorPlanCreating.GetDefault();

                return Generate(roomAdder, floorPlanCreator);
            };
        }


        private static FloorPlan Generate(RoomAdder roomBuilder, FloorPlanCreator floorPlanCreator) =>
            Take(new FloorPlanInProgress())
            .RepeatWhile(roomBuilder.Invoke, NeedsMoreRooms)
            .Map(floorPlanCreator.Invoke);
        
        internal static bool NeedsMoreRooms(FloorPlanInProgress floorPlan) =>
            Take(floorPlan)
            .Map(CountRooms)
            .Map(IsEnough)
            .Negate();

        internal static bool IsEnough(int currentRoomCount) =>
            currentRoomCount >= GoalRoomCount;

    }

}