using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
namespace DustEngine
{
    public static partial class DustGUI
    {
        public class IntSlider
        {
            public int sliderMin;
            public int sliderMax;
            public int sliderStep;
            public int limitMin;
            public int limitMax;

            public bool isChanged;

            public GUIContent content = new GUIContent();

            // Link to parent editor require for force repaint it on changing value by dragging
            // But it's optional to use
            public Editor editor;

            public bool showControlButtons = true;

            //----------------------------------------------------------------------------------------------------------

            public static IntSlider Create()
            {
                return new IntSlider();
            }

            public static IntSlider Create(int sliderMin, int sliderMax, int sliderStep = 0, int limitMin = int.MinValue, int limitMax = int.MaxValue)
            {
                return new IntSlider(sliderMin, sliderMax, sliderStep, limitMin, limitMax);
            }

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            public IntSlider()
            {
                Init(0, 100, 0, int.MinValue, int.MaxValue);
            }

            public IntSlider(int sliderMin, int sliderMax, int sliderStep = 0, int limitMin = int.MinValue, int limitMax = int.MaxValue)
            {
                Init(sliderMin, sliderMax, sliderStep, limitMin, limitMax);
            }

            public void Init(int setSliderMin, int setSliderMax, int setSliderStep, int setLimitMin, int setLimitMax)
            {
                SetSlider(setSliderMin, setSliderMax, setSliderStep);
                SetLimits(setLimitMin, setLimitMax);
            }

            //----------------------------------------------------------------------------------------------------------

            public IntSlider SetTitle(string label, string tooltip = "")
            {
                content = new GUIContent(label, tooltip);
                return this;
            }

            public IntSlider SetTitle(GUIContent label)
            {
                content = label;
                return this;
            }

            public IntSlider LinkEditor(Editor parentEditor)
            {
                editor = parentEditor;
                return this;
            }

            public IntSlider SetSlider(int setSliderMin, int setSliderMax, int setSliderStep = 0)
            {
                sliderMin = Mathf.Min(setSliderMin, setSliderMax);
                sliderMax = Mathf.Max(setSliderMin, setSliderMax);

                sliderStep = setSliderStep > 0 ? setSliderStep : Mathf.CeilToInt((sliderMax - sliderMin) * 0.01f);
                return this;
            }

            public IntSlider SetLimits(int setLimitMin, int setLimitMax)
            {
                limitMin = Mathf.Min(setLimitMin, setLimitMax);
                limitMax = Mathf.Max(setLimitMin, setLimitMax);
                return this;
            }

            //----------------------------------------------------------------------------------------------------------

            public int Draw(int value)
            {
                return Draw(value, null);
            }

            public bool Draw(SerializedProperty propertyValue)
            {
                Draw(0, propertyValue);
                return isChanged;
            }

            private int Draw(int value, SerializedProperty propertyValue)
            {
                if (propertyValue != null)
                    value = propertyValue.intValue;

                int deltaChange = 0;
                int oldValue;
                int newValue;

                Rect sliderRect = EditorGUILayout.BeginHorizontal();
                {
                    if (content != null)
                    {
                        EditorGUILayout.PrefixLabel(content);

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
                    newValue = (int) GUILayout.HorizontalSlider(oldValue, sliderMin, sliderMax);

                    if (!oldValue.Equals(newValue))
                    {
                        value = newValue;
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

                        oldValue = propertyValue.intValue;
                        EditorGUILayout.PropertyField(propertyValue, GUIContent.none, GUILayout.Width(EditorGUIUtility.fieldWidth));
                        newValue = Mathf.Clamp(propertyValue.intValue, limitMin, limitMax);

                        EditorGUI.indentLevel = indentLevel;
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

                    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
                }
                EditorGUILayout.EndHorizontal();

                if (sliderRect.Contains(Event.current.mousePosition))
                {
                    if (Event.current.type == EventType.MouseDrag)
                    {
                        deltaChange = Mathf.RoundToInt(sliderStep * Event.current.delta.x);

                        if (editor != null)
                            editor.Repaint();
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
                    }
                }

                if (propertyValue != null)
                    propertyValue.intValue = value;

                return value;
            }
        }
    }
}
#endif
