using Ninject.Extensions.Unity;
using UnityEngine;

namespace AChildsCourage.Game
{

    public class NightManagerEntity : MonoBehaviour
    {

        #region Properties

        [AutoInject] public INightManager NightManager { private get; set; }

        #endregion

        #region Methods

        public void PrepareNight()
        {
            NightManager.PrepareNight();
        }

        #endregion

    }

}