using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using UnityEngine;
using static AChildsCourage.Game.Shade.MVisibility;
using static AChildsCourage.Game.Shade.MVisionCone;

namespace AChildsCourage.Game.Shade
{

    public static class MShadeVision
    {

        public static bool CanSeePoint(ShadeVision vision, Vector2 point) =>
            vision.VisionCones
                  .Any(cone => cone.Map(Contains, vision.Head, point));

        public static Visibility GetPointVisibility(ShadeVision vision, Vector2 point) =>
            vision.VisionCones
                  .Where(cone => cone.Map(Contains, vision.Head, point))
                  .Select(cone => cone.Visibility)
                  .Match(visibilities => GetHighestValue(visibilities),
                         () => Visibility.NotVisible);


        public readonly struct ShadeVision
        {

            public ShadeHead Head { get; }

            public ImmutableHashSet<VisionCone> VisionCones { get; }


            public ShadeVision(ShadeHead head, IEnumerable<VisionCone> visionCones)
            {
                Head = head;
                VisionCones = visionCones.ToImmutableHashSet();
            }

        }

    }

}