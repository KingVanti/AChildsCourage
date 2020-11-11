﻿using AChildsCourage.Game.Floors.Generation;
using AChildsCourage.Game.Floors.Persistance;
using UnityEngine;

namespace AChildsCourage.RoomEditor
{

    public class RoomEditor : MonoBehaviour
    {

        #region Fields

        [SerializeField] private GridManager gridManager;

        private RoomAsset loadedAsset;

        #endregion

        #region Properties

        public int CurrentAssetId { get { return loadedAsset.Id; } }

        public ChunkPassages CurrentPassages { get; set; }

        public bool HasLoadedAsset { get { return loadedAsset != null; } }

        #endregion

        #region Methods

        public void OnAssetSelected(RoomAsset asset)
        {
            loadedAsset = asset;

            LoadFromAsset(asset);
        }

        private void LoadFromAsset(RoomAsset asset)
        {
            CurrentPassages = asset.Passages;
            gridManager.PlaceTiles(asset.RoomTiles);
        }

        #endregion

    }

}
