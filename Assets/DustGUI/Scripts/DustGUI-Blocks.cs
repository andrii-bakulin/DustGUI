using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DustEngine
{
#if UNITY_EDITOR
    public static partial class DustGUI
    {
        public static Rect BeginHorizontal(float width = 0, float height = 0)
        {
            return BeginHorizontal(GUIStyle.none, width, height);
        }

        public static Rect BeginHorizontalBox(float width = 0, float height = 0)
        {
            return BeginHorizontal("box", width, height);
        }

        public static Rect BeginHorizontal(GUIStyle style, float width = 0, float height = 0)
        {
            var options = new List<GUILayoutOption>();

            if (width > 0)
                options.Add(GUILayout.Width(width));

            if (height > 0)
                options.Add(GUILayout.Height(height));

            return EditorGUILayout.BeginHorizontal(style, options.ToArray());
        }

        public static void EndHorizontal()
        {
            EditorGUILayout.EndHorizontal();
        }

        //--------------------------------------------------------------------------------------------------------------

        public static Rect BeginVertical(float width = 0, float height = 0)
        {
            return BeginVertical(GUIStyle.none, width, height);
        }

        public static Rect BeginVerticalBox(float width = 0, float height = 0)
        {
            return BeginVertical("box", width, height);
        }

        public static Rect BeginVertical(GUIStyle style, float width = 0, float height = 0)
        {
            var options = new List<GUILayoutOption>();

            if (width > 0)
                options.Add(GUILayout.Width(width));

            if (height > 0)
                options.Add(GUILayout.Height(height));

            return EditorGUILayout.BeginVertical(style, options.ToArray());
        }

        public static void EndVertical()
        {
            EditorGUILayout.EndVertical();
        }
    }
#endif
}
