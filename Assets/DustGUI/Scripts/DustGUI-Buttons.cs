using UnityEngine;
using UnityEditor;

namespace DustEngine
{
#if UNITY_EDITOR
    public static partial class DustGUI
    {
        private static GUIStyle iconButtonStyle = null;

        static DustGUI()
        {
            int pad = Config.ICON_BUTTON_PADDING;
            iconButtonStyle = new GUIStyle (GUI.skin.button);
            iconButtonStyle.padding = new RectOffset(pad, pad, pad, pad);
        }

        //--------------------------------------------------------------------------------------------------------------

        public static bool Button(string caption, bool isPressed = false)
        {
            var defaultBgColor = GUI.backgroundColor;

            if (isPressed) GUI.backgroundColor = Config.BUTTON_PRESSED_COLOR;

            bool res = GUILayout.Button(caption, GUILayout.Height(30));

            if (isPressed) GUI.backgroundColor = defaultBgColor;

            return res;
        }

        //--------------------------------------------------------------------------------------------------------------

        public static bool IconButton(string iconName, bool isPressed = false)
        {
            return IconButton(iconName, Config.ICON_BUTTON_WIDTH, Config.ICON_BUTTON_HEIGHT, isPressed);
        }

        public static bool IconButton(string iconName, float width, float height, bool isPressed = false)
        {
            return IconButton(Resources.Load(iconName) as Texture, width, height, isPressed);
        }

        public static bool IconButton(Texture texture, bool isPressed = false)
        {
            return IconButton(texture, Config.ICON_BUTTON_WIDTH, Config.ICON_BUTTON_HEIGHT, isPressed);
        }

        public static bool IconButton(Texture texture, float width, float height, bool isPressed = false)
        {
            var defaultBgColor = GUI.backgroundColor;

            if (isPressed) GUI.backgroundColor = Config.BUTTON_PRESSED_COLOR;

            bool res = GUILayout.Button(texture, iconButtonStyle, new GUILayoutOption[]
            {
                GUILayout.Width(width),
                GUILayout.Height(height)
            });

            if (isPressed) GUI.backgroundColor = defaultBgColor;

            return res;
        }
    }
#endif
}
