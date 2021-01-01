using UnityEngine;
using UnityEditor;

namespace DustEngine
{
    public static partial class DustGUI
    {
        public class ExtraIntSlider : ExtraAbstractSlider
        {
            private int m_SliderMin;
            public int sliderMin
            {
                get => m_SliderMin;
                set => m_SliderMin = value;
            }

            private int m_SliderMax;
            public int sliderMax
            {
                get => m_SliderMax;
                set => m_SliderMax = value;
            }

            private int m_SliderStep;
            public int sliderStep
            {
                get => m_SliderStep;
                set => m_SliderStep = value;
            }

            private int m_LimitMin;
            public int limitMin
            {
                get => m_LimitMin;
                set => m_LimitMin = value;
            }

            private int m_LimitMax;
            public int limitMax
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

            public static ExtraIntSlider Create()
                => Create(0, 100, 1, int.MinValue, int.MaxValue);

            public static ExtraIntSlider Create100()
                => Create(0, 100, 1, 0, 100);

            public static ExtraIntSlider Create(int sliderMin, int sliderMax)
                => Create(sliderMin, sliderMax, 0, int.MinValue, int.MaxValue);

            public static ExtraIntSlider Create(int sliderMin, int sliderMax, int sliderStep)
                => Create(sliderMin, sliderMax, sliderStep, int.MinValue, int.MaxValue);

            public static ExtraIntSlider Create(int sliderMin, int sliderMax, int sliderStep, int limitMin, int limitMax)
            {
                return new ExtraIntSlider(sliderMin, sliderMax, sliderStep, limitMin, limitMax);
            }

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            public ExtraIntSlider()
            {
                Init(0, 100, 1, int.MinValue, int.MaxValue);
            }

            public ExtraIntSlider(int sliderMin, int sliderMax)
            {
                Init(sliderMin, sliderMax, 0, int.MinValue, int.MaxValue);
            }

            public ExtraIntSlider(int sliderMin, int sliderMax, int sliderStep)
            {
                Init(sliderMin, sliderMax, sliderStep, int.MinValue, int.MaxValue);
            }

            public ExtraIntSlider(int sliderMin, int sliderMax, int sliderStep, int limitMin, int limitMax)
            {
                Init(sliderMin, sliderMax, sliderStep, limitMin, limitMax);
            }

            public void Init(int setSliderMin, int setSliderMax, int setSliderStep, int setLimitMin, int setLimitMax)
            {
                SetSlider(setSliderMin, setSliderMax, setSliderStep);
                SetLimits(setLimitMin, setLimitMax);
            }

            //----------------------------------------------------------------------------------------------------------

            public ExtraIntSlider LinkEditor(Editor parentEditor)
            {
                editor = parentEditor;
                return this;
            }

            public ExtraIntSlider SetSlider(int setSliderMin, int setSliderMax)
                => SetSlider(setSliderMin, setSliderMax, 0);

            public ExtraIntSlider SetSlider(int setSliderMin, int setSliderMax, int setSliderStep)
            {
                sliderMin = Mathf.Min(setSliderMin, setSliderMax);
                sliderMax = Mathf.Max(setSliderMin, setSliderMax);

                sliderStep = setSliderStep > 0 ? setSliderStep : Mathf.CeilToInt((sliderMax - sliderMin) * 0.01f);
                return this;
            }

            public ExtraIntSlider SetLimits(int setLimitMin, int setLimitMax)
            {
                limitMin = Mathf.Min(setLimitMin, setLimitMax);
                limitMax = Mathf.Max(setLimitMin, setLimitMax);
                return this;
            }

            //----------------------------------------------------------------------------------------------------------

            public int Draw(int value)
                => Draw(null, value, null);

            public int Draw(string label, int value)
                => Draw(new GUIContent(label), value, null);

            public int Draw(GUIContent label, int value)
                => Draw(label, value, null);

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            public bool Draw(SerializedProperty propertyValue)
            {
                int newValue = Draw(null, propertyValue.intValue, propertyValue);

                if (isChanged)
                    propertyValue.intValue = newValue;

                return isChanged;
            }

            public bool Draw(string label, SerializedProperty propertyValue)
            {
                int newValue = Draw(new GUIContent(label), propertyValue.intValue, propertyValue);

                if (isChanged)
                    propertyValue.intValue = newValue;

                return isChanged;
            }

            public bool Draw(GUIContent label, SerializedProperty propertyValue)
            {
                int newValue = Draw(label, propertyValue.intValue, propertyValue);

                if (isChanged)
                    propertyValue.intValue = newValue;

                return isChanged;
            }

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            private int Draw(GUIContent label, int value, SerializedProperty propertyValue)
            {
                int deltaChange = 0;

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
                    var sliderNewValue = (int) GUILayout.HorizontalSlider(sliderOldValue, sliderMin, sliderMax);

                    if (!sliderOldValue.Equals(sliderNewValue))
                    {
                        value = sliderNewValue;
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
                        int textOldValue;
                        int textNewValue;

                        if (propertyValue != null)
                        {
                            // Because it'll try to add left-spacing when draw text-field
                            int indentLevel = IndentLevelReset();

                            textOldValue = propertyValue.intValue;
                            EditorGUILayout.PropertyField(propertyValue, GUIContent.none, GUILayout.Width(EditorGUIUtility.fieldWidth));
                            textNewValue = propertyValue.intValue;

                            IndentLevelReset(indentLevel);
                        }
                        else
                        {
                            textOldValue = value;
                            textNewValue = EditorGUILayout.IntField(value, GUILayout.Width(EditorGUIUtility.fieldWidth));
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
                        deltaChange = Mathf.CeilToInt(sliderStep * Event.current.delta.x * 0.25f); // 0.25f -> downgrade sensitivity
                    }
                }

                if (!Mathf.Approximately(deltaChange, 0f))
                {
                    var oldValue = value;
                    var newValue = Mathf.Clamp(oldValue + deltaChange, limitMin, limitMax);

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
