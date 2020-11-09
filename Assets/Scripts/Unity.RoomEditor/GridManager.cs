using AChildsCourage.Game.Floors.Persistance;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AChildsCourage.RoomEditor
{

    public class GridManager : MonoBehaviour
    {

        #region Constants

        private const string PlaceMouseButton = "leftButton";
        private const string DeleteMouseButton = "rightButton";

        #endregion

        #region Fields

        private LayerManager selectedLayer;
        private Dictionary<string, LayerManager> layersByName = new Dictionary<string, LayerManager>();

        #endregion

        #region Properties

        public string[] LayerNames { get { return layersByName.Keys.ToArray(); } }

        public string SelectedLayerName { get { return selectedLayer.name; } }

        public string SelectedTileName { get { return selectedLayer.SelectedTileName; } }

        public string[] TileNames { get { return selectedLayer.TileNames; } }

        #endregion

        #region Methods

        public void OnMouseDown(MouseDownEventArgs eventArgs)
        {
            if (eventArgs.MouseButtonName == PlaceMouseButton)
                selectedLayer.PlaceTileAt(eventArgs.Position);
            else if (eventArgs.MouseButtonName == DeleteMouseButton)
                selectedLayer.DeleteTile(eventArgs.Position);
        }


        public void SelectLayer(string layerName)
        {
            selectedLayer = layersByName[layerName];
        }


        public void SelectTile(string tileName)
        {
            selectedLayer.SelectTile(tileName);
        }


        public void ApplyGridTo(RoomAsset roomAsset)
        {
            roomAsset.GroundPositions = GetPositionsFor("Ground", "Ground");
            roomAsset.ItemPositions = GetPositionsFor("Static", "Item");
            roomAsset.SmallCouragePositions = GetPositionsFor("Static", "Courage_Small");
            roomAsset.BigCouragePositions = GetPositionsFor("Static", "Courage_Big");
        }

        private Vector2Int[] GetPositionsFor(string layerName, string tileName)
        {
            return layersByName[layerName].GetPositionsWithTile(tileName);
        }

        public void ApplyAssetToGrid(RoomAsset roomAsset)
        {
            foreach (var layer in layersByName.Values)
                layer.Clear();

            WritePositionsTo(roomAsset.GroundPositions, "Ground", "Ground");
            WritePositionsTo(roomAsset.ItemPositions, "Static", "Item");
            WritePositionsTo(roomAsset.SmallCouragePositions, "Static", "Courage_Small");
            WritePositionsTo(roomAsset.BigCouragePositions, "Static", "Courage_Big");
        }

        private void WritePositionsTo(Vector2Int[] positions, string layerName, string tileName)
        {
            var layer = layersByName[layerName];

            layer.PlaceTiles(tileName, positions);
        }


        private void Awake()
        {
            foreach (var layer in FindObjectsOfType<LayerManager>())
                layersByName.Add(layer.name, layer);

            SelectLayer(layersByName.Keys.First());
        }

        #endregion

    }

}