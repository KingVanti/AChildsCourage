using System.Collections.Generic;
using System.Collections.Immutable;
using AChildsCourage.Infrastructure;
using UnityEngine;

namespace AChildsCourage.Game.Floors.Courage
{

    internal static class CouragePickupAppearanceRepo
    {

        public delegate IEnumerable<CouragePickupAppearance> LoadCouragePickupAppearances();


        private const string RoomResourcePath = "Courage-Pickup Appearances/";


        [Service]
        public static LoadCouragePickupAppearances FromAssets => LoadAssets;

        private static IEnumerable<CouragePickupAppearance> LoadAssets() =>
            Resources.LoadAll<CouragePickupAppearance>(RoomResourcePath).ToImmutableHashSet();

    }

}