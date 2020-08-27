# DustGUI

DustGUI is library for simplify usage of Unity GUI elements from `GUI`, `GUILayout` and `EditorGUILayout` classes.

[![Codacy Badge](https://app.codacy.com/project/badge/Grade/80ec7878cd8d4a6c91244470fe5a1435)](https://www.codacy.com/manual/andrii-bakulin/DustGUI?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=andrii-bakulin/DustGUI&amp;utm_campaign=Badge_Grade)

![Unity 2020.1 - success tested](https://img.shields.io/static/v1?label=Unity%202020.1&message=tested&color=success)
![Unity 2020.1 - success tested](https://img.shields.io/static/v1?label=Unity%202019.4&message=tested&color=success)
![Unity 2020.1 - success tested](https://img.shields.io/static/v1?label=Unity%202018.4&message=tested&color=success)

## New GUI elements

DustGUI provides a few new components:

- `DustGUI.SliderExt` - extended float-slider supporting out-of-slider-range values
- `DustGUI.IntSliderExt` - extended int-slider supporting out-of-slider-range values

## DustGUI.SliderExt & DustGUI.IntSliderExt

`DustGUI.SliderExt` & `DustGUI.IntSliderExt` allow store & control values outside of the slider range. But you still can define min and max limits for values.

![DustGUI.SliderExt](https://github.com/andrii-bakulin/DustGUI/blob/master/Wiki/images/DustGUI-SliderExt-Preview.gif)

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
        fValue = DustGUI.SliderExt.Create(0f, 25f, 0.5f, -100f, 100f).LinkEditor(this).Draw("Var Label", fValue);
        
        // You can create one instance of class and use it to draw few same sliders
        var slider = new DustGUI.IntSliderExt(0, 100, 2).LinkEditor(this);
        slider.SetLimits(-1000, +1000);
        iValue0 = slider.Draw(iValue0); // full width slider
        iValue1 = slider.Draw("Int #1", iValue1); 
        iValue2 = slider.Draw(new GUIContent("Int #2", "With tooltip"), iValue2); 
        
        // Also, you can use SerializedProperty object
        // Creeate01 will set slider range between [0f..1f] with step 0.01f and limits [0f..1f]
        DustGUI.SliderExt.Create01().LinkEditor(this).Draw("Range 0..1", fProperty);
    }
}
```

PS: Linking of editor `LinkEditor(this)` is optional, but if you link editor then values will be updated more friendly when you'll change them by dragging starting with click on the title of the object.

## Bug Reporting and Feature Requests

Please add as many details as possible regarding submission of issues and feature requests

## License

The MIT License (MIT)

Copyright (c) 2020 Andrii Bakulin

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
