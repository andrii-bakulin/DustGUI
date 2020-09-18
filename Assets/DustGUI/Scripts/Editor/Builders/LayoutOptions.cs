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

            public LayoutOptions Width(float width)
            {
                if (width > 0f)
                    m_Options.Add(GUILayout.Width(width));

                return this;
            }

            public LayoutOptions MinWidth(float width)
            {
                if (width > 0f)
                    m_Options.Add(GUILayout.MinWidth(width));

                return this;
            }

            public LayoutOptions MaxWidth(float width)
            {
                if (width > 0f)
                    m_Options.Add(GUILayout.MaxWidth(width));

                return this;
            }

            public LayoutOptions ExpandWidth(bool expand)
            {
                m_Options.Add(GUILayout.ExpandWidth(expand));
                return this;
            }

            //----------------------------------------------------------------------------------------------------------

            public LayoutOptions Height(float height)
            {
                if (height > 0f)
                    m_Options.Add(GUILayout.Height(height));

                return this;
            }

            public LayoutOptions MinHeight(float height)
            {
                if (height > 0f)
                    m_Options.Add(GUILayout.MinHeight(height));

                return this;
            }

            public LayoutOptions MaxHeight(float height)
            {
                if (height > 0f)
                    m_Options.Add(GUILayout.MaxHeight(height));

                return this;
            }

            public LayoutOptions ExpandHeight(bool expand)
            {
                m_Options.Add(GUILayout.ExpandHeight(expand));
                return this;
            }

            //----------------------------------------------------------------------------------------------------------

            public LayoutOptions WidthAndHeight(float width, float height)
            {
                this.Width(width);
                this.Height(height);
                return this;
            }

            //----------------------------------------------------------------------------------------------------------

            public GUILayoutOption[] Build()
            {
                return m_Options.ToArray();
            }
        }
    }
}
