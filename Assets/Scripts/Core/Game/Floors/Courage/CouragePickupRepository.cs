using System.Collections.Generic;
using System.Linq;
using AChildsCourage.Game.Floors;
using UnityEngine;
using static AChildsCourage.MRng;

namespace AChildsCourage.Game.Floors.Courage
{

    internal class CouragePickupRepository : ICouragePickupRepository
    {

        #region Constants

        private const string CouragePickupDataPath = "Courage/";

        #endregion

        #region Fields

        private readonly List<CouragePickupData> couragePickups = new List<CouragePickupData>();

        #endregion

        #region Constructors

        public CouragePickupRepository() => couragePickups.AddRange(Resources.LoadAll<CouragePickupData>(CouragePickupDataPath));

        #endregion

        #region Methods

        public CouragePickupData GetCouragePickupData(CourageVariant variant) => couragePickups.First(cpd => cpd.Variant == variant);

        public CouragePickupData GetRandomPickupData(CreateRng createRng) => couragePickups.GetRandom(createRng);

        #endregion

    }

}