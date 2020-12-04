using static AChildsCourage.Game.Persistence.MRunData;

namespace AChildsCourage.Game.Persistence
{

    internal static class JsonRunDataLoading
    {

        internal static LoadRunData Make() => LoadCurrent;

        private static RunData LoadCurrent() =>

            // TODO: Load current run data
            NewRun;

    }

}