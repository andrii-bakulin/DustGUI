using DustEngine;
using UnityEditor;
using UnityEngine;

namespace DustDemo
{
    public class DemoIntSlider : MonoBehaviour
    {
        public int value1 = 15;
        public int value2 = 50;
    }

    [CustomEditor(typeof(DemoIntSlider))]
    public class DemoIntSliderGUI : Editor
    {
        private int tempValue;

        private SerializedProperty spValue1;
        private SerializedProperty spValue2;
        private int value3;

        public void OnEnable()
        {
            spValue1 = serializedObject.FindProperty("value1");
            spValue2 = serializedObject.FindProperty("value2");
        }

        public override void OnInspectorGUI()
        {
            tempValue = EditorGUILayout.IntSlider("Standard Slider [0..10]", tempValue, 0, 10);

            DustGUI.Space();

            DustGUI.IntSlider.Create(10, 20, 0, 50).LinkEditor(this).SetTitle("1: [0 .. [10 - 20] .. 50]").Draw(spValue1);

            DustGUI.IntSlider.Create(1, 100).LinkEditor(this).SetTitle("2: [.... [1 - 100] ....]").Draw(spValue2);

            DustGUI.Space();

            DustGUI.PrefixLabel("3: [-100 .. [-50 - 50] .. 100]");
            var slider = new DustGUI.IntSlider(-50, 50, -100, 100);
            slider.stepValue = 1;
            value3 = slider.Draw(value3);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
