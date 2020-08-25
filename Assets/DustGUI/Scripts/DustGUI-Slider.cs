using UnityEngine;
using UnityEditor;

namespace DustEngine
{
#if UNITY_EDITOR
    public static partial class DustGUI
    {
        public class Slider
        {
            public float leftValue;
            public float rightValue;
            public float stepValue;
            public float leftLimit;
            public float rightLimit;

            public bool isChanged;

            public GUIContent content = new GUIContent();

            // Link to parent editor require for force repaint it on changing value by dragging
            // But it's optional
            public Editor editor;

            public bool showControlButtons = true;

            //----------------------------------------------------------------------------------------------------------

            public static Slider Create()
            {
                return new Slider();
            }

            public static Slider Create(float leftValue, float rightValue, float leftLimit = float.MinValue, float rightLimit = float.MaxValue)
            {
                return new Slider(leftValue, rightValue, leftLimit, rightLimit);
            }

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

            public Slider()
            {
                Init(0f, 1f, float.MinValue, float.MaxValue);
            }

            public Slider(float leftValue, float rightValue, float leftLimit = float.MinValue, float rightLimit = float.MaxValue)
            {
                Init(leftValue, rightValue, leftLimit, rightLimit);
            }

            public void Init(float initLeftValue, float initRightValue, float initLeftLimit, float initRightLimit)
            {
                leftValue = initLeftValue;
                rightValue = initRightValue;
                stepValue = (rightValue - leftValue) * 0.01f;
                leftLimit = initLeftLimit;
                rightLimit = initRightLimit;
            }

            //----------------------------------------------------------------------------------------------------------

            public Slider SetTitle(string label, string tooltip = "")
            {
                content = new GUIContent(label, tooltip);
                return this;
            }

            public Slider SetTitle(GUIContent label)
            {
                content = label;
                return this;
            }

            public Slider LinkEditor(Editor parentEditor)
            {
                editor = parentEditor;
                return this;
            }

            //----------------------------------------------------------------------------------------------------------

            public float Draw(float value)
            {
                return Draw(value, null);
            }

            public bool Draw(SerializedProperty propertyValue)
            {
                Draw(0f, propertyValue);
                return isChanged;
            }

            private float Draw(float value, SerializedProperty propertyValue)
            {
                if (propertyValue != null)
                    value = propertyValue.floatValue;

                float deltaChange = 0f;
                float oldValue;
                float newValue;

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
                        ButtonState state = value <= leftLimit ? ButtonState.Locked : ButtonState.Normal;
                        if (IconButton(Config.RESOURCE_ICON_ARROW_LEFT, 16, 16, state))
                            deltaChange = -stepValue;
                    }

                    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
                    // Slider

                    oldValue = Mathf.Clamp(value, leftValue, rightValue);
                    newValue = GUILayout.HorizontalSlider(oldValue, leftValue, rightValue);

                    if (!oldValue.Equals(newValue))
                    {
                        value = (float) System.Math.Round(newValue, 2);
                        isChanged = true;
                    }

                    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

                    if (showControlButtons)
                    {
                        ButtonState state = value >= rightLimit ? ButtonState.Locked : ButtonState.Normal;
                        if (IconButton(Config.RESOURCE_ICON_ARROW_RIGHT, 16, 16, state))
                            deltaChange = +stepValue;
                    }

                    // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
                    // Text

                    if (propertyValue != null)
                    {
                        int indentLevel = EditorGUI.indentLevel;
                        EditorGUI.indentLevel = 0; // Because it'll try to add left-spacing when draw text-field

                        oldValue = propertyValue.floatValue;
                        EditorGUILayout.PropertyField(propertyValue, GUIContent.none, GUILayout.Width(EditorGUIUtility.fieldWidth));
                        newValue = Mathf.Clamp(propertyValue.floatValue, leftLimit, rightLimit);

                        EditorGUI.indentLevel = indentLevel;
                    }
                    else
                    {
                        oldValue = value;
                        newValue = EditorGUILayout.FloatField(value, GUILayout.Width(EditorGUIUtility.fieldWidth));
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
                        deltaChange = stepValue * Event.current.delta.x;

                        if (editor != null)
                            editor.Repaint();
                    }
                }

                if (!Mathf.Approximately(deltaChange, 0f))
                {
                    oldValue = value;

                    newValue = Mathf.Clamp(oldValue + deltaChange, leftLimit, rightLimit);
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
#endif
}
