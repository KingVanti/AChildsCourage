using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using AChildsCourage.Game.Char;
using UnityEngine;
using static AChildsCourage.Game.Shade.VisionCone;
using static AChildsCourage.Game.Char.Visibility;

namespace AChildsCourage.Game.Shade
{

    internal readonly struct ShadeVision
    {

        internal static bool CanSeePoint(Vector2 point, ShadeVision vision) =>
            vision.VisionCones
                  .Any(cone => cone.Map(Contains, vision.Head, point));

        internal static Visibility GetPointVisibility(ShadeVision vision, Vector2 point) =>
            vision.VisionCones
                  .Where(cone => cone.Map(Contains, vision.Head, point))
                  .Select(cone => cone.Visibility)
                  .Match(GetHighestValue,
                         () => notVisible);

        private ShadeHead Head { get; }

        private ImmutableHashSet<VisionCone> VisionCones { get; }


        public ShadeVision(ShadeHead head, IEnumerable<VisionCone> visionCones)
        {
            Head = head;
            VisionCones = visionCones.ToImmutableHashSet();
        }

    }

}