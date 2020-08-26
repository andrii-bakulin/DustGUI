# DustGUI

DustGUI is library for simplify usage of Unity GUI elements from `GUI`, `GUILayout` and `EditorGUILayout` classes.

[![Codacy Badge](https://app.codacy.com/project/badge/Grade/80ec7878cd8d4a6c91244470fe5a1435)](https://www.codacy.com/manual/andrii-bakulin/DustGUI?utm_source=github.com&amp;utm_medium=referral&amp;utm_content=andrii-bakulin/DustGUI&amp;utm_campaign=Badge_Grade)

## New GUI elements

DustGUI provides a few new components:

- `Slider` - slider for float variable which supports out-of-range values
- `IntSlider` - slider for int variable which supports out-of-range values


### Slider & IntSlider

Slider & IntSlider allow control values outside of main range. But you still can define min and max limits for values.

```C#
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
        fProperty = serializedObject.FindProperty("SomeFloatVar");
    }

    public override void OnInspectorGUI()
    {
        // Creates slider with slider range between [0f .. 25f] with click-step 0.5f 
        // and global limits for value in [-100f .. +100f]
        fValue = DustGUI.Slider.Create(0f, 25f, 0.5f, -100f, 100f).LinkEditor(this).Draw("Var Label", fValue);
        
        // You can create one instance of class and use it to draw few same sliders
        var slider = new DustGUI.IntSlider(0, 100, 2).LinkEditor(this);
        slider.SetLimits(-1000, +1000);
        iValue0 = slider.Draw(iValue0); // full width slider
        iValue1 = slider.Draw("Int #1", iValue1); 
        iValue2 = slider.Draw(new GUIContent("Int #2", "With tooltip"), iValue2); 
        
        // Also, you can use SerializedProperty object
        // Creeate01 will set slider range between [0f..1f] with step 0.01f and limits [0f..1f]
        DustGUI.Slider.Create01().LinkEditor(this).Draw("Range 0..1", fProperty);
    }
}
```

PS: Linking of editor `.LinkEditor(this).` is optional, but values will be updated more friendly when you'll change them by dragging starting with click on the title of the object.



## Bug Reporting and Feature Requests

Please add as many details as possible regarding submission of issues and feature requests

## License

The MIT License (MIT)

Copyright (c) 2020 Andrii Bakulin

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
