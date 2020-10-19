using System;
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

        private void Awake()
        {
            foreach (var layer in FindObjectsOfType<LayerManager>())
                layersByName.Add(layer.name, layer);

            SelectLayer(layersByName.Keys.First());
        }

        #endregion

    }

}