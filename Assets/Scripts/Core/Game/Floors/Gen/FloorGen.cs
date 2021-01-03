using System.Collections.Immutable;
using static AChildsCourage.Game.Floors.Gen.FloorPlan;
using static AChildsCourage.Game.Floors.MFloor;

namespace AChildsCourage.Game.Floors.Gen
{

    public static class FloorGen
    {

        public static Floor CreateFloorFrom(FloorGenParams @params, FloorPlan floorPlan) =>
            new Floor(floorPlan.Map(GetGroundPositions).ToImmutableHashSet(),
                      floorPlan.Map(GenerateWalls).ToImmutableHashSet(),
                      floorPlan.Map(ChooseCouragePickups, @params).ToImmutableHashSet(),
                      floorPlan.Map(GetStaticObjects).ToImmutableHashSet(),
                      floorPlan.Map(ChooseRunes, @params).ToImmutableHashSet(),
                      floorPlan.EndRoomChunk);

    }

}