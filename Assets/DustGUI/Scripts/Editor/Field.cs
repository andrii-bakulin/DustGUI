using UnityEngine;
using UnityEditor;

namespace DustEngine
{
    public static partial class DustGUI
    {
        //--------------------------------------------------------------------------------------------------------------
        // Bool

        public static bool Field(string label, bool value)
            => Field(label, value, 0, 0, EditorStyles.toggle);

        public static bool Field(string label, bool value, float width, float height)
            => Field(label, value, width, height, EditorStyles.toggle);

        public static bool Field(string label, bool value, float width, float height, GUIStyle style)
        {
            return EditorGUILayout.Toggle(label, value, style, NewLayoutOptions(width, height).Build());
        }

        //--------------------------------------------------------------------------------------------------------------
        // Int

        public static int Field(string label, int value)
            => Field(label, value, 0, 0, EditorStyles.numberField);

        public static int Field(string label, int value, float width, float height)
            => Field(label, value, width, height, EditorStyles.numberField);

        public static int Field(string label, int value, float width, float height, GUIStyle style)
        {
            return EditorGUILayout.IntField(label, value, style, NewLayoutOptions(width, height).Build());
        }

        //--------------------------------------------------------------------------------------------------------------
        // Float

        public static float Field(string label, float value)
            => Field(label, value, 0, 0, EditorStyles.numberField);

        public static float Field(string label, float value, float width, float height)
            => Field(label, value, width, height, EditorStyles.numberField);

        public static float Field(string label, float value, float width, float height, GUIStyle style)
        {
            return EditorGUILayout.FloatField(label, value, style, NewLayoutOptions(width, height).Build());
        }

        //--------------------------------------------------------------------------------------------------------------
        // String

        public static string Field(string label, string value)
            => Field(label, value, 0, 0, EditorStyles.textField);

        public static string Field(string label, string value, float width, float height)
            => Field(label, value, width, height, EditorStyles.textField);

        public static string Field(string label, string value, float width, float height, GUIStyle style)
        {
            return EditorGUILayout.TextField(label, value, style, NewLayoutOptions(width, height).Build());
        }

        //--------------------------------------------------------------------------------------------------------------
        // Vector3

        public static Vector3 Field(string label, Vector3 value)
            => Field(label, value, 0, 0);

        public static Vector3 Field(string label, Vector3 value, float width, float height)
        {
            return EditorGUILayout.Vector3Field(label, value, NewLayoutOptions(width, height).Build());
        }

        //--------------------------------------------------------------------------------------------------------------
        // Color

        public static Color Field(string label, Color value)
            => Field(label, value, 0, 0);

        public static Color Field(string label, Color value, float width, float height)
        {
            return EditorGUILayout.ColorField(label, value, NewLayoutOptions(width, height).Build());
        }

        //--------------------------------------------------------------------------------------------------------------
        // Color

        public static Gradient Field(string label, Gradient value)
            => Field(label, value, 0, 0);

        public static Gradient Field(string label, Gradient value, float width, float height)
        {
            return EditorGUILayout.GradientField(label, value, NewLayoutOptions(width, height).Build());
        }

        //--------------------------------------------------------------------------------------------------------------
        // AnimationCurve

        public static AnimationCurve Field(string label, AnimationCurve value)
            => Field(label, value, 0, 0);

        public static AnimationCurve Field(string label, AnimationCurve value, float width, float height)
        {
            return EditorGUILayout.CurveField(label, value, NewLayoutOptions(width, height).Build());
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static AnimationCurve Field(string label, AnimationCurve value, float width, float height, Color color)
            => Field(label, value, width, height, color, new Rect());

        public static AnimationCurve Field(string label, AnimationCurve value, float width, float height, Color color, Rect ranges)
        {
            return EditorGUILayout.CurveField(label, value, color, ranges, NewLayoutOptions(width, height).Build());
        }

        //--------------------------------------------------------------------------------------------------------------
        // SerializedProperty

        public static void Field(string label, SerializedProperty property)
            => Field(label, property, 0, 0);

        public static void Field(string label, SerializedProperty property, float width, float height)
        {
            EditorGUILayout.PropertyField(property, new GUIContent(label), NewLayoutOptions(width, height).Build());
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static void Field(GUIContent label, SerializedProperty property)
            => Field(label, property, 0, 0);

        public static void Field(GUIContent label, SerializedProperty property, float width, float height)
        {
            EditorGUILayout.PropertyField(property, label, NewLayoutOptions(width, height).Build());
        }
    }
}
