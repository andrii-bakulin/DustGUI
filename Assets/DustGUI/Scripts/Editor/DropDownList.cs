using System;
using UnityEngine;
using UnityEditor;

namespace DustEngine
{
    public static partial class DustGUI
    {
        public static int DropDownList(int selectedIndex, string[] displayedOptions)
            => DropDownList(null, selectedIndex, displayedOptions, 0, 0, null);

        public static int DropDownList(int selectedIndex, string[] displayedOptions, GUIStyle style)
            => DropDownList(null, selectedIndex, displayedOptions, 0, 0, style);

        public static int DropDownList(int selectedIndex, string[] displayedOptions, float width, float height)
            => DropDownList(null, selectedIndex, displayedOptions, width, height, null);

        public static int DropDownList(int selectedIndex, string[] displayedOptions, float width, float height, GUIStyle style)
            => DropDownList(null, selectedIndex, displayedOptions, width, height, style);

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static int DropDownList(string label, int selectedIndex, string[] displayedOptions)
            => DropDownList(label, selectedIndex, displayedOptions, 0, 0, null);

        public static int DropDownList(string label, int selectedIndex, string[] displayedOptions, GUIStyle style)
            => DropDownList(label, selectedIndex, displayedOptions, 0, 0, style);

        public static int DropDownList(string label, int selectedIndex, string[] displayedOptions, float width, float height)
            => DropDownList(label, selectedIndex, displayedOptions, width, height, null);

        public static int DropDownList(string label, int selectedIndex, string[] displayedOptions, float width, float height, GUIStyle style)
        {
            if (label == null)
                return EditorGUILayout.Popup(selectedIndex, displayedOptions, style, PackOptions(width, height));

            return EditorGUILayout.Popup(label, selectedIndex, displayedOptions, style, PackOptions(width, height));
        }

        //--------------------------------------------------------------------------------------------------------------

        public static Enum DropDownList(Enum selected)
            => DropDownList(selected, 0, 0, EditorStyles.popup);

        public static Enum DropDownList(Enum selected, GUIStyle style)
            => DropDownList(selected, 0, 0, style);

        public static Enum DropDownList(Enum selected, float width, float height)
            => DropDownList(selected, width, height, EditorStyles.popup);

        public static Enum DropDownList(Enum selected, float width, float height, GUIStyle style)
        {
            return EditorGUILayout.EnumPopup(selected, style, PackOptions(width, height));
        }
    }
}
