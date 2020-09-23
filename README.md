# DustGUI

DustGUI is library for simplify usage of Unity GUI elements from `GUI`, `GUILayout` and `EditorGUILayout` classes.

[![Codacy Badge](https://app.codacy.com/project/badge/Grade/80ec7878cd8d4a6c91244470fe5a1435)](https://www.codacy.com/manual/andrii-bakulin/DustGUI?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=andrii-bakulin/DustGUI&amp;utm_campaign=Badge_Grade)

![Unity 2020.1 - success tested](https://img.shields.io/static/v1?label=Unity%202020.1&message=tested&color=success)
![Unity 2020.1 - success tested](https://img.shields.io/static/v1?label=Unity%202019.4&message=tested&color=success)
![Unity 2020.1 - success tested](https://img.shields.io/static/v1?label=Unity%202018.4&message=tested&color=success)

## New GUI elements

DustGUI provides a few new components:

- [`DustGUI.ExtraSlider`](#dustguiextraslider--dustguiextraintslider) - extended float-slider supporting out-of-slider-range values
- [`DustGUI.ExtraIntSlider`](#dustguiextraslider--dustguiextraintslider) - extended int-slider supporting out-of-slider-range values

Builders

- [`GUIStyle Builder`](#guistyle-builder)
- [`GUILayoutOption Builder`](#guilayoutoption-builder)

Shortcuts for standard elements:

- [`DustGUI.Basic`](#dustguibasic)
- [`DustGUI.Block`](#dustguiblock)
- [`DustGUI.Button`](#dustguibutton)
- [`DustGUI.DropDownList`](#dustguidropdownlist)
- [`DustGUI.Field`](#dustguifield)
- [`DustGUI.Media`](#dustguimedia)
- [`DustGUI.Slider`](#dustguislider)
- [`DustGUI.Toolbar`](#dustguitoolbar)

## DustGUI.ExtraSlider & DustGUI.ExtraIntSlider

`DustGUI.ExtraSlider` & `DustGUI.ExtraIntSlider` allow store & control values outside of the slider range. But you still can define min and max limits for values.

![DustGUI.ExtraSlider](https://github.com/andrii-bakulin/DustGUI/blob/master/Wiki/images/DustGUI-ExtraSlider-Preview.gif)

How to use:

```C#
public class DemoScript : MonoBehaviour
{
    public float someFloatVar = 1f;
}

[CustomEditor(typeof(DemoScript))]
public class DemoScriptGUI : Editor
{
    private float fValue;
    
    private int iValue0;
    private int iValue1;
    private int iValue2;
    
    private SerializedProperty fProperty;
    
    public void OnEnable()
    {
        fProperty = serializedObject.FindProperty("someFloatVar");
    }

    public override void OnInspectorGUI()
    {
        // Creates slider with slider range between [0f .. 25f] with click-step 0.5f 
        // and global limits for value in [-100f .. +100f]
        fValue = DustGUI.ExtraSlider.Create(0f, 25f, 0.5f, -100f, 100f).LinkEditor(this).Draw("Var Label", fValue);
        
        // You can create one instance of class and use it to draw few same sliders
        var slider = new DustGUI.ExtraIntSlider(0, 100, 2).LinkEditor(this);
        slider.SetLimits(-1000, +1000);
        iValue0 = slider.Draw(iValue0); // full width slider
        iValue1 = slider.Draw("Int #1", iValue1); 
        iValue2 = slider.Draw(new GUIContent("Int #2", "With tooltip"), iValue2); 
        
        // Also, you can use SerializedProperty object
        // Creeate01 will set slider range between [0f..1f] with step 0.01f and limits [0f..1f]
        DustGUI.ExtraSlider.Create01().LinkEditor(this).Draw("Range 0..1", fProperty);
    }
}
```

PS: Linking of editor `LinkEditor(this)` is optional, but if you link editor then values will be updated more friendly when you'll change them by dragging starting with click on the title of the object.


## GUIStyle Builder

```C#
// How to create [BUILDER]
(DustGUI.Style) DustGUI.NewStyle();
(DustGUI.Style) DustGUI.NewStyle(GUIStyle other);

// Additional Shortcuts
(DustGUI.Style) DustGUI.NewStyleLabel(); 
(DustGUI.Style) DustGUI.NewStyleButton(); 
(DustGUI.Style) DustGUI.NewStylePopup(); 

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
// [BUILDER] methods

// Padding
[BUILDER].Padding(int global)
[BUILDER].Padding(int horizontal, int vertical)
[BUILDER].Padding(int top, int horizontal, int bottom)
[BUILDER].Padding(int left, int right, int top, int bottom)

[BUILDER].PaddingLeft(int value)
[BUILDER].PaddingRight(int value)
[BUILDER].PaddingTop(int value)
[BUILDER].PaddingBottom(int value)

// Margin
[BUILDER].Margin(int global)
[BUILDER].Margin(int horizontal, int vertical)
[BUILDER].Margin(int top, int horizontal, int bottom)
[BUILDER].Margin(int left, int right, int top, int bottom)

[BUILDER].MarginLeft(int value)
[BUILDER].MarginRight(int value)
[BUILDER].MarginTop(int value)
[BUILDER].MarginBottom(int value)

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

// FontSize
[BUILDER].FontSize(int value)
[BUILDER].FontSizeScaled(float scale)

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

// Alignment
[BUILDER].Alignment(TextAnchor value)

[BUILDER].AlignUpperLeft()
[BUILDER].AlignUpperCenter()
[BUILDER].AlignUpperRight()

[BUILDER].AlignMiddleLeft()
[BUILDER].AlignMiddleCenter()
[BUILDER].AlignMiddleRight()

[BUILDER].AlignLowerLeft()
[BUILDER].AlignLowerCenter()
[BUILDER].AlignLowerRight()

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

// Change TextColor & Background
[BUILDER].Background(Texture2D texture)     // warning! this method set value for all 4 states
[BUILDER].TextColor(Color color)            // warning! this method set value for all 4 states

[BUILDER].NormalBackground(Texture2D texture)
[BUILDER].NormalTextColor(Color color)

[BUILDER].HoverBackground(Texture2D texture)
[BUILDER].HoverTextColor(Color color)

[BUILDER].ActiveBackground(Texture2D texture)
[BUILDER].ActiveTextColor(Color color)

[BUILDER].FocusedBackground(Texture2D texture)
[BUILDER].FocusedTextColor(Color color)

// - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -

// Build GUIStyle
(GUIStyle) [BUILDER].Build();

// Samples
GUIStyle style0 = DustGUI.NewStyle().ExpandWidth().Build();
GUIStyle style1 = DustGUI.NewStyleLabel().AlignMiddleCenter().Build();
GUIStyle style2 = DustGUI.NewStyleButton().Padding(5, 10).Margin(0).Build();
```

## GUILayoutOption Builder

```C#
// How to create [BUILDER]
(DustGUI.LayoutOptions) DustGUI.NewLayoutOptions();
(DustGUI.LayoutOptions) DustGUI.NewLayoutOptions(float width, float height);

// [BUILDER] methods
[BUILDER].Width(float value)      // will be apply only if it greater then 0f
[BUILDER].MinWidth(float value)
[BUILDER].MaxWidth(float value)
[BUILDER].ExpandWidth(bool value)

[BUILDER].Height(float value)     // will be apply only if it greater then 0f
[BUILDER].MinHeight(float value)
[BUILDER].MaxHeight(float value)
[BUILDER].ExpandHeight(bool value)

[BUILDER].WidthAndHeight(float width, float height)

(GUILayoutOption[]) [BUILDER].Build();

// Samples
GUILayoutOption[] options0 = DustGUI.NewLayoutOptions(300, 50).ExpandWidth().Build();
GUILayoutOption[] options1 = DustGUI.NewLayoutOptions().Width(100).MaxWidth(300).Build();
```

## DustGUI.Basic

```C#
// Header
DustGUI.Header(string title);
DustGUI.Header(string title, float width);
DustGUI.Header(string title, float width, float height);

// Label
DustGUI.Label(string title);
DustGUI.Label(string title, Color color);
DustGUI.Label(string title, GUIStyle style);
DustGUI.Label(string title, float width);
DustGUI.Label(string title, float width, float height);
DustGUI.Label(string title, float width, float height, Color color);
DustGUI.Label(string title, float width, float height, GUIStyle style);

// SimpleLabel
DustGUI.SimpleLabel(string title);
DustGUI.SimpleLabel(string title, Color color);
DustGUI.SimpleLabel(string title, GUIStyle style);
DustGUI.SimpleLabel(string title, float width);
DustGUI.SimpleLabel(string title, float width, float height);
DustGUI.SimpleLabel(string title, float width, float height, Color color);
DustGUI.SimpleLabel(string title, float width, float height, GUIStyle style);

DustGUI.SimpleLabel(GUIContent title);
DustGUI.SimpleLabel(GUIContent title, Color color);
DustGUI.SimpleLabel(GUIContent title, GUIStyle style);
DustGUI.SimpleLabel(GUIContent title, float width);
DustGUI.SimpleLabel(GUIContent title, float width, float height);
DustGUI.SimpleLabel(GUIContent title, float width, float height, Color color);
DustGUI.SimpleLabel(GUIContent title, float width, float height, GUIStyle style);

DustGUI.StaticTextField(string label, string message);

// HelpBoxesDustGUI.HelpBoxInfo(string message);
DustGUI.HelpBoxWarning(string message);
DustGUI.HelpBoxError(string message);

// Space
DustGUI.Space();
DustGUI.Space(float width);

DustGUI.SpaceLine();
DustGUI.SpaceLine(float width);

DustGUI.SpaceExpand();
DustGUI.SpaceExpand(float width);

// Lock/Unlock
DustGUI.Lock();
DustGUI.Unlock();

// Indent Level controls
(int) DustGUI.indentLevel
(void) DustGUI.IndentLevelInc();
(void) DusGUI.IndentLevelDec();
(int) DustGUI.IndentLevelReset();
(int) DustGUI.IndentLevelReset(int newValue);

// Helpers
(void) DustGUI.BlurFocusControl();

(void) DustGUI.ForcedRedrawSceneView();
(void) DustGUIRuntime.ForcedRedrawSceneView(); // this method may call outside Editor folder

(bool) DustGUI.IsUndoRedoPerformed();
```

## DustGUI.Block

### Horizontal Block

```C#
(Rect) DustGUI.BeginHorizontal();
(Rect) DustGUI.BeginHorizontal(float width);
(Rect) DustGUI.BeginHorizontal(float width, float height);

(Rect) DustGUI.BeginHorizontal(GUIStyle style);
(Rect) DustGUI.BeginHorizontal(GUIStyle style, float width);
(Rect) DustGUI.BeginHorizontal(GUIStyle style, float width, float height);

(Rect) DustGUI.BeginHorizontalBox();
(Rect) DustGUI.BeginHorizontalBox(float width);
(Rect) DustGUI.BeginHorizontalBox(float width, float height);

(void) DustGUI.EndHorizontal();
```

### Vertical Block

```C#
(Rect) DustGUI.BeginVertical();
(Rect) DustGUI.BeginVertical(float width);
(Rect) DustGUI.BeginVertical(float width, float height);

(Rect) DustGUI.BeginVertical(GUIStyle style);
(Rect) DustGUI.BeginVertical(GUIStyle style, float width);
(Rect) DustGUI.BeginVertical(GUIStyle style, float width, float height);

(Rect) DustGUI.BeginVerticalBox();
(Rect) DustGUI.BeginVerticalBox(float width);
(Rect) DustGUI.BeginVerticalBox(float width, float height);

(void) DustGUI.EndVertical();
```

### Foldout Block

```C#
// foldoutId - foldout open/close state bound to this key
// targetId  - foldout open/close state bound to this object

(bool) DustGUI.FoldoutBegin(string title, string foldoutId);
(bool) DustGUI.FoldoutBegin(string title, string foldoutId, bool defaultState);
(bool) DustGUI.FoldoutBegin(string title, string foldoutId, Object targetId)
(bool) DustGUI.FoldoutBegin(string title, string foldoutId, Object targetId, bool defaultState)

// Foldout always open 
(void) DustGUI.FoldoutBegin(string title);

(void) DustGUI.FoldoutEnd()
```

### ScrollView

```C#
(Vector2) DustGUI.BeginScrollView(Vector2 scrollPosition);
(Vector2) DustGUI.BeginScrollView(Vector2 scrollPosition, float width)
(Vector2) DustGUI.BeginScrollView(Vector2 scrollPosition, float width, float height);

(Vector2) DustGUI.BeginScrollView(Vector2 scrollPosition, GUIStyle style);
(Vector2) DustGUI.BeginScrollView(Vector2 scrollPosition, GUIStyle style, float width);
(Vector2) DustGUI.BeginScrollView(Vector2 scrollPosition, GUIStyle style, float width, float height);

// Return true if scroll position did change
(bool) DustGUI.BeginScrollView(ref Vector2 scrollPosition);
(bool) DustGUI.BeginScrollView(ref Vector2 scrollPosition, float width);
(bool) DustGUI.BeginScrollView(ref Vector2 scrollPosition, float width, float height);

(bool) DustGUI.BeginScrollView(ref Vector2 scrollPosition, GUIStyle style);
(bool) DustGUI.BeginScrollView(ref Vector2 scrollPosition, GUIStyle style, float width);
(bool) DustGUI.BeginScrollView(ref Vector2 scrollPosition, GUIStyle style, float width, float height);

(void) DustGUI.EndScrollView();
```

## DustGUI.Button

```C#
enum ButtonState
{
    Normal,
    Pressed,
    Locked
}

(bool) DustGUI.Button(string label);
(bool) DustGUI.Button(string label, ButtonState state);
(bool) DustGUI.Button(string label, float width, float height);
(bool) DustGUI.Button(string label, float width, float height, ButtonState state);
(bool) DustGUI.Button(string label, float width, float height, GUIStyle style, ButtonState state);

(bool) DustGUI.IconButton(string iconName);
(bool) DustGUI.IconButton(string iconName, ButtonState state);
(bool) DustGUI.IconButton(string iconName, GUIStyle style);
(bool) DustGUI.IconButton(string iconName, float width, float height);
(bool) DustGUI.IconButton(string iconName, float width, float height, ButtonState state);
(bool) DustGUI.IconButton(string iconName, float width, float height, GUIStyle style);
(bool) DustGUI.IconButton(string iconName, float width, float height, GUIStyle style, ButtonState state);

(bool) DustGUI.IconButton(Texture texture);
(bool) DustGUI.IconButton(Texture texture, ButtonState state);
(bool) DustGUI.IconButton(Texture texture, GUIStyle style);
(bool) DustGUI.IconButton(Texture texture, float width, float height);
(bool) DustGUI.IconButton(Texture texture, float width, float height, ButtonState state);
(bool) DustGUI.IconButton(Texture texture, float width, float height, GUIStyle style);
(bool) DustGUI.IconButton(Texture texture, float width, float height, GUIStyle style, ButtonState state);

(bool) DustGUI.IconButton(GUIContent content);
(bool) DustGUI.IconButton(GUIContent content, ButtonState state);
(bool) DustGUI.IconButton(GUIContent content, GUIStyle style);
(bool) DustGUI.IconButton(GUIContent content, float width, float height);
(bool) DustGUI.IconButton(GUIContent content, float width, float height, ButtonState state);
(bool) DustGUI.IconButton(GUIContent content, float width, float height, GUIStyle style);
(bool) DustGUI.IconButton(GUIContent content, float width, float height, GUIStyle style, ButtonState state);
```

## DustGUI.DropDownList

```C#
(int) DustGUI.DropDownList(int selectedIndex, string[] displayedOptions);
(int) DustGUI.DropDownList(int selectedIndex, string[] displayedOptions, GUIStyle style);
(int) DustGUI.DropDownList(int selectedIndex, string[] displayedOptions, float width, float height);
(int) DustGUI.DropDownList(int selectedIndex, string[] displayedOptions, float width, float height, GUIStyle style);

(int) DustGUI.DropDownList(string label, int selectedIndex, string[] displayedOptions);
(int) DustGUI.DropDownList(string label, int selectedIndex, string[] displayedOptions, GUIStyle style);
(int) DustGUI.DropDownList(string label, int selectedIndex, string[] displayedOptions, float width, float height);
(int) DustGUI.DropDownList(string label, int selectedIndex, string[] displayedOptions, float width, float height, GUIStyle style);

(Enum) DustGUI.DropDownList(Enum selected);
(Enum) DustGUI.DropDownList(Enum selected, GUIStyle style);
(Enum) DustGUI.DropDownList(Enum selected, float width, float height);
(Enum) DustGUI.DropDownList(Enum selected, float width, float height, GUIStyle style);
```

## DustGUI.Field

```C#
// Bool
(bool) DustGUI.Field(string label, bool value);
(bool) DustGUI.Field(string label, bool value, float width, float height);
(bool) DustGUI.Field(string label, bool value, float width, float height, GUIStyle style);

// Int
(int) DustGUI.Field(string label, int value);
(int) DustGUI.Field(string label, int value, float width, float height);
(int) DustGUI.Field(string label, int value, float width, float height, GUIStyle style);

// Float
(float) DustGUI.Field(string label, float value);
(float) DustGUI.Field(string label, float value, float width, float height);
(float) DustGUI.Field(string label, float value, float width, float height, GUIStyle style);

// String
(string) DustGUI.Field(string label, string value);
(string) DustGUI.Field(string label, string value, float width, float height);
(string) DustGUI.Field(string label, string value, float width, float height, GUIStyle style);

// Vector3
(Vector3) DustGUI.Field(string label, Vector3 value);
(Vector3) DustGUI.Field(string label, Vector3 value, float width, float height);

// Color
(Color) DustGUI.Field(string label, Color value);
(Color) DustGUI.Field(string label, Color value, float width, float height);

// AnimationCurve
(AnimationCurve) DustGUI.Field(string label, AnimationCurve value);
(AnimationCurve) DustGUI.Field(string label, AnimationCurve value, float width, float height);
(AnimationCurve) DustGUI.Field(string label, AnimationCurve value, float width, float height, Color color);
(AnimationCurve) DustGUI.Field(string label, AnimationCurve value, float width, float height, Color color, Rect ranges);

// SerializedProperty
(void) DustGUI.Field(string label, SerializedProperty property);
(void) DustGUI.Field(string label, SerializedProperty property, float width, float height);
(void) DustGUI.Field(GUIContent label, SerializedProperty property);
(void) DustGUI.Field(GUIContent label, SerializedProperty property, float width, float height);
```

## DustGUI.Media

```C#
(void) DustGUI.Image(Rect rect, Texture texture);
```

## DustGUI.Slider

```C#
// float value

(float) DustGUI.Slider(float value, float min, float max)
(float) DustGUI.Slider(float value, float min, float max, float width, float height)

(float) DustGUI.Slider(string label, float value, float min, float max)
(float) DustGUI.Slider(string label, float value, float min, float max, float width, float height)

(float) DustGUI.Slider(GUIContent label, float value, float min, float max)
(float) DustGUI.Slider(GUIContent label, float value, float min, float max, float width, float height)

// SerializedProperty

(bool) DustGUI.Slider(SerializedProperty value, float min, float max)
(bool) DustGUI.Slider(SerializedProperty value, float min, float max, float width, float height)

(bool) DustGUI.Slider(string label, SerializedProperty value, float min, float max)
(bool) DustGUI.Slider(string label, SerializedProperty value, float min, float max, float width, float height)

(bool) DustGUI.Slider(GUIContent label, SerializedProperty value, float min, float max)
(bool) DustGUI.Slider(GUIContent label, SerializedProperty value, float min, float max, float width, float height)

//------------------------------------------------------------------------

// Slider in range [0..1] Return float
(float) DustGUI.SliderOnly01(float value)
(float) DustGUI.SliderOnly01(float value, float width, float height)

// Slider without title
(float) DustGUI.SliderOnly(float value, float min, float max)
(float) DustGUI.SliderOnly(float value, float min, float max, float width, float height)
```

## DustGUI.Toolbar

```C#
// Basic toolbar

(int) DustGUI.Toolbar(int selectedTab, string[] titles);
(int) DustGUI.Toolbar(int selectedTab, string[] titles, GUIStyle style, GUI.ToolbarButtonSize buttonSize, params GUILayoutOption[] options);

// Toolbar save active tab in EditorSession data
// - toolbarId - active tab index bound to this key
// - targetId  - active tab index bound to this object

(int) DustGUI.Toolbar(string toolbarId, string[] titles);
(int) DustGUI.Toolbar(string toolbarId, Object targetId, string[] titles);

(int) DustGUI.Toolbar(string toolbarId, string[] titles, GUIStyle style, GUI.ToolbarButtonSize buttonSize, params GUILayoutOption[] options);
(int) DustGUI.Toolbar(string toolbarId, Object targetId, string[] titles, GUIStyle style, GUI.ToolbarButtonSize buttonSize, params GUILayoutOption[] options);
```

## Bug Reporting and Feature Requests

Please add as many details as possible regarding submission of issues and feature requests

## License

The MIT License (MIT)

Copyright (c) 2020 Andrii Bakulin

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
