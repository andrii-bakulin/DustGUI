using UnityEngine;
using UnityEditor;

namespace DustEngine
{
    public static partial class DustGUI
    {
        public static int Toolbar(int selectedTab, string[] titles)
            => Toolbar(selectedTab, titles, GUI.skin.button, GUI.ToolbarButtonSize.Fixed);

        public static int Toolbar(int selectedTab, string[] titles, GUIStyle style, GUI.ToolbarButtonSize buttonSize, params GUILayoutOption[] options)
        {
            return GUILayout.Toolbar(selectedTab, titles);
        }

        //--------------------------------------------------------------------------------------------------------------

        public static int Toolbar(string toolbarId, string[] titles)
            => Toolbar(toolbarId, null, titles, GUI.skin.button, GUI.ToolbarButtonSize.Fixed);

        public static int Toolbar(string toolbarId, Object targetId, string[] titles)
            => Toolbar(toolbarId, targetId, titles, GUI.skin.button, GUI.ToolbarButtonSize.Fixed);

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static int Toolbar(string toolbarId, string[] titles, GUIStyle style, GUI.ToolbarButtonSize buttonSize, params GUILayoutOption[] options)
            => Toolbar(toolbarId, null, titles, style, buttonSize, options);

        public static int Toolbar(string toolbarId, Object targetId, string[] titles, GUIStyle style, GUI.ToolbarButtonSize buttonSize, params GUILayoutOption[] options)
        {
            string key = "DustEngine.DustGUI.Toolbar." + toolbarId;

            if (targetId != null)
                key += "." + targetId.GetInstanceID();

            int selectedTab = SessionState.GetInt(key, 0);
            selectedTab = GUILayout.Toolbar(selectedTab, titles);
            SessionState.SetInt(key, selectedTab);

            return selectedTab;
        }
    }
}
