using NUnit.Framework;
using UnityEngine;
using static AChildsCourage.Game.Shade.VisionCone;
using static AChildsCourage.Game.Shade.Visibility;

namespace AChildsCourage.Game.Shade
{

    [TestFixture]
    public class VisionConeTests
    {

        [Test]
        public void Point_That_Is_Inside_VisionCones_ViewRadius_Can_Be_Seen()
        {
            // Given

            var cone = new VisionCone(Primary, 1, 360, true);
            var head = new ShadeHead(Vector2.zero, Vector2.up, (point1, point2) => false);
            var point = new Vector2(0, 0.5f);

            // When

            var canSeePoint = cone.Map(Contains, head, point);

            // Then

            Assert.That(canSeePoint, Is.True, "Should be able to see point!");
        }

        [Test]
        public void Point_That_Is_Outside_VisionCones_ViewRadius_Cannot_Be_Seen()
        {
            // Given

            var cone = new VisionCone(Primary, 1, 360, true);
            var head = new ShadeHead(Vector2.zero, Vector2.up, (point1, point2) => false);
            var point = new Vector2(0, 1.5f);

            // When

            var canSeePoint = cone.Map(Contains, head, point);

            // Then

            Assert.That(canSeePoint, Is.False, "Should not be able to see point!");
        }

        [Test]
        public void Point_That_Is_Inside_VisionCones_ViewAngle_Can_Be_Seen()
        {
            // Given

            var cone = new VisionCone(Primary, 1, 90, true);
            var head = new ShadeHead(Vector2.zero, Vector2.up, (point1, point2) => false);
            var point = new Vector2(0, 0.5f);

            // When

            var canSeePoint = cone.Map(Contains, head, point);

            // Then

            Assert.That(canSeePoint, Is.True, "Should be able to see point!");
        }

        [Test]
        public void Point_That_Is_Outside_VisionCones_ViewAngle_Cannot_Be_Seen()
        {
            // Given

            var cone = new VisionCone(Primary, 1, 90, true);
            var head = new ShadeHead(Vector2.zero, Vector2.up, (point1, point2) => false);
            var point = new Vector2(0, -0.5f);

            // When

            var canSeePoint = cone.Map(Contains, head, point);

            // Then

            Assert.That(canSeePoint, Is.False, "Should not be able to see point!");
        }

        [Test]
        public void Point_That_Is_Unobstructed_Can_Be_Seen()
        {
            // Given

            var cone = new VisionCone(Primary, 1, 90, false);
            var head = new ShadeHead(Vector2.zero, Vector2.up, (point1, point2) => false);
            var point = new Vector2(0, 0.5f);

            // When

            var canSeePoint = cone.Map(Contains, head, point);

            // Then

            Assert.That(canSeePoint, Is.True, "Should be able to see point!");
        }

        [Test]
        public void Point_That_Is_Obstructed_Cannot_Be_Seen_When_Cone_Cannot_See_Through_Walls()
        {
            // Given

            var cone = new VisionCone(Primary, 1, 90, false);
            var head = new ShadeHead(Vector2.zero, Vector2.up, (point1, point2) => true);
            var point = new Vector2(0, 0.5f);

            // When

            var canSeePoint = cone.Map(Contains, head, point);

            // Then

            Assert.That(canSeePoint, Is.False, "Should not be able to see point!");
        }

        [Test]
        public void Point_That_Is_Obstructed_Can_Be_Seen_When_Cone_Can_See_Through_Walls()
        {
            // Given

            var cone = new VisionCone(Primary, 1, 90, true);
            var head = new ShadeHead(Vector2.zero, Vector2.up, (point1, point2) => true);
            var point = new Vector2(0, 0.5f);

            // When

            var canSeePoint = cone.Map(Contains, head, point);

            // Then

            Assert.That(canSeePoint, Is.True, "Should be able to see point!");
        }

    }

}