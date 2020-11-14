using NUnit.Framework;

namespace AChildsCourage
{

    [TestFixture]
    public class RNGTests
    {

        #region Tests

        [Test]
        public void Two_RNGs_With_Same_Seed_Yield_Same_Values()
        {
            // Given

            var seed = 31482823;
            var rng1 = new RNG(seed);
            var rng2 = new RNG(seed);

            // When/Then

            for (var i = 0; i < 10; i++)
            {
                var value1 = rng1.GetValueBetween(4.123f, 8.6622f);
                var value2 = rng2.GetValueBetween(4.123f, 8.6622f);

                Assert.That(value1, Is.EqualTo(value2), $"Values differed after {i} queries!");
            }
        }


        #endregion

    }

}