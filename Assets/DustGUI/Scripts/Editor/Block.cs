using UnityEngine;
using UnityEditor;

namespace DustEngine
{
    public static partial class DustGUI
    {
        public static Rect BeginHorizontal()
            => BeginHorizontal(GUIStyle.none, 0, 0);

        public static Rect BeginHorizontal(float width)
            => BeginHorizontal(GUIStyle.none, width, 0);

        public static Rect BeginHorizontal(float width, float height)
            => BeginHorizontal(GUIStyle.none, width, height);

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static Rect BeginHorizontalBox()
            => BeginHorizontal("box", 0, 0);

        public static Rect BeginHorizontalBox(float width)
            => BeginHorizontal("box", width, 0);

        public static Rect BeginHorizontalBox(float width, float height)
            => BeginHorizontal("box", width, height);

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static Rect BeginHorizontal(GUIStyle style)
            => BeginHorizontal(style, 0, 0);

        public static Rect BeginHorizontal(GUIStyle style, float width)
            => BeginHorizontal(style, width, 0);

        public static Rect BeginHorizontal(GUIStyle style, float width, float height)
        {
            return EditorGUILayout.BeginHorizontal(style, NewLayoutOptions(width, height).Build());
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static void EndHorizontal()
        {
            EditorGUILayout.EndHorizontal();
        }

        //--------------------------------------------------------------------------------------------------------------

        public static Rect BeginVertical()
            => BeginVertical(GUIStyle.none, 0, 0);

        public static Rect BeginVertical(float width)
            => BeginVertical(GUIStyle.none, width, 0);

        public static Rect BeginVertical(float width, float height)
            => BeginVertical(GUIStyle.none, width, height);

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static Rect BeginVerticalBox()
            => BeginVertical("box", 0, 0);

        public static Rect BeginVerticalBox(float width)
            => BeginVertical("box", width, 0);

        public static Rect BeginVerticalBox(float width, float height)
            => BeginVertical("box", width, height);

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static Rect BeginVertical(GUIStyle style)
            => BeginVertical(style, 0, 0);

        public static Rect BeginVertical(GUIStyle style, float width)
            => BeginVertical(style, width, 0);

        public static Rect BeginVertical(GUIStyle style, float width, float height)
        {
            return EditorGUILayout.BeginVertical(style, NewLayoutOptions(width, height).Build());
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static void EndVertical()
        {
            EditorGUILayout.EndVertical();
        }

        //--------------------------------------------------------------------------------------------------------------

        public static bool FoldoutBegin(string title, string foldoutId)
            => FoldoutBegin(title, foldoutId, null, true);

        public static bool FoldoutBegin(string title, string foldoutId, bool defaultState)
            => FoldoutBegin(title, foldoutId, null, defaultState);

        public static bool FoldoutBegin(string title, string foldoutId, Object targetId)
            => FoldoutBegin(title, foldoutId, targetId, true);

        public static bool FoldoutBegin(string title, string foldoutId, Object targetId, bool defaultState)
        {
#if UNITY_2019_1_OR_NEWER
            string key = "DustEngine.DustGUI.Foldout." + foldoutId;

            if (targetId != null)
                key += "." + targetId.GetInstanceID();

            bool state = SessionState.GetBool(key, defaultState);
            state = EditorGUILayout.BeginFoldoutHeaderGroup(state, title);
            SessionState.SetBool(key, state);

            IndentLevelInc();
            return state;
#else
            return FoldoutBegin(title);
#endif
        }

        public static bool FoldoutBegin(string title)
        {
#if UNITY_2019_1_OR_NEWER
            EditorGUILayout.BeginFoldoutHeaderGroup(true, title);
#else
            Header(title);
#endif

            IndentLevelInc();
            return true;
        }

        public static void FoldoutEnd()
        {
            IndentLevelDec();

#if UNITY_2019_1_OR_NEWER
            EditorGUILayout.EndFoldoutHeaderGroup();
#else
            SpaceLine();
#endif
        }

        //--------------------------------------------------------------------------------------------------------------

        public static Vector2 BeginScrollView(Vector2 scrollPosition)
            => BeginScrollView(scrollPosition, GUIStyle.none, 0, 0);

        public static Vector2 BeginScrollView(Vector2 scrollPosition, float width)
            => BeginScrollView(scrollPosition, GUIStyle.none, width, 0);

        public static Vector2 BeginScrollView(Vector2 scrollPosition, float width, float height)
            => BeginScrollView(scrollPosition, GUIStyle.none, width, height);

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static Vector2 BeginScrollView(Vector2 scrollPosition, GUIStyle style)
            => BeginScrollView(scrollPosition, style, 0, 0);

        public static Vector2 BeginScrollView(Vector2 scrollPosition, GUIStyle style, float width)
            => BeginScrollView(scrollPosition, style, width, 0);

        public static Vector2 BeginScrollView(Vector2 scrollPosition, GUIStyle style, float width, float height)
        {
            return EditorGUILayout.BeginScrollView(scrollPosition, style, NewLayoutOptions(width, height).Build());
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static bool BeginScrollView(ref Vector2 scrollPosition)
            => BeginScrollView(ref scrollPosition, GUIStyle.none, 0, 0);

        public static bool BeginScrollView(ref Vector2 scrollPosition, float width)
            => BeginScrollView(ref scrollPosition, GUIStyle.none, width, 0);

        public static bool BeginScrollView(ref Vector2 scrollPosition, float width, float height)
            => BeginScrollView(ref scrollPosition, GUIStyle.none, width, height);

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static bool BeginScrollView(ref Vector2 scrollPosition, GUIStyle style)
            => BeginScrollView(ref scrollPosition, style, 0, 0);

        public static bool BeginScrollView(ref Vector2 scrollPosition, GUIStyle style, float width)
            => BeginScrollView(ref scrollPosition, style, width, 0);

        public static bool BeginScrollView(ref Vector2 scrollPosition, GUIStyle style, float width, float height)
        {
            var lastPosition = scrollPosition;
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, style, NewLayoutOptions(width, height).Build());
            return !lastPosition.Equals(scrollPosition);
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static void EndScrollView()
        {
            EditorGUILayout.EndScrollView();
        }
    }
}
