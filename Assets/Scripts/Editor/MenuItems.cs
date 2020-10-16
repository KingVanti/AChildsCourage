using AChildsCourage.Game.Floors.Editor;
using UnityEditor;

namespace AChildsCourage.Editor
{

    public static class MenuItems
    {

        #region Methods

        [MenuItem("Window/A Child's Courage/Room-editor")]
        public static void OpenRoomEditor()
        {
            EditorWindow.GetWindow<RoomEditorView>().Show();
        }

        #endregion

    }

}