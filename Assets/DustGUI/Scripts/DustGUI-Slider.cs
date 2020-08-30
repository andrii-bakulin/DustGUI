using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
namespace DustEngine
{
    public static partial class DustGUI
    {
        public static float Slider(float value, float min, float max, float width = 0, float height = 0)
        {
            return Slider((GUIContent) null, value, min, max, width, height);
        }

        public static float Slider(string label, float value, float min, float max, float width = 0, float height = 0)
        {
            return Slider(new GUIContent(label), value, min, max, width, height);
        }

        public static float Slider(GUIContent label, float value, float min, float max, float width = 0, float height = 0)
        {
            if( label != null)
                return EditorGUILayout.Slider(label, value, min, max, PackOptions(width, height));
            else
                return EditorGUILayout.Slider(value, min, max, PackOptions(width, height));
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static void Slider(SerializedProperty value, float min, float max, float width = 0, float height = 0)
        {
            Slider((GUIContent) null, value, min, max, width, height);
        }

        public static void Slider(string label, SerializedProperty value, float min, float max, float width = 0, float height = 0)
        {
            Slider(new GUIContent(label), value, min, max, width, height);
        }

        public static void Slider(GUIContent label, SerializedProperty value, float min, float max, float width = 0, float height = 0)
        {
            if( label != null)
                EditorGUILayout.Slider(value, min, max, label, PackOptions(width, height));
            else
                EditorGUILayout.Slider(value, min, max, PackOptions(width, height));
        }

        //--------------------------------------------------------------------------------------------------------------

        public static float SliderOnly01(float value, float width = 0, float height = 0)
        {
            return SliderOnly(value, 0f, 1f, width, height);
        }

        public static float SliderOnly(float value, float min, float max, float width = 0, float height = 0)
        {
            return GUILayout.HorizontalSlider(value, min, max, PackOptions(width, height));
        }
    }
}
#endif
