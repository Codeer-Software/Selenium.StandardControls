Selenium.StandardControls
===
Created by Ishikawa-Tatsuya Matsui-Bin  
[![NuGet Version and Downloads count](https://buildstats.info/nuget/Selenium.StandardControls)](https://www.nuget.org/packages/Selenium.StandardControls/)

What is Selenium.StandardControls?
---
The Selenium.StandardControls, wraps the Seleinum in C#, test libraries to handle the standard controls of html in a simple manner.

Sample Code
---
```cs  
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Firefox;

namespace Test
{
    [TestClass]
    public class TestControls
    {
        FirefoxDriver _driver;
        Page_Controls _page;

        [TestInitialize]
        public void TestInitialize()
        {
            _driver = new FirefoxDriver();
            _page = Page_Controls.Open(_driver);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _driver.Dispose();
        }

        [TestMethod]
        public void TextBox()
        {
            _page.TextBox.Edit("abc");
            _page.TextBox.Text.Is("abc");
        }
    }
}
```
Corresponding Control
---
Click and edit , such as the acquisition of the text is possible
- Anchor
```cs 
Anchor.Text.Is("codeer");
Anchor.Invoke();
```
- Button
```cs 
Button.Invoke();
```
- CheckBox
```cs 
CheckBox.Edit(true);
CheckBox.Checked.IsTrue();
CheckBox.Edit(false);
CheckBox.Checked.IsFalse();
```
- DropDownList
```cs 
DropDown.Edit("Apple");
DropDown.SelectedIndex.Is(0);
DropDown.Edit("Orange");
DropDown.SelectedIndex.Is(1);
DropDown.Edit(3);
DropDown.SelectedIndex.Is(3);
DropDown.Text.Is("Pinapple");
```
- Label
```cs 
Label.Text.Is("Title Controls");
```
- RadioButton
```cs 
RadioButton.Checked.IsTrue();
RadioButton.Edit();
RadioButton.Checked.IsFalse();
```
- TextArea
```cs 
TextArea.Edit("abc");
TextArea.Text.Is("abc");
```
- TextBox
```cs 
TextBox.Edit("abc");
TextBox.Text.Is("abc");
TextBox.Show();
TextBox.Focus();
TextBox.Blur();
```

About Wait
---
The following controls there is a Action, which says Wait.
- AnchorDriver
- ButtonDriver
- CheckBoxDriver
- DropDownListDriver
- RadioButtonDriver
- TextBoxDriver

This is used in case that does not work properly and not listening to the post- editing of each control . Since the part that depends on the screen , there is no single answer , such as may be wait unconditionally 5 seconds . Where it has been making that can last in the Action to set the editing of each control .

Example: After editing TextBox, wait until you see again TextBox
```cs 
TextBox.Wait = () =>
{
    while (true)
    {
        try
        {
        　　TextBox.Show();
            break;
        }
        catch { }
        Thread.Sleep(100);
    }
};
TextBox.Edit("abc");
//Waiting for the Show
TextBox.Text.Is("abc");
```

Author Info
---
Ishikawa-Tatsuya & Matsui-Bin is a software developer in Japan at Codeer, Inc.  
Ishikawa-Tatsuya & Matsui-Bin is awarding Microsoft MVP.

License
---
This library is under the Apache License.
