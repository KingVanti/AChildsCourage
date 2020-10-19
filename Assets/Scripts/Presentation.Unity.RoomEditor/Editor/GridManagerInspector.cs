using UnityEditor;
using UnityEngine;

namespace AChildsCourage.RoomEditor.Editor
{

    [CustomEditor(typeof(GridManager))]
    public class GridManagerInspector : UnityEditor.Editor
    {

        #region Methods

        public override void OnInspectorGUI()
        {
            if (Application.isPlaying)
            {
                var gridManager = (GridManager)target;

                EditorGUILayout.LabelField("Layer", EditorStyles.boldLabel);
                EditorGUILayout.LabelField($"Selected: {gridManager.SelectedPlacerName}");

                foreach (var placerName in gridManager.PlacerNames)
                    if (GUILayout.Button(placerName))
                        gridManager.SelectPlacer(placerName);
            }
            else
                EditorGUILayout.LabelField("Press play to start editing", EditorStyles.helpBox);
        }

        #endregion

    }

}