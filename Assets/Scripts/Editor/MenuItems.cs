using AChildsCourage.Game.Floors.Editor;
using UnityEditor;

namespace AChildsCourage.Editor
{

    public static class MenuItems
    {

        #region Methods

        [MenuItem("A Child's Courage/Room-editor")]
        public static void OpenRoomEditor()
        {
            EditorWindow.GetWindow<RoomEditorWindow>().Show();
        }

        #endregion

    }

}