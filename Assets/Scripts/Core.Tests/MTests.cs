using NUnit.Framework;
using static AChildsCourage.M;

namespace AChildsCourage
{

    [TestFixture]
    public class MTests
    {

        [Test]
        public void Remapped_Float_Values_Are_Always_Inside_The_Target_Range([Random(float.MinValue, float.MaxValue, 100)] float value)
        {
            // When

            var remapped = value.Map(Remap, float.MinValue, float.MaxValue, 0f, 1f);

            // Then

            Assert.That(remapped, Is.GreaterThanOrEqualTo(0), "Value should not be below target minimum!");
            Assert.That(remapped, Is.LessThanOrEqualTo(1), "Value should not be above target maximum!");
        }

        [Test]
        public void Float_Remappings_Can_Be_Reversed([Random(float.MinValue, float.MaxValue, 100)] float value)
        {
            // When

            var remapped = value.Map(Remap, float.MinValue, float.MaxValue, 0f, 1f);
            var reversed = remapped.Map(Remap, 0f, 1f, float.MinValue, float.MaxValue);

            // Then

            Assert.That(reversed, Is.EqualTo(value).Within(0.01f).Percent, "Reversed value should be equal to original!");
        }

        [Test]
        public void Remapped_Int_Values_Are_Always_Inside_The_Target_Range([Random(int.MinValue, int.MaxValue, 100)] int value)
        {
            // When

            var remapped = value.Map(Remap, int.MinValue, int.MaxValue, 0f, 1f);

            // Then

            Assert.That(remapped, Is.GreaterThanOrEqualTo(0), "Value should not be below target minimum!");
            Assert.That(remapped, Is.LessThanOrEqualTo(1), "Value should not be above target maximum!");
        }

        [Test]
        public void Int_Remappings_Can_Be_Reversed([Random(int.MinValue, int.MaxValue, 100)] int value)
        {
            // When

            var remapped = value.Map(Remap, int.MinValue, int.MaxValue, 0f, 1f);
            var reversed = remapped.Map(Remap, 0f, 1f, (float) int.MinValue, (float) int.MaxValue);

            // Then

            Assert.That(reversed, Is.EqualTo(value).Within(0.01f).Percent, "Reversed value should be equal to original!");
        }

        [Test]
        public void Clamped_Float_Values_Are_Always_Inside_The_Target_Range([Random(float.MinValue, float.MaxValue, 100)] float value)
        {
            // When

            var remapped = value.Map(Clamp, 0f, 1f);

            // Then

            Assert.That(remapped, Is.GreaterThanOrEqualTo(0), "Value should not be below target minimum!");
            Assert.That(remapped, Is.LessThanOrEqualTo(1), "Value should not be above target maximum!");
        }

        [Test]
        public void Clamped_Int_Values_Are_Always_Inside_The_Target_Range([Random(int.MinValue, int.MaxValue, 100)] int value)
        {
            // When

            var remapped = value.Map(Clamp, 0, 1);

            // Then

            Assert.That(remapped, Is.GreaterThanOrEqualTo(0), "Value should not be below target minimum!");
            Assert.That(remapped, Is.LessThanOrEqualTo(1), "Value should not be above target maximum!");
        }

        [Test]
        public void Inverting_A_Value_Twice_Gives_The_Original_Value([Random(float.MinValue, float.MaxValue, 100)] float value)
        {
            // When

            var inverted = value.Map(Inverse);
            var reverted = inverted.Map(Inverse);

            // Then

            Assert.That(reverted, Is.EqualTo(value).Within(0.01f).Percent, "Inverting value twice should give original value!");
        }

        [Test]
        public void Inverting_0_Returns_0()
        {
            // When

            var inverted = 0f.Map(Inverse);

            // Then

            Assert.That(inverted, Is.Zero, "Inverting 0 should return 0!");
        }

        [Test]
        public void Inverting_A_Number_Greater_Than_1_Gives_A_Number_Between_0_And_1([Random(1.1f, float.MaxValue, 100)] float value)
        {
            // When

            var inverted = value.Map(Inverse);

            // Then

            Assert.That(inverted, Is.GreaterThan(0), "Inverted number should be larger than 0!");
            Assert.That(inverted, Is.LessThan(1), "Inverted number should be less than 1!");
        }

        [Test]
        public void Inverting_A_Number_Between_0_And_1_Gives_A_Number_Greater_Than_1([Random(0.1f, 0.99f, 100)] float value)
        {
            // When

            var inverted = value.Map(Inverse);

            // Then

            Assert.That(inverted, Is.GreaterThan(1), "Inverted number should be larger than 1!");
        }

        [Test]
        public void Inverting_A_Number_Less_Than_Negative_1_Gives_A_Number_Between_0_And_Negative_1([Random(float.MinValue, -1.1f, 100)] float value)
        {
            // When

            var inverted = value.Map(Inverse);

            // Then

            Assert.That(inverted, Is.LessThan(0), "Inverted number should be less than 0!");
            Assert.That(inverted, Is.GreaterThan(-1), "Inverted number should be greater than -1!");
        }

        [Test]
        public void Inverting_A_Number_Between_0_And_Negative_1_Gives_A_Number_Less_Than_Negative_1([Random(-0.99f, -0.1f, 100)] float value)
        {
            // When

            var inverted = value.Map(Inverse);

            // Then

            Assert.That(inverted, Is.LessThan(-1), "Inverted number should be less than -1!");
        }
        
        [Test]
        public void A_Normalized_Angle_Is_Between_0_And_360([Random(float.MinValue, float.MaxValue, 100)] float angle)
        {
            // When

            var normalized = angle.Map(NormalizeAngle);

            // Then

            Assert.That(normalized, Is.GreaterThanOrEqualTo(0), "Normalized angle should be greater than 0!");
            Assert.That(normalized, Is.LessThanOrEqualTo(360), "Normalized angle should be less than 360!");
        }

    }

}