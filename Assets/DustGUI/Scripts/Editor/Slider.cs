using UnityEngine;
using UnityEditor;

namespace DustEngine
{
    public static partial class DustGUI
    {
        public static float Slider(float value, float min, float max)
            => Slider((GUIContent) null, value, min, max, 0, 0);

        public static float Slider(float value, float min, float max, float width, float height)
            => Slider((GUIContent) null, value, min, max, width, height);

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static float Slider(string label, float value, float min, float max)
            => Slider(new GUIContent(label), value, min, max, 0, 0);

        public static float Slider(string label, float value, float min, float max, float width, float height)
            => Slider(new GUIContent(label), value, min, max, width, height);

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static float Slider(GUIContent label, float value, float min, float max)
            => Slider(label, value, min, max, 0, 0);

        public static float Slider(GUIContent label, float value, float min, float max, float width, float height)
        {
            if( label == null)
                return EditorGUILayout.Slider(value, min, max, PackOptions(width, height));

            return EditorGUILayout.Slider(label, value, min, max, PackOptions(width, height));
        }

        //--------------------------------------------------------------------------------------------------------------

        public static void Slider(SerializedProperty value, float min, float max)
            => Slider((GUIContent) null, value, min, max, 0f, 0f);

        public static void Slider(SerializedProperty value, float min, float max, float width, float height)
            => Slider((GUIContent) null, value, min, max, width, height);

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static void Slider(string label, SerializedProperty value, float min, float max)
            => Slider(new GUIContent(label), value, min, max, 0f, 0f);

        public static void Slider(string label, SerializedProperty value, float min, float max, float width, float height)
            => Slider(new GUIContent(label), value, min, max, width, height);

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static void Slider(GUIContent label, SerializedProperty value, float min, float max)
            => Slider(label, value, min, max, 0f, 0f);

        public static void Slider(GUIContent label, SerializedProperty value, float min, float max, float width, float height)
        {
            if( label == null)
                EditorGUILayout.Slider(value, min, max, PackOptions(width, height));

            EditorGUILayout.Slider(value, min, max, label, PackOptions(width, height));
        }

        //--------------------------------------------------------------------------------------------------------------

        public static float SliderOnly01(float value)
            => SliderOnly(value, 0f, 1f, 0f, 0f);

        public static float SliderOnly01(float value, float width, float height)
            => SliderOnly(value, 0f, 1f, width, height);

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static float SliderOnly(float value, float min, float max)
            => SliderOnly(value, min, max, 0f, 0f);

        public static float SliderOnly(float value, float min, float max, float width, float height)
        {
            return GUILayout.HorizontalSlider(value, min, max, PackOptions(width, height));
        }
    }
}
