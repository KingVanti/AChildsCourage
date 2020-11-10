using AChildsCourage.Game.Floors.Persistance;
using ICSharpCode.NRefactory.Visitors;
using System;
using UnityEngine;

namespace AChildsCourage.RoomEditor
{

    public class RoomEditor : MonoBehaviour
    {

        #region Fields

        private RoomAsset loadedAsset;

        #endregion

        #region Properties

        public bool HasLoadedAsset { get { return loadedAsset != null; } }

        #endregion

        #region Methods

        public void OnAssetSelected(RoomAsset asset)
        {
            loadedAsset = asset;

            LoadAsset(asset);
        }

        private void LoadAsset(RoomAsset asset)
        {
            throw new NotImplementedException();
        }

        #endregion

    }

}
