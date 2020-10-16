using Ninject.Extensions.Unity;
using System;
using UnityEngine;

namespace AChildsCourage.Game.Floors.Generation
{

    public class FloorBuilderEntity : MonoBehaviour
    {

        #region Properties

        [AutoInject] public IFloorBuilder FloorBuilder { set { BindTo(value); } }

        #endregion

        #region Methods

        private void BindTo(IFloorBuilder floorBuilder)
        {
            floorBuilder.OnFloorPlaced += (_, e) => OnFloorPlaced(e);
        }

        private void OnFloorPlaced(FloorPlacedEventArgs eventArgs)
        {
            throw new NotImplementedException();
        }

        #endregion

    }

}