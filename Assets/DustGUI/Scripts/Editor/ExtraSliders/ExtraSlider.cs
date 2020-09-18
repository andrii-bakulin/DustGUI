using UnityEngine;
using UnityEditor;

namespace DustEngine
{
    public static partial class DustGUI
    {
        public class ExtraSlider
        {
            public class UIConfig
            {
                public bool showLabel = true;
                public bool showButtons = true;
                public bool showValue = true;
            }

            public static Rect s_SliderDraggingRect = Rect.zero;

            public UIConfig ui = new UIConfig();

            public float sliderMin;
            public float sliderMax;
            public float sliderStep;
            public float limitMin;
            public float limitMax;

            public bool isChanged;

            // Link to parent editor require for force repaint it on changing value by dragging
            // But it's optional to use
            public Editor editor;

            //----------------------------------------------------------------------------------------------------------

            public static ExtraSlider Create()
                => Create(0f, 1f, 0.01f, float.MinValue, float.MaxValue);

            public static ExtraSlider Create01()
                => Create(0f, 1f, 0.01f, 0f, 1f);

            public static ExtraSlider Create(float sliderMin, float sliderMax)
                => Create(sliderMin, sliderMax, 0f, float.MinValue, float.MaxValue);

            public static ExtraSlider Create(float sliderMin, float sliderMax, float sliderStep)
                => Create(sliderMin, sliderMax, sliderStep, float.MinValue, float.MaxValue);

            public static ExtraSlider Create(float sliderMin, float sliderMax, float sliderStep, float limitMin, float limitMax)
            {
                return new ExtraSlider(sliderMin, sliderMax, sliderStep, limitMin, limitMax);
            }

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            public ExtraSlider()
            {
                Init(0f, 1f, 0f, float.MinValue, float.MaxValue);
            }

            public ExtraSlider(float sliderMin, float sliderMax)
            {
                Init(sliderMin, sliderMax, 0f, float.MinValue, float.MaxValue);
            }

            public ExtraSlider(float sliderMin, float sliderMax, float sliderStep)
            {
                Init(sliderMin, sliderMax, sliderStep, float.MinValue, float.MaxValue);
            }

            public ExtraSlider(float sliderMin, float sliderMax, float sliderStep, float limitMin, float limitMax)
            {
                Init(sliderMin, sliderMax, sliderStep, limitMin, limitMax);
            }

            public void Init(float setSliderMin, float setSliderMax, float setSliderStep, float setLimitMin, float setLimitMax)
            {
                SetSlider(setSliderMin, setSliderMax, setSliderStep);
                SetLimits(setLimitMin, setLimitMax);
            }

            //----------------------------------------------------------------------------------------------------------

            public ExtraSlider LinkEditor(Editor parentEditor)
            {
                editor = parentEditor;
                return this;
            }

            public ExtraSlider SetSlider(float setSliderMin, float setSliderMax)
                => SetSlider(setSliderMin, setSliderMax, 0f);

            public ExtraSlider SetSlider(float setSliderMin, float setSliderMax, float setSliderStep)
            {
                sliderMin = Mathf.Min(setSliderMin, setSliderMax);
                sliderMax = Mathf.Max(setSliderMin, setSliderMax);

                sliderStep = setSliderStep > 0f ? setSliderStep : (sliderMax - sliderMin) * 0.01f;
                return this;
            }

            public ExtraSlider SetLimits(float setLimitMin, float setLimitMax)
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

                Rect labelRect = Rect.zero;

                EditorGUILayout.BeginHorizontal();
                {
                    if (ui.showLabel && label != null)
                    {
                        EditorGUILayout.PrefixLabel(label);
                        labelRect = GUILayoutUtility.GetLastRect();

                        EditorGUIUtility.AddCursorRect(labelRect, MouseCursor.SlideArrow);
                    }

                    if (ui.showButtons)
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

                    if (ui.showButtons)
                    {
                        ButtonState state = value >= limitMax ? ButtonState.Locked : ButtonState.Normal;
                        if (IconButton(Config.RESOURCE_ICON_ARROW_RIGHT, 16, 16, state))
                            deltaChange = +sliderStep;
                    }

                    if (ui.showValue)
                    {
                        if (propertyValue != null)
                        {
                            // Because it'll try to add left-spacing when draw text-field
                            int indentLevel = IndentLevelReset();

                            oldValue = propertyValue.floatValue;
                            EditorGUILayout.PropertyField(propertyValue, GUIContent.none, GUILayout.Width(EditorGUIUtility.fieldWidth));
                            newValue = Mathf.Clamp(propertyValue.floatValue, limitMin, limitMax);

                            IndentLevelReset(indentLevel);
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
                    }
                }
                EditorGUILayout.EndHorizontal();

                if (!labelRect.Equals(Rect.zero))
                {
                    if (Event.current.type == EventType.MouseDown && labelRect.Contains(Event.current.mousePosition))
                    {
                        s_SliderDraggingRect = labelRect;
                    }
                    else if (Event.current.type == EventType.MouseUp)
                    {
                        s_SliderDraggingRect = Rect.zero;
                    }
                    else if (Event.current.type == EventType.MouseDrag && labelRect.Equals(s_SliderDraggingRect))
                    {
                        deltaChange = sliderStep * Event.current.delta.x;
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

                        if (editor != null)
                            editor.Repaint();
                    }
                }

                if (propertyValue != null)
                    propertyValue.floatValue = value;

                return value;
            }
        }
    }
}
