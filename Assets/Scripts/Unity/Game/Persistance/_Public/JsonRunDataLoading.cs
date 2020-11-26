namespace AChildsCourage.Game.Persistance
{

    internal static class JsonRunDataLoading
    {

        internal static RunDataLoader Make()
        {
            return LoadCurrent;
        }

        private static RunData LoadCurrent()
        {
            // TODO: Load current run data

            var nightData = new NightData(123);

            return new RunData(nightData);
        }

    }

}