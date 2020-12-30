using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using AChildsCourage.Infrastructure;
using UnityEngine;

namespace AChildsCourage.Game.Floors.Courage
{

    internal static class CouragePickupAppearanceRepo
    {

        public delegate IEnumerable<CouragePickupAppearance> LoadCouragePickupAppearances();


        private const string RoomResourcePath = "Courage-Pickup Appearances/";


        [Service]
        public static LoadCouragePickupAppearances FromAssets =>
            () => LoadAssets().Select(a => a.Appearance);

        private static IEnumerable<CouragePickupAppearanceAsset> LoadAssets() =>
            Resources.LoadAll<CouragePickupAppearanceAsset>(RoomResourcePath).ToImmutableHashSet();

    }

}