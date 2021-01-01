using UnityEngine;
using UnityEditor;

namespace DustEngine
{
    public static partial class DustGUI
    {
        public class ExtraSlider : ExtraAbstractSlider
        {
            private float m_SliderMin;
            public float sliderMin
            {
                get => m_SliderMin;
                set => m_SliderMin = value;
            }

            private float m_SliderMax;
            public float sliderMax
            {
                get => m_SliderMax;
                set => m_SliderMax = value;
            }

            private float m_SliderStep;
            public float sliderStep
            {
                get => m_SliderStep;
                set => m_SliderStep = value;
            }

            private float m_LimitMin;
            public float limitMin
            {
                get => m_LimitMin;
                set => m_LimitMin = value;
            }

            private float m_LimitMax;
            public float limitMax
            {
                get => m_LimitMax;
                set => m_LimitMax = value;
            }

            private bool m_IsChanged;
            public bool isChanged
            {
                get => m_IsChanged;
                set => m_IsChanged = value;
            }

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
                => Draw(null, value, null);

            public float Draw(string label, float value)
                => Draw(new GUIContent(label), value, null);

            public float Draw(GUIContent label, float value)
                => Draw(label, value, null);

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            public bool Draw(SerializedProperty propertyValue)
            {
                float newValue = Draw(null, propertyValue.floatValue, propertyValue);

                if (isChanged)
                    propertyValue.floatValue = newValue;

                return isChanged;
            }

            public bool Draw(string label, SerializedProperty propertyValue)
            {
                float newValue = Draw(new GUIContent(label), propertyValue.floatValue, propertyValue);

                if (isChanged)
                    propertyValue.floatValue = newValue;

                return isChanged;
            }

            public bool Draw(GUIContent label, SerializedProperty propertyValue)
            {
                float newValue = Draw(label, propertyValue.floatValue, propertyValue);

                if (isChanged)
                    propertyValue.floatValue = newValue;

                return isChanged;
            }

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            private float Draw(GUIContent label, float value, SerializedProperty propertyValue)
            {
                float deltaChange = 0f;

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

                    var sliderOldValue = Mathf.Clamp(value, sliderMin, sliderMax);
                    var sliderNewValue = GUILayout.HorizontalSlider(sliderOldValue, sliderMin, sliderMax);

                    if (!sliderOldValue.Equals(sliderNewValue))
                    {
                        value = (float) System.Math.Round(sliderNewValue, 2);
                        isChanged = true;

                        BlurFocusControl();
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
                        float textOldValue;
                        float textNewValue;

                        if (propertyValue != null)
                        {
                            // Because it'll try to add left-spacing when draw text-field
                            int indentLevel = IndentLevelReset();

                            textOldValue = propertyValue.floatValue;
                            EditorGUILayout.PropertyField(propertyValue, GUIContent.none, GUILayout.Width(EditorGUIUtility.fieldWidth));
                            textNewValue = propertyValue.floatValue;

                            IndentLevelReset(indentLevel);
                        }
                        else
                        {
                            textOldValue = value;
                            textNewValue = EditorGUILayout.FloatField(value, GUILayout.Width(EditorGUIUtility.fieldWidth));
                        }

                        textNewValue = Mathf.Clamp(textNewValue, limitMin, limitMax);

                        if (!textOldValue.Equals(textNewValue))
                        {
                            value = textNewValue;
                            isChanged = true;
                        }
                    }
                }
                EditorGUILayout.EndHorizontal();

                if (!labelRect.Equals(Rect.zero))
                {
                    if (Event.current.type == EventType.MouseDown && labelRect.Contains(Event.current.mousePosition))
                    {
                        sliderDraggingRect = labelRect;
                    }
                    else if (Event.current.type == EventType.MouseUp)
                    {
                        sliderDraggingRect = Rect.zero;
                    }
                    else if (Event.current.type == EventType.MouseDrag && labelRect.Equals(sliderDraggingRect))
                    {
                        deltaChange = sliderStep * Event.current.delta.x;
                    }
                }

                if (!Mathf.Approximately(deltaChange, 0f))
                {
                    var oldValue = value;

                    var newValue = Mathf.Clamp(oldValue + deltaChange, limitMin, limitMax);
                    newValue = (float) System.Math.Round(newValue, 2);

                    if (!oldValue.Equals(newValue))
                    {
                        value = newValue;
                        isChanged = true;

                        if (editor != null)
                            editor.Repaint();
                    }
                }

                return value;
            }
        }
    }
}
