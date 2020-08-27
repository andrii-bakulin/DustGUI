using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
namespace DustEngine
{
    public static partial class DustGUI
    {
        public class IntSliderExt
        {
            public int sliderMin;
            public int sliderMax;
            public int sliderStep;
            public int limitMin;
            public int limitMax;

            public bool isChanged;

            // Link to parent editor require for force repaint it on changing value by dragging
            // But it's optional to use
            public Editor editor;

            public bool showControlButtons = true;

            //----------------------------------------------------------------------------------------------------------

            public static IntSliderExt Create()
            {
                return new IntSliderExt();
            }

            public static SliderExt Create100()
            {
                return new SliderExt(0, 100, 1, 0, 100);
            }

            public static IntSliderExt Create(int sliderMin, int sliderMax, int sliderStep = 0, int limitMin = int.MinValue, int limitMax = int.MaxValue)
            {
                return new IntSliderExt(sliderMin, sliderMax, sliderStep, limitMin, limitMax);
            }

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            public IntSliderExt()
            {
                Init(0, 100, 0, int.MinValue, int.MaxValue);
            }

            public IntSliderExt(int sliderMin, int sliderMax, int sliderStep = 0, int limitMin = int.MinValue, int limitMax = int.MaxValue)
            {
                Init(sliderMin, sliderMax, sliderStep, limitMin, limitMax);
            }

            public void Init(int setSliderMin, int setSliderMax, int setSliderStep, int setLimitMin, int setLimitMax)
            {
                SetSlider(setSliderMin, setSliderMax, setSliderStep);
                SetLimits(setLimitMin, setLimitMax);
            }

            //----------------------------------------------------------------------------------------------------------

            public IntSliderExt LinkEditor(Editor parentEditor)
            {
                editor = parentEditor;
                return this;
            }

            public IntSliderExt SetSlider(int setSliderMin, int setSliderMax, int setSliderStep = 0)
            {
                sliderMin = Mathf.Min(setSliderMin, setSliderMax);
                sliderMax = Mathf.Max(setSliderMin, setSliderMax);

                sliderStep = setSliderStep > 0 ? setSliderStep : Mathf.CeilToInt((sliderMax - sliderMin) * 0.01f);
                return this;
            }

            public IntSliderExt SetLimits(int setLimitMin, int setLimitMax)
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
