using NUnit.Framework;
using UnityEngine;

namespace AChildsCourage
{

    [TestFixture]
    public class UtilityExtensionsTests
    {

        [Test]
        public void Changing_The_Alpha_Of_A_Color_Does_Not_Change_The_Other_Channels([Random(0f, 1f, 100)] float channel)
        {
            // Given

            var color = new Color(channel, channel, channel, 0);

            // When

            var changed = color.WithAlpha(1);

            // Then

            Assert.That(changed.r, Is.EqualTo(color.r), "R channel changed!");
            Assert.That(changed.g, Is.EqualTo(color.g), "R channel changed!");
            Assert.That(changed.b, Is.EqualTo(color.b), "R channel changed!");
        }

        [Test]
        public void Changing_The_Alpha_Of_A_Color_Changes_The_Alpha_Channel([Random(0f, 1f, 100)] float alpha)
        {
            // Given

            var color = new Color(0, 0, 0, 0);

            // When

            var changed = color.WithAlpha(alpha);

            // Then

            Assert.That(changed.a, Is.EqualTo(alpha), "A channel did not change!");
        }

    }

}