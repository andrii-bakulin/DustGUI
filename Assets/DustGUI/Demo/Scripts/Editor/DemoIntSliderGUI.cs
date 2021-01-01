using DustEngine;
using UnityEditor;
using UnityEngine;

namespace DustDemo
{
    [CustomEditor(typeof(DemoIntSlider))]
    [CanEditMultipleObjects]
    public class DemoIntSliderGUI : Editor
    {
        private DemoIntSlider main;

        private SerializedProperty spValue1;
        private SerializedProperty spValue2;

        public void OnEnable()
        {
            main = target as DemoIntSlider;

            spValue1 = serializedObject.FindProperty("value1");
            spValue2 = serializedObject.FindProperty("value2");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            main.value0 = EditorGUILayout.IntSlider("Standard Slider [0..10]", main.value0, 0, 10);

            DustGUI.SpaceLine();

            DustGUI.ExtraIntSlider.Create(10, 20, 1, 0, 50).LinkEditor(this).Draw("[0 .. [10 - 20] .. 50]", spValue1);

            DustGUI.ExtraIntSlider.Create().LinkEditor(this).SetSlider(1, 100).Draw("[.... [1 - 100] ....]", spValue2);

            DustGUI.SpaceLine();

            DustGUI.Label("[-100 .. [-50 - 50] .. 100]");

            // Use one Slider instance to draw few UI-Elements
            var slider = new DustGUI.ExtraIntSlider(-50, 50, 5, -100, 100);
            slider.LinkEditor(this);

            main.value3 = slider.Draw(main.value3);
            main.value4 = slider.Draw("Title", main.value4);
            main.value5 = slider.Draw(new GUIContent("Title with tooltip", "Tooltip here"), main.value5);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
