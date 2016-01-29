# **Xamarin Behaviors Toolkit**
Xamarin Behaviors Toolkit is an easy-to-use means of adding common and reusable interactivity to your Xamarin applications with minimal code. Use of Xamarin Behaviors Toolkit is governed by the MIT License.

## Build Status
[![Build status](https://ci.appveyor.com/api/projects/status/goi8xb7yjvdvbcdk?svg=true)](https://ci.appveyor.com/project/ThomasLebrun/xamarinbehaviorstoolkit)

## Getting Started
As a primary Windows (XAML) developer, I've always enjoyed using the behaviors to easily add, with just some lines of code, some great features to any controls.
Since I work with Xamarin, I've always thought  it could be a good idea to get this functionality in the Xamarin projects.

So welcome to the home of the **Xamarin Behaviors Toolkit**, a custom implementation of the XAML behaviors for the Xamarin (Android / iOS) projects!

## Code Example
 **Using a behavior**
 
 To attach a behavior to a control (Button, EditText, etc.), you just have to use the **AttachBehavior** method.

 Take a look at the following code sample showing how to use a behavior in an application, just with a snippet of code:
 ```cs
var mySecondEditText = FindViewById<EditText>(Resource.Id.MySecondEditText);
mySecondEditText.AttachBehavior(new SelectAllOnFocusBehavior());
 ```

**Creating a behavior**

Xamarin Behavior Toolkit implement the same architecture as the Microsoft behaviors which can be used in any XAML application. To create your own behavior, you just have to inherit from the **Behavior<T>** class and override the methods **OnAttached** and/or **OnDetaching**. To reference the control on which the behavior is attached, you can access the property **AssociatedObject**:
```cs
public class SelectAllOnFocusBehavior : Behavior<EditText>
{
    private EventHandler<View.FocusChangeEventArgs> _focusChangeEventHandler;
    
    /// <summary>
    /// Method to override when the behavior is attached to the view.
    /// </summary>
    public override void OnAttached()
    {
        _focusChangeEventHandler = (sender, args) =>
        {
            if (args.HasFocus)
            {
                this.AssociatedObject.SelectAll();
            }
        };

        this.AssociatedObject.FocusChange += _focusChangeEventHandler;
    }

    /// <summary>
    /// Method to override when the behavior is removed from the view.
    /// </summary>
    public override void OnDetaching()
    {
        if (_focusChangeEventHandler != null)
        {
            this.AssociatedObject.FocusChange -= _focusChangeEventHandler;
        }
    }
}
 ```

## Todo
 - Implement behaviors architecture for Xamarin.iOS projects
 - Add built-in behaviors for Xamarin.iOS projects
 - Add more built-in behaviors for Xamarin.Android projects

## More Info
 - Want to contribute? We accept any interesting pull requests!
 - [Report a bug or ask a question](https://github.com/ThomasLebrun/XamarinBehaviorsToolkit/issues)
 - [License](http://opensource.org/licenses/MIT)

