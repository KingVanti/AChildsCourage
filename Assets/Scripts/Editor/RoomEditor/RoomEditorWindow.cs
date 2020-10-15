using AChildsCourage.Game.FloorGeneration.Persistance;
using UnityEditor;
using UnityEngine;

namespace AChildsCourage.Game.FloorGeneration.Editor
{

    public class RoomEditorWindow : EditorWindow
    {

        #region Fields

        private RoomAsset selectedRoomAsset;
        private readonly RoomEditor roomEditor = new RoomEditor();

        #endregion

        #region Methods

        private void OnGUI()
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "RoomEditor")
                EditorGUILayout.LabelField("Open the RoomEditor scene to edit rooms!");
            else if (!Application.isPlaying)
                EditorGUILayout.LabelField("Press play to edit rooms!");
            else
                DrawEditor();
        }

        private void DrawEditor()
        {
            DrawSelectedRoomEditor();

            if (selectedRoomAsset != null)
                DrawRoomEditor();
            else
                EditorGUILayout.LabelField("Select a room asset to start editing!");
        }

        private void DrawSelectedRoomEditor()
        {
            selectedRoomAsset = (RoomAsset)EditorGUILayout.ObjectField("Room asset: ", selectedRoomAsset, typeof(RoomAsset), false);
        }

        private void DrawRoomEditor()
        {
            if (GUILayout.Button("Load room from asset"))
                roomEditor.LoadFromAsset(selectedRoomAsset);
            if (GUILayout.Button("Save changes"))
                roomEditor.SaveChangesToAsset(selectedRoomAsset);
        }

        #endregion

    }

}