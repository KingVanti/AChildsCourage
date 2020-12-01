﻿using System;
using System.Collections.Immutable;
using NUnit.Framework;

namespace AChildsCourage.Game.Monsters.Navigation
{

    [TestFixture]
    public class InvestigationTests
    {

        [Test]
        public void Started_Investigations_Have_No_Investigated_Positions()
        {
            // Given

            var started = Investigation.StartNew(
                new FloorState(new AOI()),
                new MonsterState(new EntityPosition(), DateTime.MinValue, InvestigationHistory.Empty),
                RNG.Always(0));

            // When

            var count = started.InvestigatedPositions.Count;

            // Then

            Assert.That(count, Is.Zero, "Should not have investigated positions!");
        }


        [Test]
        public void POIs_That_Are_Closer_Are_Chosen_Over_Ones_That_Are_Far_Away()
        {
            // Given

            var poi1 = new POI(new TilePosition(1, 0));
            var poi2 = new POI(new TilePosition(2, 0));
            var investigation = new Investigation(
                new AOI(AOIIndex.Zero, new TilePosition(), ImmutableArray.Create(poi1, poi2)),
                ImmutableHashSet<TilePosition>.Empty);
            var monsterPosition = new EntityPosition(0, 0);

            // When

            var next = Investigation.NextTarget(investigation, monsterPosition);

            // Then

            Assert.That(next, Is.EqualTo(poi1.Position), "Should choose closer POI!");
        }

        [Test]
        public void POIs_That_Were_Already_Visited_Should_Not_Be_Chosen()
        {
            // Given

            var poi1 = new POI(new TilePosition(1, 0));
            var poi2 = new POI(new TilePosition(2, 0));
            var investigation = new Investigation(
                new AOI(AOIIndex.Zero, new TilePosition(), ImmutableArray.Create(poi1, poi2)),
                ImmutableHashSet.Create(new TilePosition(1, 0)));
            var monsterPosition = new EntityPosition(0, 0);

            // When

            var next = Investigation.NextTarget(investigation, monsterPosition);

            // Then

            Assert.That(next, Is.EqualTo(poi2.Position), "Should not choose visited POI!");
        }


        [Test]
        public void An_Investigation_Is_Complete_If_Half_Of_All_POIs_Were_Explored()
        {
            // Given

            var investigation = new Investigation(
                new AOI(AOIIndex.Zero, new TilePosition(), ImmutableArray.Create(
                            new POI(new TilePosition(0, 0)),
                            new POI(new TilePosition(1, 1)))),
                ImmutableHashSet.Create(new TilePosition(0, 0)));

            // When

            var completed = Investigation.IsComplete(investigation);

            // Then

            Assert.That(completed, Is.True, "Investigation should be complete!");
        }

        [Test]
        public void An_Investigation_Is_Not_Complete_If_Less_Than_Half_Of_All_POIs_Were_Explored()
        {
            // Given

            var investigation = new Investigation(
                new AOI(AOIIndex.Zero, new TilePosition(), ImmutableArray.Create(
                            new POI(new TilePosition(0, 0)),
                            new POI(new TilePosition(1, 1)),
                            new POI(new TilePosition(2, 2)))),
                ImmutableHashSet.Create(new TilePosition(0, 0)));

            // When

            var completed = Investigation.IsComplete(investigation);

            // Then

            Assert.That(completed, Is.False, "Investigation should not be complete!");
        }


        [Test]
        public void AOIs_That_Are_Closer_Are_Chosen_Over_Ones_That_Are_Far_Away()
        {
            // Given

            var aoi1 = new AOI((AOIIndex) 1, new TilePosition(10, 0));
            var aoi2 = new AOI((AOIIndex) 2, new TilePosition(20, 0));
            var monsterState = new MonsterState(new EntityPosition(0, 0), DateTime.MinValue, InvestigationHistory.Empty);

            // When

            var weight1 = Investigation.CalcTotalWeight(aoi1, monsterState);
            var weight2 = Investigation.CalcTotalWeight(aoi2, monsterState);

            // Then

            Assert.That(weight1, Is.GreaterThan(weight2), "Should have higher weight when closer to monster!");
        }

        [Test]
        public void AOIs_That_Have_Not_Been_Visited_Longer_Are_Chosen_Over_Ones_That_Were_Visited_Recently()
        {
            // Given

            var aoi1 = new AOI((AOIIndex) 1, new TilePosition(2, 0));
            var aoi2 = new AOI((AOIIndex) 2, new TilePosition(2, 0));
            var monsterState = new MonsterState(new EntityPosition(1, 0), new DateTime(2020, 1, 1, 1, 1, 2), new InvestigationHistory(
                                                    new CompletedInvestigation((AOIIndex) 1, new DateTime(2020, 1, 1, 1, 1, 0)),
                                                    new CompletedInvestigation((AOIIndex) 2, new DateTime(2020, 1, 1, 1, 1, 1))));

            // When

            var weight1 = Investigation.CalcTotalWeight(aoi1, monsterState);
            var weight2 = Investigation.CalcTotalWeight(aoi2, monsterState);

            // Then

            Assert.That(weight1, Is.GreaterThan(weight2), "Should have higher weight when last investigation is further back!");
        }

        [Test]
        public void AOIs_That_Have_Never_Been_Visited_Chosen_Over_Ones_That_Were_Visited()
        {
            // Given

            var aoi1 = new AOI((AOIIndex) 1, new TilePosition(2, 0));
            var aoi2 = new AOI((AOIIndex) 2, new TilePosition(2, 0));
            var monsterState = new MonsterState(new EntityPosition(1, 0), new DateTime(2020, 1, 1, 1, 1, 2), new InvestigationHistory(
                                                    new CompletedInvestigation((AOIIndex) 2, new DateTime(2020, 1, 1, 1, 1, 1))));

            // When

            var weight1 = Investigation.CalcTotalWeight(aoi1, monsterState);
            var weight2 = Investigation.CalcTotalWeight(aoi2, monsterState);

            // Then

            Assert.That(weight1, Is.GreaterThan(weight2), "Should have higher weight when never been visited!");
        }

    }

}