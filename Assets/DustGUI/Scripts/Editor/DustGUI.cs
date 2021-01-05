using UnityEngine;
using UnityEditor;

namespace DustEngine
{
    public static partial class DustGUI
    {
        //--------------------------------------------------------------------------------------------------------------
        // Header

        public static void Header(string title)
            => Header(title, 0, 0);

        public static void Header(string title, float width)
            => Header(title, width, 0);

        public static void Header(string title, float width, float height)
        {
            EditorGUILayout.LabelField(title, EditorStyles.boldLabel, NewLayoutOptions(width, height).Build());
        }

        //--------------------------------------------------------------------------------------------------------------
        // Label

        public static Color labelNormalColor => GUI.skin.label.normal.textColor;

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static void Label(string title)
            => Label(title, 0, 0);

        public static void Label(string title, Color color)
            => Label(title, 0, 0, color);

        public static void Label(string title, GUIStyle style)
            => Label(title, 0, 0, style);

        public static void Label(string title, float width)
            => Label(title, width, 0);

        public static void Label(string title, float width, float height)
            => Label(title, width, height, labelNormalColor);

        public static void Label(string title, float width, float height, Color color)
        {
            Label(title, width, height, NewStyleLabel().NormalTextColor(color).Build());
        }

        public static void Label(string title, float width, float height, GUIStyle style)
        {
            EditorGUILayout.LabelField(title, style, NewLayoutOptions(width, height).Build());
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static void SimpleLabel(string title)
            => SimpleLabel(title, 0, 0, labelNormalColor);

        public static void SimpleLabel(string title, Color color)
            => SimpleLabel(title, 0, 0, color);

        public static void SimpleLabel(string title, GUIStyle style)
            => SimpleLabel(title, 0, 0, style);

        public static void SimpleLabel(string title, float width)
            => SimpleLabel(title, width, 0, labelNormalColor);

        public static void SimpleLabel(string title, float width, float height)
            => SimpleLabel(title, width, height, labelNormalColor);

        public static void SimpleLabel(string title, float width, float height, Color color)
            => SimpleLabel(title, width, height, NewStyleLabel().NormalTextColor(color).Build());

        public static void SimpleLabel(string title, float width, float height, GUIStyle style)
        {
            GUILayout.Label(title, style, NewLayoutOptions(width, height).Build());
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static void SimpleLabel(GUIContent title)
            => SimpleLabel(title, 0, 0, labelNormalColor);

        public static void SimpleLabel(GUIContent title, Color color)
            => SimpleLabel(title, 0, 0, color);

        public static void SimpleLabel(GUIContent title, GUIStyle style)
            => SimpleLabel(title, 0, 0, style);

        public static void SimpleLabel(GUIContent title, float width)
            => SimpleLabel(title, width, 0, labelNormalColor);

        public static void SimpleLabel(GUIContent title, float width, float height)
            => SimpleLabel(title, width, height, labelNormalColor);

        public static void SimpleLabel(GUIContent title, float width, float height, Color color)
            => SimpleLabel(title, width, height, NewStyleLabel().NormalTextColor(color).Build());

        public static void SimpleLabel(GUIContent title, float width, float height, GUIStyle style)
        {
            GUILayout.Label(title, style, NewLayoutOptions(width, height).Build());
        }

        //--------------------------------------------------------------------------------------------------------------

        public static void StaticTextField(string label, string message)
        {
            Lock();
            EditorGUILayout.TextField(label, message);
            Unlock();
        }

        public static void HelpBoxInfo(string message)
            => EditorGUILayout.HelpBox(message, MessageType.Info);

        public static void HelpBoxWarning(string message)
            => EditorGUILayout.HelpBox(message, MessageType.Warning);

        public static void HelpBoxError(string message)
            => EditorGUILayout.HelpBox(message, MessageType.Error);

        //--------------------------------------------------------------------------------------------------------------

        public static void Space()
            => Space(6f);

        public static void Space(float width)
        {
#if UNITY_2019_1_OR_NEWER
            EditorGUILayout.Space(width);
#else
            EditorGUILayout.Space();
#endif
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static void SpaceLine()
            => SpaceLine(1f);

        public static void SpaceLine(float width)
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

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static void SpaceExpand()
            => SpaceExpand(6f);

        public static void SpaceExpand(float width)
        {
#if UNITY_2019_1_OR_NEWER
            EditorGUILayout.Space(width, true);
#else
            EditorGUILayout.Space();
#endif
        }

        //--------------------------------------------------------------------------------------------------------------

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

        public static int IndentLevelReset()
            => IndentLevelReset(0);

        public static int IndentLevelReset(int newValue)
        {
            int prevValue = indentLevel;
            indentLevel = newValue;
            return prevValue;
        }

        //--------------------------------------------------------------------------------------------------------------

        public static void BlurFocusControl()
        {
            GUI.FocusControl("");
        }

        //--------------------------------------------------------------------------------------------------------------

        public static void ForcedRedrawSceneView()
        {
            SceneView.lastActiveSceneView.Repaint();
        }

        public static void ForcedRedrawInspector(Editor editor)
        {
            editor.Repaint();
        }

        public static bool IsUndoRedoPerformed()
        {
            return Event.current.type == EventType.ValidateCommand && Event.current.commandName == "UndoRedoPerformed";
        }

        //--------------------------------------------------------------------------------------------------------------
    }
}
