using AChildsCourage.Game.NightManagement;

namespace AChildsCourage.Game.Persistance
{

    public class JsonRunStorage : IRunStorage
    {

        #region Methods

        public RunData LoadCurrent()
        {
            // TODO: Load current run data

            var nightData = new NightData(123);

            return new RunData(nightData);
        }

        #endregion

    }

}