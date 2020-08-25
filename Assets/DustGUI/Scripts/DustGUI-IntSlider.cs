using UnityEngine;
using UnityEditor;

namespace DustEngine
{
#if UNITY_EDITOR
    public static partial class DustGUI
    {
        public class IntSlider
        {
            public int leftValue;
            public int rightValue;
            public int stepValueOnClick;
            public int stepValueOnDrag;
            public int leftLimit;
            public int rightLimit;

            public bool isChanged;

            public GUIContent content = new GUIContent();

            // Link to parent editor require for force repaint it on changing value by dragging
            // But it's optional
            public Editor editor;

            public bool showControlButtons = true;

            //----------------------------------------------------------------------------------------------------------

            public static IntSlider Create()
            {
                return new IntSlider();
            }

            public static IntSlider Create(int leftValue, int rightValue, int leftLimit = int.MinValue, int rightLimit = int.MaxValue)
            {
                return new IntSlider(leftValue, rightValue, leftLimit, rightLimit);
            }

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            public IntSlider()
            {
                Init(0, 100, int.MinValue, int.MaxValue);
            }

            public IntSlider(int leftValue, int rightValue, int leftLimit = int.MinValue, int rightLimit = int.MaxValue)
            {
                Init(leftValue, rightValue, leftLimit, rightLimit);
            }

            public void Init(int initLeftValue, int initRightValue, int initLeftLimit, int initRightLimit)
            {
                leftValue = initLeftValue;
                rightValue = initRightValue;

                stepValueOnClick = Mathf.CeilToInt((initRightValue - initLeftValue) * 0.10f);
                stepValueOnDrag = Mathf.CeilToInt((initRightValue - initLeftValue) * 0.025f);

                leftLimit = initLeftLimit;
                rightLimit = initRightLimit;
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
                        if (IconButton(Config.RESOURCE_ICON_ARROW_LEFT, 16, 16, value <= leftLimit))
                            deltaChange = -stepValueOnClick;
                    }

                    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
                    // Slider

                    oldValue = Mathf.Clamp(value, leftValue, rightValue);
                    newValue = (int) GUILayout.HorizontalSlider(oldValue, leftValue, rightValue);

                    if (!oldValue.Equals(newValue))
                    {
                        value = newValue;
                        isChanged = true;
                    }

                    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

                    if (showControlButtons)
                    {
                        if (IconButton(Config.RESOURCE_ICON_ARROW_RIGHT, 16, 16, value >= rightLimit))
                            deltaChange = +stepValueOnClick;
                    }

                    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
                    // Text

                    if (propertyValue != null)
                    {
                        int indentLevel = EditorGUI.indentLevel;
                        EditorGUI.indentLevel = 0; // Because it'll try to add left-spacing when draw text-field

                        oldValue = propertyValue.intValue;
                        EditorGUILayout.PropertyField(propertyValue, GUIContent.none, GUILayout.Width(EditorGUIUtility.fieldWidth));
                        newValue = Mathf.Clamp(propertyValue.intValue, leftLimit, rightLimit);

                        EditorGUI.indentLevel = indentLevel;
                    }
                    else
                    {
                        oldValue = value;
                        newValue = EditorGUILayout.IntField(value, GUILayout.Width(EditorGUIUtility.fieldWidth));
                        newValue = Mathf.Clamp(newValue, leftLimit, rightLimit);
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
                        deltaChange = Mathf.RoundToInt(stepValueOnDrag * Event.current.delta.x);

                        if (editor != null)
                            editor.Repaint();
                    }
                }

                if (!Mathf.Approximately(deltaChange, 0f))
                {
                    oldValue = value;
                    newValue = Mathf.Clamp(oldValue + deltaChange, leftLimit, rightLimit);

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
#endif
}
