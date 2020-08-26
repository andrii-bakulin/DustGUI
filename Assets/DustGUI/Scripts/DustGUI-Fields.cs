using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
namespace DustEngine
{
    public static partial class DustGUI
    {
        public static bool Field(string label, bool value)
        {
            return EditorGUILayout.Toggle(label, value);
        }

        public static int Field(string label, int value)
        {
            return EditorGUILayout.IntField(label, value);
        }

        public static float Field(string label, float value)
        {
            return EditorGUILayout.FloatField(label, value);
        }

        public static string Field(string label, string value)
        {
            return EditorGUILayout.TextField(label, value);
        }

        public static Vector3 Field(string label, Vector3 value)
        {
            return EditorGUILayout.Vector3Field(label, value);
        }

        public static Color Field(string label, Color value)
        {
            return EditorGUILayout.ColorField(label, value);
        }

        public static AnimationCurve Field(string label, AnimationCurve value)
        {
            return EditorGUILayout.CurveField(label, value);
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
