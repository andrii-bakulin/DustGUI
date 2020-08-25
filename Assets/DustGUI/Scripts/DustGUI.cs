using UnityEngine;
using UnityEditor;

namespace DustEngine
{
#if UNITY_EDITOR
    public static partial class DustGUI
    {
        public static void Header(string title)
        {
            EditorGUILayout.LabelField(title, EditorStyles.boldLabel);
        }

        public static void Label(string title)
        {
            EditorGUILayout.LabelField(title);
        }

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

        public static void Space(int width = 1)
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
    }
#endif
}
