using DustEngine;
using UnityEditor;
using UnityEngine;

namespace DustDemo
{
    [CustomEditor(typeof(DemoSlider))]
    public class DemoSliderGUI : Editor
    {
        private float tempValue;

        private SerializedProperty spValue1;
        private SerializedProperty spValue2;
        private float value3;
        private float value4;
        private float value5;

        public void OnEnable()
        {
            spValue1 = serializedObject.FindProperty("value1");
            spValue2 = serializedObject.FindProperty("value2");
        }

        public override void OnInspectorGUI()
        {
            tempValue = EditorGUILayout.Slider("Standard Slider [0..5]", tempValue, 0f, 5f);


            DustGUI.SpaceLine();


            DustGUI.SliderExt.Create(1f, 2f, 0.01f, 0f, 5f).LinkEditor(this).Draw("[0f .. [1f - 2f] .. 5f]", spValue1);

            DustGUI.SliderExt.Create().LinkEditor(this).SetSlider(1f, 10f).Draw("[.... [1f - 10f] ....]", spValue2);


            DustGUI.SpaceLine();


            DustGUI.Label("[-100f .. [-50f - 50f] .. 100f]");

            // Use one Slider instance to draw few UI-Elements
            var slider = new DustGUI.SliderExt(-50f, 50f, 0.5f, -100f, 100f);
            value3 = slider.Draw(value3);
            value4 = slider.Draw("Title", value4);
            value5 = slider.Draw(new GUIContent("Title with tooltip", "Tooltip here"), value5);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
