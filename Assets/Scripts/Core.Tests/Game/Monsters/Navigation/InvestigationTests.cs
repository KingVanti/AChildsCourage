﻿using System;
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