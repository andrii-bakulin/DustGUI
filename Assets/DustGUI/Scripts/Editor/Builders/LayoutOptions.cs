using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DustEngine
{
    public static partial class DustGUI
    {
        public static LayoutOptions NewLayoutOptions()
        {
            return new LayoutOptions();
        }

        public static LayoutOptions NewLayoutOptions(float width, float height)
        {
            var options = new LayoutOptions();
            options.WidthAndHeight(width, height);
            return options;
        }

        public class LayoutOptions
        {
            private readonly List<GUILayoutOption> m_Options;

            public LayoutOptions()
            {
                m_Options = new List<GUILayoutOption>();
            }

            //----------------------------------------------------------------------------------------------------------

            public LayoutOptions Width(float value)
            {
                if (value > 0f)
                    m_Options.Add(GUILayout.Width(value));

                return this;
            }

            public LayoutOptions MinWidth(float value)
            {
                m_Options.Add(GUILayout.MinWidth(value));
                return this;
            }

            public LayoutOptions MaxWidth(float value)
            {
                m_Options.Add(GUILayout.MaxWidth(value));
                return this;
            }

            public LayoutOptions ExpandWidth()
                => ExpandWidth(true);

            public LayoutOptions ExpandWidth(bool value)
            {
                m_Options.Add(GUILayout.ExpandWidth(value));
                return this;
            }

            //----------------------------------------------------------------------------------------------------------

            public LayoutOptions Height(float value)
            {
                if (value > 0f)
                    m_Options.Add(GUILayout.Height(value));

                return this;
            }

            public LayoutOptions MinHeight(float value)
            {
                m_Options.Add(GUILayout.MinHeight(value));
                return this;
            }

            public LayoutOptions MaxHeight(float value)
            {
                m_Options.Add(GUILayout.MaxHeight(value));
                return this;
            }

            public LayoutOptions ExpandHeight()
                => ExpandHeight(true);

            public LayoutOptions ExpandHeight(bool value)
            {
                m_Options.Add(GUILayout.ExpandHeight(value));
                return this;
            }

            //----------------------------------------------------------------------------------------------------------

            public LayoutOptions WidthAndHeight(float width, float height)
            {
                return Width(width).Height(height);
            }

            //----------------------------------------------------------------------------------------------------------

            public GUILayoutOption[] Build()
            {
                return m_Options.ToArray();
            }
        }
    }
}
