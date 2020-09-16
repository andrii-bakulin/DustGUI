using UnityEngine;
using UnityEditor;

namespace DustEngine
{
    public static partial class DustGUI
    {
        public static void Image(Rect rect, Texture texture)
        {
            EditorGUI.LabelField(rect, new GUIContent(texture));
        }
    }
}
