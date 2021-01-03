using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using AChildsCourage.Game.Floors.Courage;
using static AChildsCourage.Game.Floors.RoomPersistence.MRoomContentData;
using static AChildsCourage.Game.MTilePosition;
using static AChildsCourage.F;

namespace AChildsCourage.Game.Floors.Gen
{

    public readonly struct FloorPlan
    {

        private const int WallHeight = 2;


        public static FloorPlan EmptyFloorPlan => new FloorPlan(ImmutableHashSet<TilePosition>.Empty,
                                                                ImmutableHashSet<CouragePickup>.Empty,
                                                                ImmutableHashSet<StaticObject>.Empty,
                                                                ImmutableHashSet<Rune>.Empty);


        public static IEnumerable<TilePosition> GetGroundPositions(FloorPlan floorPlan) =>
            floorPlan.groundPositions;

        public static IEnumerable<Wall> GenerateWalls(FloorPlan floorPlan)
        {
            var groundPositions = floorPlan
                                  .Map(GetGroundPositions).ToImmutableHashSet();

            bool IsUnoccupied(TilePosition tilePosition) =>
                !groundPositions.Contains(tilePosition);

            IEnumerable<TilePosition> GetSurroundingWallPositions(TilePosition groundPosition) =>
                Grid.Generate(-1, -1, 3, 3 + WallHeight)
                    .Where(offset => offset.X != 0 || offset.Y != 0)
                    .Select(offset => groundPosition.Map(OffsetBy, new TileOffset(offset.X, offset.Y)));

            IEnumerable<TilePosition> GenerateWallPositions() =>
                groundPositions
                    .SelectMany(GetSurroundingWallPositions)
                    .Where(IsUnoccupied);


            Wall CreateWall(TilePosition wallPosition)
            {
                bool HasGroundBelow() =>
                    GetCheckGroundPositions()
                        .Any(groundPositions.Contains);

                IEnumerable<TilePosition> GetCheckGroundPositions() =>
                    GetGroundOffsets()
                        .Select(offset => wallPosition.Map(OffsetBy, offset));

                IEnumerable<TileOffset> GetGroundOffsets() =>
                    Enumerable.Range(-WallHeight, WallHeight)
                              .Select(y => new TileOffset(0, y));

                var wallType = HasGroundBelow()
                    ? WallType.Side
                    : WallType.Top;

                return new Wall(wallPosition, wallType);
            }


            return GenerateWallPositions()
                .Select(CreateWall);
        }

        public static IEnumerable<CouragePickup> ChooseCouragePickups(FloorPlan floorPlan, FloorGenParams @params)
        {
            var rng = MRng.RngFromSeed(@params.Seed);

            float CalculateCourageOrbWeight(TilePosition position, ImmutableHashSet<TilePosition> taken)
            {
                var distanceOriginWeight =
                    position
                        .Map(GetDistanceFromOrigin)
                        .Clamp(20, 40)
                        .Remap(20f, 40f, 1, 10f);

                var distanceToClosestWeight = taken.Any()
                    ? taken.Select(p => p.Map(DistanceTo, position)).Min()
                           .Clamp(10, 30)
                           .Remap(10, 30, 1, 20)
                    : 20;

                return distanceOriginWeight + distanceToClosestWeight;
            }

            float CalculateCourageSparkWeight(TilePosition position, ImmutableHashSet<TilePosition> taken)
            {
                var distanceToClosestWeight = taken.Any()
                    ? taken.Select(p => p.Map(DistanceTo, position)).Min()
                           .Clamp(1, 10)
                           .Remap(1, 10, 4, 1)
                    : 1;

                return distanceToClosestWeight;
            }

            IEnumerable<TilePosition> GetCouragePositionsOfVariant(CourageVariant variant) =>
                floorPlan.couragePickups
                         .Where(p => p.Variant == variant)
                         .Select(p => p.Position);

            TilePosition ChooseNextPickupPosition(IEnumerable<TilePosition> positions, ImmutableHashSet<TilePosition> taken, CouragePickupWeightFunction weightFunction)
            {
                bool IsNotTaken(TilePosition p) => !taken.Contains(p);

                float CalculateWeight(TilePosition p) => weightFunction(p, taken);

                return Take(positions)
                       .Where(IsNotTaken)
                       .GetWeightedRandom(CalculateWeight, rng);
            }

            IEnumerable<CouragePickup> ChoosePickupsOfVariant(CourageVariant variant, CouragePickupWeightFunction weightFunction)
            {
                var positions = GetCouragePositionsOfVariant(variant).ToImmutableHashSet();

                ImmutableHashSet<TilePosition> AddNext(ImmutableHashSet<TilePosition> taken) => taken.Add(ChooseNextPickupPosition(positions, taken, weightFunction));

                return AggregateTimes(ImmutableHashSet<TilePosition>.Empty, AddNext, @params.CouragePickupCounts[variant])
                    .Select(p => new CouragePickup(p, variant));
            }

            return ChoosePickupsOfVariant(CourageVariant.Spark, CalculateCourageSparkWeight)
                .Concat(ChoosePickupsOfVariant(CourageVariant.Orb, CalculateCourageOrbWeight));
        }

        public static IEnumerable<StaticObject> GetStaticObjects(FloorPlan floorPlan) =>
            floorPlan.staticObjects;

        public static IEnumerable<Rune> ChooseRunes(FloorPlan floorPlan, FloorGenParams @params)
        {
            var rng = MRng.RngFromSeed(@params.Seed);

            float CalculateRuneWeight(TilePosition position, ImmutableHashSet<TilePosition> taken)
            {
                var distanceToClosestWeight = taken.Any()
                    ? taken.Select(p => p.Map(DistanceTo, position)).Min()
                           .Clamp(25, 60)
                           .Remap(25, 60, 1, 20)
                    : 20;

                return distanceToClosestWeight;
            }

            TilePosition ChooseNextRunePosition(IEnumerable<TilePosition> positions, ImmutableHashSet<TilePosition> taken)
            {
                bool IsNotTaken(TilePosition p) => !taken.Contains(p);

                float CalculateWeight(TilePosition p) => CalculateRuneWeight(p, taken);

                return Take(positions)
                       .Where(IsNotTaken)
                       .GetWeightedRandom(CalculateWeight, rng);
            }

            var runePositions = floorPlan.runes
                                         .Select(r => r.Position)
                                         .ToImmutableHashSet();

            ImmutableHashSet<TilePosition> AddNext(ImmutableHashSet<TilePosition> taken) => 
                taken.Add(ChooseNextRunePosition(runePositions, taken));

            return AggregateTimes(ImmutableHashSet<TilePosition>.Empty, AddNext, @params.RuneCount)
                .Select(p => new Rune(p));
        }

        public static FloorPlan AddContent(FloorPlan floorPlan, RoomContentData content)
        {
            var groundPositions = content.GroundData.Select(g => g.Position);
            var couragePickups = content.CourageData.Select(c => new CouragePickup(c.Position, c.Variant));
            var staticObjects = content.StaticObjects.Select(s => new StaticObject(s.Position));
            var runes = content.Runes.Select(r => new Rune(r.Position));

            return new FloorPlan(floorPlan.groundPositions.Union(groundPositions),
                                 floorPlan.couragePickups.Union(couragePickups),
                                 floorPlan.staticObjects.Union(staticObjects),
                                 floorPlan.runes.Union(runes));
        }


        private readonly ImmutableHashSet<TilePosition> groundPositions;
        private readonly ImmutableHashSet<CouragePickup> couragePickups;
        private readonly ImmutableHashSet<StaticObject> staticObjects;
        private readonly ImmutableHashSet<Rune> runes;


        private FloorPlan(ImmutableHashSet<TilePosition> groundPositions, ImmutableHashSet<CouragePickup> couragePickups, ImmutableHashSet<StaticObject> staticObjects, ImmutableHashSet<Rune> runes)
        {
            this.groundPositions = groundPositions;
            this.couragePickups = couragePickups;
            this.staticObjects = staticObjects;
            this.runes = runes;
        }


        private delegate float CouragePickupWeightFunction(TilePosition position, ImmutableHashSet<TilePosition> taken);

    }

}