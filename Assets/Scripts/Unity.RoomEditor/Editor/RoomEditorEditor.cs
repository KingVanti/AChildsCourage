using AChildsCourage.Game.Floors.Persistance;
using UnityEditor;
using UnityEngine;

namespace AChildsCourage.RoomEditor.Editor
{

    [CustomEditor(typeof(RoomEditor))]
    public class RoomEditorEditor : UnityEditor.Editor
    {

        #region Properties

        private RoomEditor RoomEditor { get { return target as RoomEditor; } }

        #endregion

        #region Methods

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (Application.isPlaying)
                DrawRoomEdiorGUI();
            else
                EditorGUILayout.LabelField("Press play to start editing!");
        }

        private void DrawRoomEdiorGUI()
        {
            if (!RoomEditor.HasLoadedAsset)
                DrawLoadAssetGUI();
        }

        private void DrawLoadAssetGUI()
        {
            EditorGUILayout.LabelField("Drop in a room-asset to load it");
            var selectedAsset = (RoomAsset)EditorGUILayout.ObjectField(null, typeof(RoomAsset), false);

            EditorGUILayout.Space();

            if (selectedAsset != null)
                RoomEditor.OnAssetSelected(selectedAsset);
        }

        #endregion

    }

}