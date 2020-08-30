using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
namespace DustEngine
{
    public static partial class DustGUI
    {
        public static bool Field(string label, bool value, float width = 0f, float height = 0f)
        {
            return EditorGUILayout.Toggle(label, value, PackOptions(width, height));
        }

        public static bool Field(string label, bool value, float width, float height, GUIStyle style)
        {
            return EditorGUILayout.Toggle(label, value, style, PackOptions(width, height));
        }

        //--------------------------------------------------------------------------------------------------------------

        public static int Field(string label, int value, float width = 0f, float height = 0f)
        {
            return EditorGUILayout.IntField(label, value, PackOptions(width, height));
        }

        public static int Field(string label, int value, float width, float height, GUIStyle style)
        {
            return EditorGUILayout.IntField(label, value, style, PackOptions(width, height));
        }

        //--------------------------------------------------------------------------------------------------------------

        public static float Field(string label, float value, float width = 0f, float height = 0f)
        {
            return EditorGUILayout.FloatField(label, value, PackOptions(width, height));
        }

        public static float Field(string label, float value, float width, float height, GUIStyle style)
        {
            return EditorGUILayout.FloatField(label, value, style, PackOptions(width, height));
        }

        //--------------------------------------------------------------------------------------------------------------

        public static string Field(string label, string value, float width = 0f, float height = 0f)
        {
            return EditorGUILayout.TextField(label, value, PackOptions(width, height));
        }

        public static string Field(string label, string value, float width, float height, GUIStyle style)
        {
            return EditorGUILayout.TextField(label, value, style, PackOptions(width, height));
        }

        //--------------------------------------------------------------------------------------------------------------

        public static Vector3 Field(string label, Vector3 value, float width = 0f, float height = 0f)
        {
            return EditorGUILayout.Vector3Field(label, value, PackOptions(width, height));
        }

        public static Color Field(string label, Color value, float width = 0f, float height = 0f)
        {
            return EditorGUILayout.ColorField(label, value, PackOptions(width, height));
        }

        public static AnimationCurve Field(string label, AnimationCurve value, float width = 0f, float height = 0f)
        {
            return EditorGUILayout.CurveField(label, value, PackOptions(width, height));
        }

        public static AnimationCurve Field(string label, AnimationCurve value, float width, float height, Color color, Rect ranges = new Rect())
        {
            return EditorGUILayout.CurveField(label, value, color, ranges, PackOptions(width, height));
        }

        public static void Field(string label, SerializedProperty property, float width = 0f, float height = 0f)
        {
            EditorGUILayout.PropertyField(property, new GUIContent(label), PackOptions(width, height));
        }

        public static void Field(GUIContent label, SerializedProperty property, float width = 0f, float height = 0f)
        {
            EditorGUILayout.PropertyField(property, label, PackOptions(width, height));
        }
    }
}
#endif
