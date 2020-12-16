using System;
using AChildsCourage.Game.Floors;
using AChildsCourage.Game.Floors.RoomPersistence;
using UnityEngine;
using static AChildsCourage.Game.Floors.MChunkPassages;

namespace AChildsCourage.RoomEditor
{

    public class RoomEditor : MonoBehaviour
    {

        #region Fields

#pragma warning disable 649

        [SerializeField] private GroundTileLayer groundLayer;
        [SerializeField] private CouragePickupLayer courageLayer;
        [SerializeField] private ItemPickupLayer itemLayer;
        [SerializeField] private StaticObjectLayer staticObjectLayer;
        [SerializeField] private RuneLayer runeLayer;

#pragma warning restore 649

        #endregion

        #region Properties

        internal RoomAsset LoadedAsset { get; private set; }

        internal TileCategory SelectedTileCategory { get; set; }

        internal CourageVariant SelectedCourageVariant { get; set; }

        internal ChunkPassages CurrentPassages { get; set; }

        internal RoomType CurrentRoomType { get; set; }


        internal int CurrentAssetId => LoadedAsset.Id;


        internal bool HasLoadedAsset => LoadedAsset != null;


        internal string CurrentAssetName => LoadedAsset.name;
        
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
            itemLayer.PlaceAll(content.ItemData);
            staticObjectLayer.PlaceAll(content.StaticObjects);
            runeLayer.PlaceAll(content.Runes);
        }


        public void OnMouseDown(MouseDownEventArgs eventArgs)
        {
            if (HasLoadedAsset) ProcessEvent(eventArgs);
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
                case TileCategory.Item:
                    itemLayer.PlaceAt(position);
                    break;
                case TileCategory.StaticObject:
                    staticObjectLayer.PlaceAt(position);
                    break;
                case TileCategory.Runes:
                    runeLayer.PlaceAt(position);
                    break;
                default: throw new Exception("Invalid tile category!");
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
                case TileCategory.Item:
                    itemLayer.DeleteTileAt(position);
                    break;
                case TileCategory.StaticObject:
                    staticObjectLayer.DeleteTileAt(position);
                    break;
                case TileCategory.Runes:
                    runeLayer.DeleteTileAt(position);
                    break;
                default: throw new Exception("Invalid tile category!");
            }
        }


        internal void SaveChanges()
        {
            LoadedAsset.Passages = CurrentPassages;
            LoadedAsset.Content = ReadContent();
        }

        private RoomContentData ReadContent() =>
            new RoomContentData(
                                groundLayer.ReadAll(),
                                courageLayer.ReadAll(),
                                itemLayer.ReadAll(),
                                staticObjectLayer.ReadAll(),
                                runeLayer.ReadAll());


        internal void Unload()
        {
            LoadedAsset = null;

            groundLayer.Clear();
            courageLayer.Clear();
            itemLayer.Clear();
            staticObjectLayer.Clear();
            runeLayer.Clear();
        }

        #endregion

    }

}