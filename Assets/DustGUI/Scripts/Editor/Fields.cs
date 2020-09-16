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
            return EditorGUILayout.Toggle(label, value, style, PackOptions(width, height));
        }

        //--------------------------------------------------------------------------------------------------------------
        // Int

        public static int Field(string label, int value)
            => Field(label, value, 0, 0, EditorStyles.numberField);

        public static int Field(string label, int value, float width, float height)
            => Field(label, value, width, height, EditorStyles.numberField);

        public static int Field(string label, int value, float width, float height, GUIStyle style)
        {
            return EditorGUILayout.IntField(label, value, style, PackOptions(width, height));
        }

        //--------------------------------------------------------------------------------------------------------------
        // Float

        public static float Field(string label, float value)
            => Field(label, value, 0, 0, EditorStyles.numberField);

        public static float Field(string label, float value, float width, float height)
            => Field(label, value, width, height, EditorStyles.numberField);

        public static float Field(string label, float value, float width, float height, GUIStyle style)
        {
            return EditorGUILayout.FloatField(label, value, style, PackOptions(width, height));
        }

        //--------------------------------------------------------------------------------------------------------------
        // String

        public static string Field(string label, string value)
            => Field(label, value, 0, 0, EditorStyles.textField);

        public static string Field(string label, string value, float width, float height)
            => Field(label, value, width, height, EditorStyles.textField);

        public static string Field(string label, string value, float width, float height, GUIStyle style)
        {
            return EditorGUILayout.TextField(label, value, style, PackOptions(width, height));
        }

        //--------------------------------------------------------------------------------------------------------------
        // Vector3

        public static Vector3 Field(string label, Vector3 value)
            => Field(label, value, 0, 0);

        public static Vector3 Field(string label, Vector3 value, float width, float height)
        {
            return EditorGUILayout.Vector3Field(label, value, PackOptions(width, height));
        }

        //--------------------------------------------------------------------------------------------------------------
        // Color

        public static Color Field(string label, Color value)
            => Field(label, value, 0, 0);

        public static Color Field(string label, Color value, float width, float height)
        {
            return EditorGUILayout.ColorField(label, value, PackOptions(width, height));
        }

        //--------------------------------------------------------------------------------------------------------------
        // AnimationCurve

        public static AnimationCurve Field(string label, AnimationCurve value)
            => Field(label, value, 0, 0);

        public static AnimationCurve Field(string label, AnimationCurve value, float width, float height)
        {
            return EditorGUILayout.CurveField(label, value, PackOptions(width, height));
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static AnimationCurve Field(string label, AnimationCurve value, float width, float height, Color color)
            => Field(label, value, width, height, color, new Rect());

        public static AnimationCurve Field(string label, AnimationCurve value, float width, float height, Color color, Rect ranges)
        {
            return EditorGUILayout.CurveField(label, value, color, ranges, PackOptions(width, height));
        }

        //--------------------------------------------------------------------------------------------------------------
        // SerializedProperty

        public static void Field(string label, SerializedProperty property)
            => Field(label, property, 0, 0);

        public static void Field(string label, SerializedProperty property, float width, float height)
        {
            EditorGUILayout.PropertyField(property, new GUIContent(label), PackOptions(width, height));
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static void Field(GUIContent label, SerializedProperty property)
            => Field(label, property, 0, 0);

        public static void Field(GUIContent label, SerializedProperty property, float width, float height)
        {
            EditorGUILayout.PropertyField(property, label, PackOptions(width, height));
        }
    }
}
