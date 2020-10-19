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

        private TilePlacer selectedPlacer;
        private Dictionary<string, TilePlacer> allPlacers = new Dictionary<string, TilePlacer>();

        #endregion

        #region Properties

        public string[] PlacerNames { get { return allPlacers.Keys.ToArray(); } }

        public string SelectedPlacerName { get { return selectedPlacer != null ? selectedPlacer.name : "None"; } }

        #endregion

        #region Methods

        public void OnMouseDown(MouseDownEventArgs eventArgs)
        {
            if (eventArgs.MouseButtonName == PlaceMouseButton)
                selectedPlacer.PlaceTileAt(eventArgs.Position);
            else if (eventArgs.MouseButtonName == DeleteMouseButton)
                selectedPlacer.DeleteTile(eventArgs.Position);
        }

        public void SelectPlacer(string placerName)
        {
            selectedPlacer = allPlacers[placerName];
        }

        private void Awake()
        {
            foreach (var placer in FindObjectsOfType<TilePlacer>())
                allPlacers.Add(placer.name, placer);

            selectedPlacer = allPlacers.Values.First();
        }

        #endregion

    }

}