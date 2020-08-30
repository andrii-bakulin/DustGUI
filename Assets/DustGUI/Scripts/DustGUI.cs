using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DustEngine
{
    public static partial class DustGUI
    {
#if UNITY_EDITOR
        public static void Header(string title, float width = 0, float height = 0)
        {
            EditorGUILayout.LabelField(title, EditorStyles.boldLabel, PackOptions(width, height));
        }

        //--------------------------------------------------------------------------------------------------------------

        public static Color labelNormalColor => GUI.skin.label.normal.textColor;

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static void Label(string title, GUIStyle style)
        {
            Label(title, 0, 0, style);
        }

        public static void Label(string title, float width = 0f, float height = 0f)
        {
            Label(title, width, height, labelNormalColor);
        }

        public static void Label(string title, float width, float height, Color color)
        {
            var style = new GUIStyle(GUI.skin.label);
            style.normal.textColor = color;

            Label(title, width, height, style);
        }

        public static void Label(string title, float width, float height, GUIStyle style)
        {
            EditorGUILayout.LabelField(title, style, PackOptions(width, height));
        }
        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static void SimpleLabel(string title)
        {
            SimpleLabel(title, 0, 0, labelNormalColor);
        }

        public static void SimpleLabel(string title, GUIStyle style)
        {
            SimpleLabel(title, 0, 0, style);
        }

        public static void SimpleLabel(string title, float width, float height)
        {
            SimpleLabel(title, width, height, labelNormalColor);
        }

        public static void SimpleLabel(string title, float width, float height, Color color)
        {
            var style = new GUIStyle(GUI.skin.label);
            style.normal.textColor = color;

            SimpleLabel(title, width, height, style);
        }

        public static void SimpleLabel(string title, float width, float height, GUIStyle style)
        {
            GUILayout.Label(title, style, PackOptions(width, height));
        }

        //--------------------------------------------------------------------------------------------------------------

        public static void StaticTextField(string label, string message)
        {
            Lock();
            EditorGUILayout.TextField(label, message);
            Unlock();
        }

        public static void HelpBoxInfo(string message)
        {
            EditorGUILayout.HelpBox(message, MessageType.Info);
        }

        public static void HelpBoxWarning(string message)
        {
            EditorGUILayout.HelpBox(message, MessageType.Warning);
        }

        public static void HelpBoxError(string message)
        {
            EditorGUILayout.HelpBox(message, MessageType.Error);
        }

        //--------------------------------------------------------------------------------------------------------------

        public static void Space(float width = 6f)
        {
#if UNITY_2019_1_OR_NEWER
            EditorGUILayout.Space(width);
#else
            EditorGUILayout.Space();
#endif
        }

        public static void SpaceLine(float width = 1f)
        {
#if UNITY_2019_1_OR_NEWER
            EditorGUILayout.Space(width * EditorGUIUtility.singleLineHeight);
#else
            for (int i = 0; i < width; i++)
            {
                EditorGUILayout.Space();
            }
#endif
        }

        public static void SpaceExpand(float width = 6f)
        {
            EditorGUILayout.Space(width, true);
        }

        public static void Lock()
        {
            GUI.enabled = false;
        }

        public static void Unlock()
        {
            GUI.enabled = true;
        }

        //--------------------------------------------------------------------------------------------------------------

        public static int indentLevel
        {
            get => EditorGUI.indentLevel;
            set => EditorGUI.indentLevel = value;
        }

        public static void IndentLevelInc()
        {
            indentLevel++;
        }

        public static void IndentLevelDec()
        {
            indentLevel--;
        }

        public static int IndentLevelReset(int newValue = 0)
        {
            int prevValue = indentLevel;
            indentLevel = newValue;
            return prevValue;
        }

        //--------------------------------------------------------------------------------------------------------------

        public static void ForcedRedrawSceneView()
        {
            SceneView.lastActiveSceneView.Repaint();
        }

        public static bool IsUndoRedoPerformed()
        {
            return Event.current.type == EventType.ValidateCommand && Event.current.commandName == "UndoRedoPerformed";
        }

        //--------------------------------------------------------------------------------------------------------------

        public static GUILayoutOption[] PackOptions(float width, float height)
        {
            var options = new List<GUILayoutOption>();

            if (width > 0f)
                options.Add(GUILayout.Width(width));

            if (height > 0f)
                options.Add(GUILayout.Height(height));

            return options.ToArray();
        }
#endif
    }
}
