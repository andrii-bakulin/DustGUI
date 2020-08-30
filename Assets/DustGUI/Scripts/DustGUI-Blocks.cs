using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
namespace DustEngine
{
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
            return EditorGUILayout.BeginHorizontal(style, PackOptions(width, height));
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
            return EditorGUILayout.BeginVertical(style, PackOptions(width, height));
        }

        public static void EndVertical()
        {
            EditorGUILayout.EndVertical();
        }

        //--------------------------------------------------------------------------------------------------------------

#if UNITY_2019_1_OR_NEWER
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
#endif

        //--------------------------------------------------------------------------------------------------------------

        public static Vector2 BeginScrollView(Vector2 scrollPosition, float width = 0, float height = 0)
        {
            return BeginScrollView(scrollPosition, GUIStyle.none, width, height);
        }

        public static Vector2 BeginScrollView(Vector2 scrollPosition, GUIStyle style, float width = 0, float height = 0)
        {
            return EditorGUILayout.BeginScrollView(scrollPosition, style, PackOptions(width, height));
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static bool BeginScrollView(ref Vector2 scrollPosition, float width = 0, float height = 0)
        {
            return BeginScrollView(ref scrollPosition, GUIStyle.none, width, height);
        }

        public static bool BeginScrollView(ref Vector2 scrollPosition, GUIStyle style, float width = 0, float height = 0)
        {
            var lastPosition = scrollPosition;
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, style, PackOptions(width, height));
            return !lastPosition.Equals(scrollPosition);
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static void EndScrollView()
        {
            EditorGUILayout.EndScrollView();
        }
    }
}
#endif
