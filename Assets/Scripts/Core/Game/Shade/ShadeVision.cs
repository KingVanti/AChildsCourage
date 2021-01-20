using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using AChildsCourage.Game.Char;
using UnityEngine;
using static AChildsCourage.Game.Shade.VisionCone;
using static AChildsCourage.Game.Char.Visibility;

namespace AChildsCourage.Game.Shade
{

    public readonly struct ShadeVision
    {

        public static bool CanSeePoint(Vector2 point, ShadeVision vision) =>
            vision.VisionCones
                  .Any(cone => cone.Map(Contains, vision.Head, point));

        public static Visibility GetPointVisibility(ShadeVision vision, Vector2 point) =>
            vision.VisionCones
                  .Where(cone => cone.Map(Contains, vision.Head, point))
                  .Select(cone => cone.Visibility)
                  .Match(GetHighestValue,
                         () => notVisible);

        public ShadeHead Head { get; }

        public ImmutableHashSet<VisionCone> VisionCones { get; }


        public ShadeVision(ShadeHead head, IEnumerable<VisionCone> visionCones)
        {
            Head = head;
            VisionCones = visionCones.ToImmutableHashSet();
        }

    }

}