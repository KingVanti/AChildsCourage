using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using UnityEngine;

namespace AChildsCourage.Game.Floors.Courage
{

    internal static class CouragePickupAppearanceRepo
    {

        public delegate IEnumerable<CouragePickupAppearance> LoadCouragePickupAppearances();


        private const string RoomResourcePath = "Courage-Pickup Appearances/";


        [Service]
        public static LoadCouragePickupAppearances FromAssets =>
            () => LoadAssets().Select(ReadData);

        private static IEnumerable<CouragePickupAppearanceAsset> LoadAssets() =>
            Resources.LoadAll<CouragePickupAppearanceAsset>(RoomResourcePath).ToImmutableHashSet();

        private static CouragePickupAppearance ReadData(CouragePickupAppearanceAsset asset) =>
            new CouragePickupAppearance(asset.Variant,
                                        asset.LightOuterRadius,
                                        asset.LightIntensity,
                                        asset.Emission,
                                        asset.Sprite,
                                        asset.Scale);

    }

}