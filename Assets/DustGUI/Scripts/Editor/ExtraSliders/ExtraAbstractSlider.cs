using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DustEngine
{
    public static partial class DustGUI
    {
        public abstract class ExtraAbstractSlider
        {
            public class UIConfig
            {
                private bool m_ShowLabel = true;
                public bool showLabel
                {
                    get => m_ShowLabel;
                    set => m_ShowLabel = value;
                }

                private bool m_ShowButtons = true;
                public bool showButtons
                {
                    get => m_ShowButtons;
                    set => m_ShowButtons = value;
                }

                private bool m_ShowValue = true;
                public bool showValue
                {
                    get => m_ShowValue;
                    set => m_ShowValue = value;
                }
            }

            //----------------------------------------------------------------------------------------------------------

            public readonly UIConfig ui = new UIConfig();

            protected static Rect s_SliderDraggingRect = Rect.zero;

            // Link to parent editor require for force repaint it on changing value by dragging
            // But it's optional to use
            protected Editor m_Editor;
            public Editor editor
            {
                get => m_Editor;
                set => m_Editor = value;
            }
        }
    }
}
