using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
namespace DustEngine
{
    public static partial class DustGUI
    {
        public class Slider
        {
            public float sliderMin;
            public float sliderMax;
            public float sliderStep;
            public float limitMin;
            public float limitMax;

            public bool isChanged;

            // Link to parent editor require for force repaint it on changing value by dragging
            // But it's optional to use
            public Editor editor;

            public bool showControlButtons = true;

            //----------------------------------------------------------------------------------------------------------

            public static Slider Create()
            {
                return new Slider();
            }

            public static Slider Create01()
            {
                return new Slider(0f, 1f, 0.01f, 0f, 1f);
            }

            public static Slider Create(float sliderMin, float sliderMax, float sliderStep = 0f, float limitMin = float.MinValue, float limitMax = float.MaxValue)
            {
                return new Slider(sliderMin, sliderMax, sliderStep, limitMin, limitMax);
            }

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            public Slider()
            {
                Init(0f, 1f, 0f, float.MinValue, float.MaxValue);
            }

            public Slider(float sliderMin, float sliderMax, float sliderStep = 0f, float limitMin = float.MinValue, float limitMax = float.MaxValue)
            {
                Init(sliderMin, sliderMax, sliderStep, limitMin, limitMax);
            }

            public void Init(float setSliderMin, float setSliderMax, float setSliderStep, float setLimitMin, float setLimitMax)
            {
                SetSlider(setSliderMin, setSliderMax, setSliderStep);
                SetLimits(setLimitMin, setLimitMax);
            }

            //----------------------------------------------------------------------------------------------------------

            public Slider LinkEditor(Editor parentEditor)
            {
                editor = parentEditor;
                return this;
            }

            public Slider SetSlider(float setSliderMin, float setSliderMax, float setSliderStep = 0f)
            {
                sliderMin = Mathf.Min(setSliderMin, setSliderMax);
                sliderMax = Mathf.Max(setSliderMin, setSliderMax);

                sliderStep = setSliderStep > 0f ? setSliderStep : (sliderMax - sliderMin) * 0.01f;
                return this;
            }

            public Slider SetLimits(float setLimitMin, float setLimitMax)
            {
                limitMin = Mathf.Min(setLimitMin, setLimitMax);
                limitMax = Mathf.Max(setLimitMin, setLimitMax);
                return this;
            }

            //----------------------------------------------------------------------------------------------------------

            public float Draw(float value)
            {
                return Draw(null, value, null);
            }

            public float Draw(string label, float value)
            {
                return Draw(new GUIContent(label), value, null);
            }

            public float Draw(GUIContent label, float value)
            {
                return Draw(label, value, null);
            }

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            public bool Draw(SerializedProperty propertyValue)
            {
                Draw(null, 0f, propertyValue);
                return isChanged;
            }

            public bool Draw(string label, SerializedProperty propertyValue)
            {
                Draw(new GUIContent(label), 0f, propertyValue);
                return isChanged;
            }

            public bool Draw(GUIContent label, SerializedProperty propertyValue)
            {
                Draw(label, 0f, propertyValue);
                return isChanged;
            }

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            private float Draw(GUIContent label, float value, SerializedProperty propertyValue)
            {
                if (propertyValue != null)
                    value = propertyValue.floatValue;

                float deltaChange = 0f;
                float oldValue;
                float newValue;

                Rect sliderRect = EditorGUILayout.BeginHorizontal();
                {
                    if (label != null)
                    {
                        EditorGUILayout.PrefixLabel(label);

                        Rect labelRect = sliderRect;
                        labelRect.width *= 0.33f; // Cannot find better solution for now

                        EditorGUIUtility.AddCursorRect(labelRect, MouseCursor.SlideArrow);
                    }

                    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

                    if (showControlButtons)
                    {
                        ButtonState state = value <= limitMin ? ButtonState.Locked : ButtonState.Normal;
                        if (IconButton(Config.RESOURCE_ICON_ARROW_LEFT, 16, 16, state))
                            deltaChange = -sliderStep;
                    }

                    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
                    // Slider

                    oldValue = Mathf.Clamp(value, sliderMin, sliderMax);
                    newValue = GUILayout.HorizontalSlider(oldValue, sliderMin, sliderMax);

                    if (!oldValue.Equals(newValue))
                    {
                        value = (float) System.Math.Round(newValue, 2);
                        isChanged = true;
                    }

                    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

                    if (showControlButtons)
                    {
                        ButtonState state = value >= limitMax ? ButtonState.Locked : ButtonState.Normal;
                        if (IconButton(Config.RESOURCE_ICON_ARROW_RIGHT, 16, 16, state))
                            deltaChange = +sliderStep;
                    }

                    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
                    // Text

                    if (propertyValue != null)
                    {
                        int indentLevel = EditorGUI.indentLevel;
                        EditorGUI.indentLevel = 0; // Because it'll try to add left-spacing when draw text-field

                        oldValue = propertyValue.floatValue;
                        EditorGUILayout.PropertyField(propertyValue, GUIContent.none, GUILayout.Width(EditorGUIUtility.fieldWidth));
                        newValue = Mathf.Clamp(propertyValue.floatValue, limitMin, limitMax);

                        EditorGUI.indentLevel = indentLevel;
                    }
                    else
                    {
                        oldValue = value;
                        newValue = EditorGUILayout.FloatField(value, GUILayout.Width(EditorGUIUtility.fieldWidth));
                        newValue = Mathf.Clamp(newValue, limitMin, limitMax);
                    }

                    if (!oldValue.Equals(newValue))
                    {
                        value = newValue;
                        isChanged = true;
                    }

                    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
                }
                EditorGUILayout.EndHorizontal();

                if (sliderRect.Contains(Event.current.mousePosition))
                {
                    if (Event.current.type == EventType.MouseDrag)
                    {
                        deltaChange = sliderStep * Event.current.delta.x;

                        if (editor != null)
                            editor.Repaint();
                    }
                }

                if (!Mathf.Approximately(deltaChange, 0f))
                {
                    oldValue = value;

                    newValue = Mathf.Clamp(oldValue + deltaChange, limitMin, limitMax);
                    newValue = (float) System.Math.Round(newValue, 2);

                    if (!oldValue.Equals(newValue))
                    {
                        value = newValue;
                        isChanged = true;
                    }
                }

                if (propertyValue != null)
                    propertyValue.floatValue = value;

                return value;
            }
        }
    }
}
#endif
