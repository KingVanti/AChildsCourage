using AChildsCourage.Game.NightManagement;

namespace AChildsCourage.Game.Persistance
{

    public class RunData
    {

        #region Properties

        public NightData CurrentNight { get; }

        #endregion

        #region Constructors

        public RunData()
        {
            CurrentNight = null;
        }

        public RunData(NightData currentNight)
        {
            CurrentNight = currentNight;
        }

        #endregion

    }

}