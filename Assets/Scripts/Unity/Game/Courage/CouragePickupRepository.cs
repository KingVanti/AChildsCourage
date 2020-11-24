using AChildsCourage.Game.Floors;
using System.Collections.Generic;
using UnityEngine;
using static AChildsCourage.RNG;

namespace AChildsCourage.Game.Courage
{
    internal class CouragePickupRepository : ICouragePickupRepository {

        #region Constants

        private const string CouragePickupDataPath = "Courage/";

        #endregion

        #region Fields

        private List<CouragePickupData> couragePickups = new List<CouragePickupData>();

        #endregion

        #region Constructors

        public CouragePickupRepository() {

            couragePickups.AddRange(Resources.LoadAll<CouragePickupData>(CouragePickupDataPath));

        }

        #endregion

        #region Methods

        public CouragePickupData GetCouragePickupData(CourageVariant variant) {

            foreach (CouragePickupData cpd in couragePickups) {
                if (cpd.Variant == variant) {
                    return cpd;
                }
            }

            throw new System.Exception("Could not find Courage variant!");

        }

        public CouragePickupData GetRandomPickupData(RNGSource rng) {
            return couragePickups.GetRandom(rng);
        }

        #endregion


    }

}
