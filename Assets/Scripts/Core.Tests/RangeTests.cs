using System;
using NUnit.Framework;

namespace AChildsCourage
{

    [TestFixture]
    public class RangeTests
    {

        [Test]
        public void Max_Cannot_Be_Larger_Than_Min([Random(1, float.MaxValue, 10)] float min, [Random(float.MinValue, -1, 10)] float max) =>

            // Then
            Assert.That(() => new Range<float>(min, max), Throws.InstanceOf<ArgumentException>());

        [Test]
        public void Max_Can_Be_Same_As_Min([Random(float.MinValue, float.MaxValue, 100)] float minMax) =>

            // Then
            Assert.That(() => new Range<float>(minMax, minMax), Throws.Nothing);

    }

}