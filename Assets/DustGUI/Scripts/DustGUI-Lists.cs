using System;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
namespace DustEngine
{
    public static partial class DustGUI
    {
        public static int DropDownList(int selectedIndex, string[] displayedOptions, float width = 0, float height = 0, GUIStyle style = null)
        {
            return DropDownList(null, selectedIndex, displayedOptions, width, height, style);
        }

        public static int DropDownList(string label, int selectedIndex, string[] displayedOptions, float width = 0, float height = 0, GUIStyle style = null)
        {
            if (label == null)
                return EditorGUILayout.Popup(selectedIndex, displayedOptions, style, PackOptions(width, height));

            return EditorGUILayout.Popup(label, selectedIndex, displayedOptions, style, PackOptions(width, height));
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static Enum DropDownList(Enum selected, float width = 0, float height = 0, GUIStyle style = null)
        {
            return EditorGUILayout.EnumPopup(selected, style, PackOptions(width, height));
        }
    }
}
#endif
