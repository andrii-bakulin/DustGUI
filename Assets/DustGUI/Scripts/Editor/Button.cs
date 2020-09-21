using UnityEngine;
using UnityEditor;

namespace DustEngine
{
    public static partial class DustGUI
    {
        public enum ButtonState
        {
            Normal = 0,
            Pressed = 1,
            Locked = 2,
        }

        private static GUIStyle s_IconButtonStyle = null;
        public static GUIStyle iconButtonStyle => new GUIStyle(s_IconButtonStyle);

        static DustGUI()
        {
            s_IconButtonStyle = NewStyleButton().Padding(Config.ICON_BUTTON_PADDING).Build();
        }

        //--------------------------------------------------------------------------------------------------------------

        private static Color m_DefaultBgColor;

        private static void ApplyButtonState(ButtonState state)
        {
            m_DefaultBgColor = GUI.backgroundColor;

            if (state == ButtonState.Pressed)
            {
                GUI.backgroundColor = Config.BUTTON_PRESSED_COLOR;
            }
            else if (state == ButtonState.Locked)
            {
                GUI.backgroundColor = Config.BUTTON_LOCKED_COLOR;
            }
            // else: ButtonState.Normal -> do nothing
        }

        private static void RollbackButtonState()
        {
            GUI.backgroundColor = m_DefaultBgColor;
        }

        //--------------------------------------------------------------------------------------------------------------

        public static bool Button(string label)
            => Button(label, 0, 30, ButtonState.Normal);

        public static bool Button(string label, ButtonState state)
            => Button(label, 0, 30, state);

        public static bool Button(string label, float width, float height)
            => Button(label, width, height, ButtonState.Normal);

        public static bool Button(string label, float width, float height, ButtonState state)
            => Button(label, width, height, null, state);

        public static bool Button(string label, float width, float height, GUIStyle style, ButtonState state)
        {
            if (style == null)
                style = NewStyleButton().Build();

            ApplyButtonState(state);

            bool res = GUILayout.Button(label, style, NewLayoutOptions(width, height).Build());

            RollbackButtonState();

            return state == ButtonState.Locked ? false : res;
        }

        //--------------------------------------------------------------------------------------------------------------

        public static bool IconButton(string iconName)
            => IconButton(Resources.Load(iconName) as Texture, Config.ICON_BUTTON_WIDTH, Config.ICON_BUTTON_HEIGHT, null, ButtonState.Normal);

        public static bool IconButton(string iconName, ButtonState state)
            => IconButton(Resources.Load(iconName) as Texture, Config.ICON_BUTTON_WIDTH, Config.ICON_BUTTON_HEIGHT, null, state);

        public static bool IconButton(string iconName, GUIStyle style)
            => IconButton(Resources.Load(iconName) as Texture, Config.ICON_BUTTON_WIDTH, Config.ICON_BUTTON_HEIGHT, style, ButtonState.Normal);

        public static bool IconButton(string iconName, float width, float height)
            => IconButton(Resources.Load(iconName) as Texture, width, height, null, ButtonState.Normal);

        public static bool IconButton(string iconName, float width, float height, ButtonState state)
            => IconButton(Resources.Load(iconName) as Texture, width, height, null, state);

        public static bool IconButton(string iconName, float width, float height, GUIStyle style)
            => IconButton(Resources.Load(iconName) as Texture, width, height, style, ButtonState.Normal);

        public static bool IconButton(string iconName, float width, float height, GUIStyle style, ButtonState state)
            => IconButton(Resources.Load(iconName) as Texture, width, height, style, state);

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static bool IconButton(Texture texture)
            => IconButton(new GUIContent(texture), Config.ICON_BUTTON_WIDTH, Config.ICON_BUTTON_HEIGHT, null, ButtonState.Normal);

        public static bool IconButton(Texture texture, ButtonState state)
            => IconButton(new GUIContent(texture), Config.ICON_BUTTON_WIDTH, Config.ICON_BUTTON_HEIGHT, null, state);

        public static bool IconButton(Texture texture, GUIStyle style)
            => IconButton(new GUIContent(texture), Config.ICON_BUTTON_WIDTH, Config.ICON_BUTTON_HEIGHT, style, ButtonState.Normal);

        public static bool IconButton(Texture texture, float width, float height)
            => IconButton(new GUIContent(texture), width, height, null, ButtonState.Normal);

        public static bool IconButton(Texture texture, float width, float height, ButtonState state)
            => IconButton(new GUIContent(texture), width, height, null, state);

        public static bool IconButton(Texture texture, float width, float height, GUIStyle style)
            => IconButton(new GUIContent(texture), width, height, style, ButtonState.Normal);

        public static bool IconButton(Texture texture, float width, float height, GUIStyle style, ButtonState state)
            => IconButton(new GUIContent(texture), width, height, style, state);

        // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

        public static bool IconButton(GUIContent content)
            => IconButton(content, Config.ICON_BUTTON_WIDTH, Config.ICON_BUTTON_HEIGHT, null, ButtonState.Normal);

        public static bool IconButton(GUIContent content, ButtonState state)
            => IconButton(content, Config.ICON_BUTTON_WIDTH, Config.ICON_BUTTON_HEIGHT, null, state);

        public static bool IconButton(GUIContent content, GUIStyle style)
            => IconButton(content, Config.ICON_BUTTON_WIDTH, Config.ICON_BUTTON_HEIGHT, style, ButtonState.Normal);

        public static bool IconButton(GUIContent content, float width, float height)
            => IconButton(content, width, height, null, ButtonState.Normal);

        public static bool IconButton(GUIContent content, float width, float height, ButtonState state)
            => IconButton(content, width, height, null, state);

        public static bool IconButton(GUIContent content, float width, float height, GUIStyle style)
            => IconButton(content, width, height, style, ButtonState.Normal);

        public static bool IconButton(GUIContent content, float width, float height, GUIStyle style, ButtonState state)
        {
            if (style == null)
                style = s_IconButtonStyle;

            ApplyButtonState(state);

            bool res = GUILayout.Button(content, style, NewLayoutOptions(width, height).Build());

            RollbackButtonState();

            return state == ButtonState.Locked ? false : res;
        }
    }
}
