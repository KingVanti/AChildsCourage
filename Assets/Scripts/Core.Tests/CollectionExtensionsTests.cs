using NUnit.Framework;
using System.Linq;

namespace AChildsCourage
{

    [TestFixture]
    public class CollectionExtensionsTests
    {

        #region Methods

        [Test]
        public void Getting_A_Random_Element_From_Empty_Collection_Returns_Default()
        {
            // Given

            var elements = new int[0];
            var rng = new RNG(0);

            // When

            var element = elements.GetRandom(rng);

            // Then

            Assert.That(element, Is.Zero, "Did not get default element!");
        }

        [Test]
        public void Getting_A_Random_Weighted_Element_From_Empty_Collection_Returns_Default()
        {
            // Given

            var elements = new int[0];
            var rng = new RNG(0);

            // When

            var element = elements.GetWeightedRandom(e => e, rng);

            // Then

            Assert.That(element, Is.Zero, "Did not get default element!");
        }

        [Test]
        public void Elements_With_Double_The_Weight_Appear_Approximatly_Double_As_Often()
        {
            // Given

            var elements = new[] { 1, 2 };
            var rng = new RNG(0);

            // When

            var numbers = new int[1000];

            for (int i = 0; i < numbers.Length; i++)
                numbers[i] = elements.GetWeightedRandom(e => e, rng);

            // Then

            var element1Count = numbers.Count(n => n == 1);
            var element2Count = numbers.Count(n => n == 2);

            Assert.That(element2Count, Is.EqualTo(element1Count * 2).Within(100), "Element not selected approximately double as often!");
        }

        #endregion

    }

}