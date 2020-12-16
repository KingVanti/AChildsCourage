using System.Collections.Immutable;
using System.Linq;
using UnityEngine;
using static AChildsCourage.MRng;

namespace AChildsCourage.Game.Floors.Courage
{

    internal class CouragePickupRepository : ICouragePickupRepository
    {

        #region Constants

        private const string CouragePickupDataPath = "Courage-Pickup Appearances/";

        #endregion

        #region Fields

        private readonly ImmutableHashSet<CouragePickupAppearance> couragePickups;

        #endregion

        #region Constructors

        public CouragePickupRepository() => couragePickups = Resources.LoadAll<CouragePickupAppearance>(CouragePickupDataPath).ToImmutableHashSet();

        #endregion

        #region Methods

        public CouragePickupAppearance GetCouragePickupData(CourageVariant variant) => couragePickups.First(cpd => cpd.Variant == variant);

        public CouragePickupAppearance GetRandomPickupData(CreateRng createRng) => couragePickups.GetRandom(createRng);

        #endregion

    }

}