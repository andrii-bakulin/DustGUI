using UnityEngine;
using UnityEditor;

namespace DustEngine
{
    public static partial class DustGUI
    {
        public static class Config
        {
            public const int ICON_BUTTON_WIDTH = 28;
            public const int ICON_BUTTON_HEIGHT = 28;
            public const int ICON_BUTTON_PADDING = 2;

            public static readonly Color BUTTON_PRESSED_COLOR = new Color(0.75f, 0.75f, 0.75f);

            public const string RESOURCE_ICON_ARROW_DOWN = "DustGUI/Arrow-Down";
            public const string RESOURCE_ICON_ARROW_LEFT = "DustGUI/Arrow-Left";
            public const string RESOURCE_ICON_ARROW_RIGHT = "DustGUI/Arrow-Right";
            public const string RESOURCE_ICON_ARROW_UP = "DustGUI/Arrow-Up";
        }
    }
}
