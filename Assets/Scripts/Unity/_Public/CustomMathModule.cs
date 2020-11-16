namespace AChildsCourage
{
    public static class CustomMathModule
    {

        #region Methods

        /// <summary>
        /// Re-maps a number from one range to another.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="begin1"></param>
        /// <param name="end1"></param>
        /// <param name="begin2"></param>
        /// <param name="end2"></param>
        /// <returns></returns>
        public static float Map(float x, float begin1, float end1, float begin2, float end2)
        {
            return begin2 + (end2 - begin2) * ((x - begin1) / (end1 - begin1));
        }

        #endregion

    }
}

