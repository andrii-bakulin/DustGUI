using UnityEngine;
using UnityEditor;

namespace DustEngine
{
    public static partial class DustGUI
    {
        public class ExtraIntSlider
        {
            public class UIConfig
            {
                public bool showLabel = true;
                public bool showButtons = true;
                public bool showValue = true;
            }

            public static Rect s_SliderDraggingRect = Rect.zero;

            public UIConfig ui = new UIConfig();

            public int sliderMin;
            public int sliderMax;
            public int sliderStep;
            public int limitMin;
            public int limitMax;

            public bool isChanged;

            // Link to parent editor require for force repaint it on changing value by dragging
            // But it's optional to use
            public Editor editor;

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
            {
                return Draw(null, value, null);
            }

            public int Draw(string label, int value)
            {
                return Draw(new GUIContent(label), value, null);
            }

            public int Draw(GUIContent label, int value)
            {
                return Draw(label, value, null);
            }

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            public bool Draw(SerializedProperty propertyValue)
            {
                Draw(null, 0, propertyValue);
                return isChanged;
            }

            public bool Draw(string label, SerializedProperty propertyValue)
            {
                Draw(new GUIContent(label), 0, propertyValue);
                return isChanged;
            }

            public bool Draw(GUIContent label, SerializedProperty propertyValue)
            {
                Draw(label, 0, propertyValue);
                return isChanged;
            }

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            private int Draw(GUIContent label, int value, SerializedProperty propertyValue)
            {
                if (propertyValue != null)
                    value = propertyValue.intValue;

                int deltaChange = 0;
                int oldValue;
                int newValue;

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
                    newValue = (int) GUILayout.HorizontalSlider(oldValue, sliderMin, sliderMax);

                    if (!oldValue.Equals(newValue))
                    {
                        value = newValue;
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

                            oldValue = propertyValue.intValue;
                            EditorGUILayout.PropertyField(propertyValue, GUIContent.none, GUILayout.Width(EditorGUIUtility.fieldWidth));
                            newValue = Mathf.Clamp(propertyValue.intValue, limitMin, limitMax);

                            IndentLevelReset(indentLevel);
                        }
                        else
                        {
                            oldValue = value;
                            newValue = EditorGUILayout.IntField(value, GUILayout.Width(EditorGUIUtility.fieldWidth));
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
                        deltaChange = Mathf.CeilToInt(sliderStep * Event.current.delta.x * 0.25f); // 0.25f -> downgrade sensitivity
                    }
                }

                if (!Mathf.Approximately(deltaChange, 0f))
                {
                    oldValue = value;
                    newValue = Mathf.Clamp(oldValue + deltaChange, limitMin, limitMax);

                    if (!oldValue.Equals(newValue))
                    {
                        value = newValue;
                        isChanged = true;

                        if (editor != null)
                            editor.Repaint();
                    }
                }

                if (propertyValue != null)
                    propertyValue.intValue = value;

                return value;
            }
        }
    }
}
