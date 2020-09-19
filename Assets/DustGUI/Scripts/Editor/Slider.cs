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
                return EditorGUILayout.Slider(value, min, max, NewLayoutOptions(width, height).Build());

            return EditorGUILayout.Slider(label, value, min, max, NewLayoutOptions(width, height).Build());
        }

        //--------------------------------------------------------------------------------------------------------------

        public static bool Slider(SerializedProperty value, float min, float max)
            => Slider((GUIContent) null, value, min, max, 0f, 0f);

        public static bool Slider(SerializedProperty value, float min, float max, float width, float height)
            => Slider((GUIContent) null, value, min, max, width, height);

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static bool Slider(string label, SerializedProperty value, float min, float max)
            => Slider(new GUIContent(label), value, min, max, 0f, 0f);

        public static bool Slider(string label, SerializedProperty value, float min, float max, float width, float height)
            => Slider(new GUIContent(label), value, min, max, width, height);

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static bool Slider(GUIContent label, SerializedProperty value, float min, float max)
            => Slider(label, value, min, max, 0f, 0f);

        public static bool Slider(GUIContent label, SerializedProperty value, float min, float max, float width, float height)
        {
            var oldValue = value.floatValue;

            if (label == null)
                EditorGUILayout.Slider(value, min, max, NewLayoutOptions(width, height).Build());
            else
                EditorGUILayout.Slider(value, min, max, label, NewLayoutOptions(width, height).Build());

            return !value.floatValue.Equals(oldValue);
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
            return GUILayout.HorizontalSlider(value, min, max, NewLayoutOptions(width, height).Build());
        }
    }
}
