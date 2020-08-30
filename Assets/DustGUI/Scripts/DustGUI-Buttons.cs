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

        public static GUIStyle GetIconButtonStyle()
        {
            return new GUIStyle(iconButtonStyle);
        }

        //--------------------------------------------------------------------------------------------------------------

        private static Color m_DefaultBgColor;

        private static void ApplyButtonState(ButtonState state)
        {
            m_DefaultBgColor = GUI.backgroundColor;

            switch (state)
            {
                default:
                case ButtonState.Normal: break;
                case ButtonState.Pressed: GUI.backgroundColor = Config.BUTTON_PRESSED_COLOR; break;
                case ButtonState.Locked: GUI.backgroundColor = Config.BUTTON_PRESSED_COLOR; break;
            }
        }

        private static void RollbackButtonState()
        {
            GUI.backgroundColor = m_DefaultBgColor;
        }

        //--------------------------------------------------------------------------------------------------------------

        public static bool Button(string label, ButtonState state = ButtonState.Normal)
        {
            return Button(label, 0, 30, state);
        }

        public static bool Button(string label, float width, float height, ButtonState state = ButtonState.Normal)
        {
            ApplyButtonState(state);

            bool res = GUILayout.Button(label, PackOptions(width, height));

            RollbackButtonState();

            return state == ButtonState.Locked ? false : res;
        }

        //--------------------------------------------------------------------------------------------------------------

        public static bool IconButton(string iconName, ButtonState state)
        {
            return IconButton(Resources.Load(iconName) as Texture, Config.ICON_BUTTON_WIDTH, Config.ICON_BUTTON_HEIGHT, null, state);
        }

        public static bool IconButton(string iconName,
            float width = Config.ICON_BUTTON_WIDTH,
            float height = Config.ICON_BUTTON_HEIGHT,
            ButtonState state = ButtonState.Normal)
        {
            return IconButton(Resources.Load(iconName) as Texture, width, height, null, state);
        }

        public static bool IconButton(string iconName,
            float width,
            float height,
            GUIStyle style,
            ButtonState state = ButtonState.Normal)
        {
            return IconButton(Resources.Load(iconName) as Texture, width, height, style, state);
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static bool IconButton(Texture texture, ButtonState state)
        {
            return IconButton(new GUIContent(texture), Config.ICON_BUTTON_WIDTH, Config.ICON_BUTTON_HEIGHT, null, state);
        }

        public static bool IconButton(Texture texture,
            float width = Config.ICON_BUTTON_WIDTH,
            float height = Config.ICON_BUTTON_HEIGHT,
            ButtonState state = ButtonState.Normal)
        {
            return IconButton(new GUIContent(texture), width, height, null, state);
        }

        public static bool IconButton(Texture texture,
            float width,
            float height,
            GUIStyle style,
            ButtonState state = ButtonState.Normal)
        {
            return IconButton(new GUIContent(texture), width, height, style, state);
        }

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static bool IconButton(GUIContent content, ButtonState state)
        {
            return IconButton(content, Config.ICON_BUTTON_WIDTH, Config.ICON_BUTTON_HEIGHT, null, state);
        }

        public static bool IconButton(GUIContent content,
            float width = Config.ICON_BUTTON_WIDTH,
            float height = Config.ICON_BUTTON_HEIGHT,
            ButtonState state = ButtonState.Normal)
        {
            return IconButton(content, width, height, null, state);
        }

        public static bool IconButton(GUIContent content,
            float width,
            float height,
            GUIStyle style,
            ButtonState state = ButtonState.Normal)
        {
            if (style == null)
                style = iconButtonStyle;

            ApplyButtonState(state);

            bool res = GUILayout.Button(content, style, new GUILayoutOption[]
            {
                GUILayout.Width(width),
                GUILayout.Height(height)
            });

            RollbackButtonState();

            return state == ButtonState.Locked ? false : res;
        }
    }
}
#endif
