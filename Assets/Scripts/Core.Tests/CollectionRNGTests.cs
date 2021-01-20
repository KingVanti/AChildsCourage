using System;
using NUnit.Framework;
using static AChildsCourage.Rng;

namespace AChildsCourage
{

    [TestFixture]
    public class CollectionRngTests
    {

        [Test]
        public void Getting_A_Random_Element_From_A_Non_Empty_Collection_Gets_The_Correct_Element()
        {
            // Given

            var elements = new[] {1, 2, 3};

            // When/Then

            var element = elements.TryGetRandom(ConstantRng(0), () => 0);

            // Then

            Assert.That(element, Is.EqualTo(1), "Did not get correct element!");
        }

        [Test]
        public void Getting_A_Random_Element_From_Empty_Collection_Continues_With_Correct_Action()
        {
            // Given

            var elements = new int[0];

            // When/Then

            var element = elements.TryGetRandom(ConstantRng(0), () => 0);

            // Then

            Assert.That(element, Is.Zero, "Did not get default element!");
        }

        [Test]
        public void Getting_A_Random_Weighted_Element_From_Empty_Collection_Continues_With_Correct_Action()
        {
            // Given

            var elements = new int[0];

            // When

            var element = elements.TryGetWeightedRandom(e => e, ConstantRng(0), () => 0);

            // Then

            Assert.That(element, Is.Zero, "Did not get default element!");
        }

        [Test]
        public void Elements_With_Double_The_Weight_Appear_Approximately_Double_As_Often()
        {
            // Given

            var elements = new[] {1, 2};
            var rng = RngFromSeed(0);

            // When

            var element1Count = 0;
            var element2Count = 0;

            for (var i = 0; i < 1000; i++)
                if (elements.TryGetWeightedRandom(e => e, rng, () => throw new Exception("Empty!")) == 1)
                    element1Count++;
                else
                    element2Count++;

            // Then

            Assert.That(element2Count, Is.EqualTo(element1Count * 2)
                                         .Within(100), "Element not selected approximately double as often!");
        }

    }

}