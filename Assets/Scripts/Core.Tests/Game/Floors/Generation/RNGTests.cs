using NUnit.Framework;
using System;

namespace AChildsCourage.Game.Floors.Generation
{

    [TestFixture]
    public class RNGTests
    {

        #region Tests

        [Test]
        public void Given_Same_Seed_The_RNG_Outcome_Is_Exactly_The_Same()
        {

            // Given

            int seed = 31482823;
            Random random = new Random(seed);
            RNG rng1 = new RNG(seed);
            RNG rng2 = new RNG(seed);

            // When

            float rng3 = rng1.GetValueBetween(4.123f, 8.6622f);
            float rng4 = rng2.GetValueBetween(4.123f, 8.6622f);

            // Then

            Assert.That(rng3 == rng4);

        }


        #endregion


    }

}