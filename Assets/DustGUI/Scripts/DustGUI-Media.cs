using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
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
#endif
