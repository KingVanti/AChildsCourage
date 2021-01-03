using NUnit.Framework;
using UnityEngine;
using static AChildsCourage.Game.Shade.ShadeVision;
using static AChildsCourage.Game.Shade.Visibility;

namespace AChildsCourage.Game.Shade
{

    [TestFixture]
    public class ShadeVisionTests
    {

        [Test]
        public void Can_See_Point_If_It_Is_In_Any_Vision_Cone()
        {
            // Given

            var vision = CreateVisionWithCones(new VisionCone(Primary, 1, 360, true));
            var point = new Vector2(0, 0.5f);

            // When

            var canSeePoint = vision.Map(CanSeePoint, point);

            // Then

            Assert.That(canSeePoint, Is.True, "Should be able to see point!");
        }

        [Test]
        public void Cannot_See_Point_If_It_Is_Not_In_Any_Vision_Cone()
        {
            // Given

            var vision = CreateVisionWithCones(new VisionCone(Primary, 1, 360, true));
            var point = new Vector2(0, 1.5f);

            // When

            var canSeePoint = vision.Map(CanSeePoint, point);

            // Then

            Assert.That(canSeePoint, Is.False, "Should not be able to see point!");
        }

        [Test]
        public void Point_Visibility_Is_Visibility_Of_Highest_Value_Cone_That_Can_See_It()
        {
            // Given

            var vision = CreateVisionWithCones(new VisionCone(Primary, 1, 360, true),
                                               new VisionCone(Secondary, 1, 360, true));
            var point = new Vector2(0, 0.5f);

            // When

            var pointVisibility = GetPointVisibility(vision, point);

            // Then

            Assert.That(pointVisibility, Is.EqualTo(Primary), "Should have highest value visibility!");
        }


        private static ShadeVision CreateVisionWithCones(params VisionCone[] cones) =>
            new ShadeVision(new ShadeHead(Vector2.zero, Vector2.up, (point1, point2) => false),
                            cones);

    }

}