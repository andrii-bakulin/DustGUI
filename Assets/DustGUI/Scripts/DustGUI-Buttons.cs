using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
namespace DustEngine
{
    public static partial class DustGUI
    {
        private static GUIStyle iconButtonStyle = null;

        public enum ButtonState
        {
            Normal = 0,
            Pressed = 1,
            Locked = 2,
        }

        static DustGUI()
        {
            int pad = Config.ICON_BUTTON_PADDING;
            iconButtonStyle = new GUIStyle (GUI.skin.button);
            iconButtonStyle.padding = new RectOffset(pad, pad, pad, pad);
        }

        //--------------------------------------------------------------------------------------------------------------

        public static bool Button(string caption, ButtonState state = ButtonState.Normal)
        {
            var defaultBgColor = GUI.backgroundColor;

            switch (state)
            {
                default:
                case ButtonState.Normal: break;
                case ButtonState.Pressed: GUI.backgroundColor = Config.BUTTON_PRESSED_COLOR; break;
                case ButtonState.Locked: GUI.backgroundColor = Config.BUTTON_PRESSED_COLOR; break;
            }

            bool res = GUILayout.Button(caption, GUILayout.Height(30));

            GUI.backgroundColor = defaultBgColor;

            return state == ButtonState.Locked ? false : res;
        }

        //--------------------------------------------------------------------------------------------------------------

        public static bool IconButton(string iconName, ButtonState state = ButtonState.Normal)
        {
            return IconButton(iconName, Config.ICON_BUTTON_WIDTH, Config.ICON_BUTTON_HEIGHT, state);
        }

        public static bool IconButton(string iconName, float width, float height, ButtonState state = ButtonState.Normal)
        {
            return IconButton(Resources.Load(iconName) as Texture, width, height, null, state);
        }

        public static bool IconButton(string iconName, float width, float height, GUIStyle style, ButtonState state = ButtonState.Normal)
        {
            return IconButton(Resources.Load(iconName) as Texture, width, height, style, state);
        }

        public static bool IconButton(Texture texture, ButtonState state = ButtonState.Normal)
        {
            return IconButton(texture, Config.ICON_BUTTON_WIDTH, Config.ICON_BUTTON_HEIGHT, null, state);
        }

        public static bool IconButton(Texture texture, float width, float height, ButtonState state = ButtonState.Normal)
        {
            return IconButton(texture, width, height, null, state);
        }

        public static bool IconButton(Texture texture, float width, float height, GUIStyle style, ButtonState state = ButtonState.Normal)
        {
            if (style == null)
                style = iconButtonStyle;

            var defaultBgColor = GUI.backgroundColor;

            switch (state)
            {
                default:
                case ButtonState.Normal: break;
                case ButtonState.Pressed: GUI.backgroundColor = Config.BUTTON_PRESSED_COLOR; break;
                case ButtonState.Locked: GUI.backgroundColor = Config.BUTTON_PRESSED_COLOR; break;
            }

            bool res = GUILayout.Button(texture, style, new GUILayoutOption[]
            {
                GUILayout.Width(width),
                GUILayout.Height(height)
            });

            GUI.backgroundColor = defaultBgColor;

            return state == ButtonState.Locked ? false : res;
        }
    }
}
#endif
