using DustEngine;
using UnityEditor;
using UnityEngine;

namespace DustDemo
{
    [CustomEditor(typeof(DemoIntSlider))]
    public class DemoIntSliderGUI : Editor
    {
        private int tempValue;

        private SerializedProperty spValue1;
        private SerializedProperty spValue2;
        private int value3;
        private int value4;
        private int value5;

        public void OnEnable()
        {
            spValue1 = serializedObject.FindProperty("value1");
            spValue2 = serializedObject.FindProperty("value2");
        }

        public override void OnInspectorGUI()
        {
            tempValue = EditorGUILayout.IntSlider("Standard Slider [0..10]", tempValue, 0, 10);


            DustGUI.SpaceLine();


            DustGUI.IntSliderExt.Create(10, 20, 1, 0, 50).LinkEditor(this).Draw("[0 .. [10 - 20] .. 50]", spValue1);

            DustGUI.IntSliderExt.Create().LinkEditor(this).SetSlider(1, 100).Draw("[.... [1 - 100] ....]", spValue2);


            DustGUI.SpaceLine();


            DustGUI.Label("[-100 .. [-50 - 50] .. 100]");

            // Use one Slider instance to draw few UI-Elements
            var slider = new DustGUI.IntSliderExt(-50, 50, 5, -100, 100);
            value3 = slider.Draw(value3);
            value4 = slider.Draw("Title", value4);
            value5 = slider.Draw(new GUIContent("Title with tooltip", "Tooltip here"), value5);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
