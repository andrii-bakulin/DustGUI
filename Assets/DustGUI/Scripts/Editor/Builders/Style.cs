using UnityEngine;
using UnityEditor;

namespace DustEngine
{
    public static partial class DustGUI
    {
        //--------------------------------------------------------------------------------------------------------------
        // Builders

        public static Style NewStyle()
        {
            var style = new Style();
            style.m_GUIStyle = new GUIStyle();
            return style;
        }

        public static Style NewStyle(GUIStyle other)
        {
            var style = new Style();
            style.m_GUIStyle = new GUIStyle(other);
            return style;
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        // Shortcuts

        public static Style NewStyleLabel()      => NewStyle(GUI.skin.label);
        public static Style NewStyleButton()     => NewStyle(GUI.skin.button);
        public static Style NewStylePopup()      => NewStyle(EditorStyles.popup);

        //--------------------------------------------------------------------------------------------------------------

        public class Style
        {
            internal GUIStyle m_GUIStyle;

            //----------------------------------------------------------------------------------------------------------
            // Padding

            public Style Padding(int global)
                => Padding(global, global, global, global);

            public Style Padding(int horizontal, int vertical)
                => Padding(horizontal, horizontal, vertical, vertical);

            public Style Padding(int top, int horizontal, int bottom)
                => Padding(horizontal, horizontal, top, bottom);

            public Style Padding(int left, int right, int top, int bottom)
            {
                m_GUIStyle.padding = new RectOffset(left, right, top, bottom);
                return this;
            }

            public Style PaddingLeft(int value)   { m_GUIStyle.padding.left   = value; return this; }
            public Style PaddingRight(int value)  { m_GUIStyle.padding.right  = value; return this; }
            public Style PaddingTop(int value)    { m_GUIStyle.padding.top    = value; return this; }
            public Style PaddingBottom(int value) { m_GUIStyle.padding.bottom = value; return this; }

            //----------------------------------------------------------------------------------------------------------
            // Margin

            public Style Margin(int global)
                => Margin(global, global, global, global);

            public Style Margin(int horizontal, int vertical)
                => Margin(horizontal, horizontal, vertical, vertical);

            public Style Margin(int top, int horizontal, int bottom)
                => Margin(horizontal, horizontal, top, bottom);

            public Style Margin(int left, int right, int top, int bottom)
            {
                m_GUIStyle.margin = new RectOffset(left, right, top, bottom);
                return this;
            }

            public Style MarginLeft(int value)   { m_GUIStyle.margin.left   = value; return this; }
            public Style MarginRight(int value)  { m_GUIStyle.margin.right  = value; return this; }
            public Style MarginTop(int value)    { m_GUIStyle.margin.top    = value; return this; }
            public Style MarginBottom(int value) { m_GUIStyle.margin.bottom = value; return this; }

            //----------------------------------------------------------------------------------------------------------
            // Alignment

            public Style Alignment(TextAnchor value)
            {
                m_GUIStyle.alignment = value;
                return this;
            }

            public Style AlignUpperLeft()    { m_GUIStyle.alignment = TextAnchor.UpperLeft;    return this; }
            public Style AlignUpperCenter()  { m_GUIStyle.alignment = TextAnchor.UpperCenter;  return this; }
            public Style AlignUpperRight()   { m_GUIStyle.alignment = TextAnchor.UpperRight;   return this; }

            public Style AlignMiddleLeft()   { m_GUIStyle.alignment = TextAnchor.MiddleLeft;   return this; }
            public Style AlignMiddleCenter() { m_GUIStyle.alignment = TextAnchor.MiddleCenter; return this; }
            public Style AlignMiddleRight()  { m_GUIStyle.alignment = TextAnchor.MiddleRight;  return this; }

            public Style AlignLowerLeft()    { m_GUIStyle.alignment = TextAnchor.LowerLeft;    return this; }
            public Style AlignLowerCenter()  { m_GUIStyle.alignment = TextAnchor.LowerCenter;  return this; }
            public Style AlignLowerRight()   { m_GUIStyle.alignment = TextAnchor.LowerRight;   return this; }

            //----------------------------------------------------------------------------------------------------------

            public GUIStyle Build()
            {
                return m_GUIStyle;
            }
        }
    }
}
