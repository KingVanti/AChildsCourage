using static AChildsCourage.Game.Persistance.MRunData;

namespace AChildsCourage.Game.Persistance
{

    internal static class JsonRunDataLoading
    {

        internal static LoadRunData Make()
        {
            return LoadCurrent;
        }

        private static RunData LoadCurrent()
        {
            // TODO: Load current run data

            return NewRun;
        }

    }

}