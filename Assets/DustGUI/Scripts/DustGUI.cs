using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DustEngine
{
    public static partial class DustGUI
    {
#if UNITY_EDITOR
        public static void Header(string title)
        {
            EditorGUILayout.LabelField(title, EditorStyles.boldLabel);
        }

        //--------------------------------------------------------------------------------------------------------------

        public static Color labelNormalColor => GUI.skin.label.normal.textColor;

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static void Label(string title)
        {
            Label(title, 0, 0, labelNormalColor);
        }

        public static void Label(string title, float width, float height)
        {
            Label(title, width, height, labelNormalColor);
        }

        public static void Label(string title, float width, float height, Color color)
        {
            var style = new GUIStyle(GUI.skin.label);
            style.normal.textColor = color;

            GUILayout.Label(title, style, PackOptions(width, height));
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static void PrefixLabel(string title)
        {
            PrefixLabel(title, 0, 0, labelNormalColor);
        }

        public static void PrefixLabel(string title, float width, float height)
        {
            PrefixLabel(title, width, height, labelNormalColor);
        }

        public static void PrefixLabel(string title, float width, float height, Color color)
        {
            var style = new GUIStyle(GUI.skin.label);
            style.normal.textColor = color;

            EditorGUILayout.LabelField(title, style, PackOptions(width, height));
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

        public static bool FoldoutBegin(string title, string foldoutId, bool defaultState = true)
        {
            string key = "DustEngine.DustGUI.Foldout." + foldoutId;

            bool state = SessionState.GetBool(key, defaultState);
            state = EditorGUILayout.BeginFoldoutHeaderGroup(state, title);
            SessionState.SetBool(key, state);

            IndentLevelInc();
            return state;
        }

        public static void FoldoutBegin(string title)
        {
            EditorGUILayout.BeginFoldoutHeaderGroup(true, title);
            IndentLevelInc();
        }

        public static void FoldoutEnd()
        {
            IndentLevelDec();
            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        //--------------------------------------------------------------------------------------------------------------

        public static void Space(float width = 1)
        {
            EditorGUILayout.Space(width * EditorGUIUtility.singleLineHeight / 2f);
        }

        public static void Lock()
        {
            GUI.enabled = false;
        }

        public static void Unlock()
        {
            GUI.enabled = true;
        }

        public static void IndentLevelInc()
        {
            EditorGUI.indentLevel++;
        }

        public static void IndentLevelDec()
        {
            EditorGUI.indentLevel--;
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
