﻿using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.RoomPersistance;
using UnityEngine;

namespace AChildsCourage.RoomEditor
{

    public class RoomEditor : MonoBehaviour
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private GroundTileLayer groundLayer;
        [SerializeField] private CouragePickupLayer courageLayer;

#pragma warning restore 649

        #endregion

        #region Properties

        internal RoomAsset LoadedAsset { get; private set; }

        internal TileCategory SelectedTileCategory { get; set; }

        internal CourageVariant SelectedCourageVariant { get; set; }

        internal ChunkPassages CurrentPassages { get; set; }

        internal RoomType CurrentRoomType { get; set; }


        internal int CurrentAssetId { get { return LoadedAsset.Id; } }


        internal bool HasLoadedAsset { get { return LoadedAsset != null; } }


        internal bool CurrentRoomIsStartRoom { get { return CurrentRoomType == RoomType.Start; } }


        internal bool CurrentRoomIsEndRoom { get { return CurrentRoomType == RoomType.End; } }

        #endregion

        #region Methods

        internal void OnAssetSelected(RoomAsset asset)
        {
            LoadedAsset = asset;
            CurrentPassages = asset.Passages;
            CurrentRoomType = asset.Type;

            LoadContent(asset.Content);
        }

        private void LoadContent(RoomContentData content)
        {
            groundLayer.PlaceAll(content.GroundData);
            courageLayer.PlaceAll(content.CourageData);
        }


        public void OnMouseDown(MouseDownEventArgs eventArgs)
        {
            if (HasLoadedAsset)
                ProcessEvent(eventArgs);
        }

        private void ProcessEvent(MouseDownEventArgs eventArgs)
        {
            if (eventArgs.MouseButtonName == MouseListener.LeftButtonName)
                PlaceTileAt(eventArgs.Position);
            else
                DeleteTileAt(eventArgs.Position);
        }

        private void PlaceTileAt(Vector2Int position)
        {
            switch (SelectedTileCategory)
            {
                case TileCategory.Ground:
                    groundLayer.PlaceAt(position);
                    break;
                case TileCategory.Courage:
                    courageLayer.PlaceAt(position, SelectedCourageVariant);
                    break;
            }
        }

        private void DeleteTileAt(Vector2Int position)
        {
            switch (SelectedTileCategory)
            {
                case TileCategory.Ground:
                    groundLayer.DeleteTileAt(position);
                    break;
                case TileCategory.Courage:
                    courageLayer.DeleteTileAt(position);
                    break;
            }
        }


        internal void SaveChanges()
        {
            LoadedAsset.Passages = CurrentPassages;
            LoadedAsset.Content = ReadContent();
        }

        private RoomContentData ReadContent()
        {
            return new RoomContentData(
                groundLayer.ReadAll(),
                courageLayer.ReadAll());
        }

        #endregion

    }

}
