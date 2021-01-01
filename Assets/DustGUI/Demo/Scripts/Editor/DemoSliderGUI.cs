using DustEngine;
using UnityEditor;
using UnityEngine;

namespace DustDemo
{
    [CustomEditor(typeof(DemoSlider))]
    [CanEditMultipleObjects]
    public class DemoSliderGUI : Editor
    {
        private DemoSlider main;

        private SerializedProperty spValue1;
        private SerializedProperty spValue2;

        public void OnEnable()
        {
            main = target as DemoSlider;

            spValue1 = serializedObject.FindProperty("value1");
            spValue2 = serializedObject.FindProperty("value2");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            main.value0 = EditorGUILayout.Slider("Standard Slider [0..5]", main.value0, 0f, 5f);

            DustGUI.SpaceLine();

            DustGUI.ExtraSlider.Create(1f, 2f, 0.01f, 0f, 5f).LinkEditor(this).Draw("[0f .. [1f - 2f] .. 5f]", spValue1);

            DustGUI.ExtraSlider.Create().LinkEditor(this).SetSlider(1f, 10f).Draw("[.... [1f - 10f] ....]", spValue2);

            DustGUI.SpaceLine();

            DustGUI.Label("[-100f .. [-50f - 50f] .. 100f]");

            // Use one Slider instance to draw few UI-Elements
            var slider = new DustGUI.ExtraSlider(-50f, 50f, 0.5f, -100f, 100f);
            slider.LinkEditor(this);

            main.value3 = slider.Draw(main.value3);
            main.value4 = slider.Draw("Title", main.value4);
            main.value5 = slider.Draw(new GUIContent("Title with tooltip", "Tooltip here"), main.value5);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
