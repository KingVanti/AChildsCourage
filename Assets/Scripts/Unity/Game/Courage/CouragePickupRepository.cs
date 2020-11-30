using System;
using System.Collections.Generic;
using AChildsCourage.Game.Floors;
using UnityEngine;
using static AChildsCourage.RNG;

namespace AChildsCourage.Game.Courage
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

        public CouragePickupRepository()
        {
            couragePickups.AddRange(Resources.LoadAll<CouragePickupData>(CouragePickupDataPath));
        }

        #endregion

        #region Methods

        public CouragePickupData GetCouragePickupData(CourageVariant variant)
        {
            foreach (var cpd in couragePickups)
                if (cpd.Variant == variant)
                    return cpd;

            throw new Exception("Could not find Courage variant!");
        }

        public CouragePickupData GetRandomPickupData(CreateRNG createRng)
        {
            return couragePickups.GetRandom(createRng);
        }

        #endregion

    }

}