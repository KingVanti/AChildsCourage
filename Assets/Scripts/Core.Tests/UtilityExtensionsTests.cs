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
        
        [Test]
        public void Adding_Z_To_A_Vector_Does_Not_Change_The_Other_Coordinates([Random(0f, 1f, 100)] float coordinates)
        {
            // Given

            var vector = new Vector2(coordinates, coordinates);

            // When

            var withZ = vector.WithZ(1);

            // Then

            Assert.That(withZ.x, Is.EqualTo(vector.x), "X changed!");
            Assert.That(withZ.y, Is.EqualTo(vector.y), "Y changed!");
        }

        [Test]
        public void Adding_Z_To_A_Vector_Changes_The_Z_Coordinate([Random(0f, 1f, 100)] float z)
        {
            // Given

            var vector = new Vector2(0, 0);

            // When

            var withZ = vector.WithZ(z);

            // Then

            Assert.That(withZ.z, Is.EqualTo(z), "Z was not added correctly!");
        }

    }

}