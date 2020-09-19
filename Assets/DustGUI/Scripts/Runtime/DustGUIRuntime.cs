using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
namespace DustEngine
{
    public class DustGUIRuntime : MonoBehaviour
    {
        public static void ForcedRedrawSceneView()
        {
            SceneView.lastActiveSceneView.Repaint();
        }
    }
}
#endif
