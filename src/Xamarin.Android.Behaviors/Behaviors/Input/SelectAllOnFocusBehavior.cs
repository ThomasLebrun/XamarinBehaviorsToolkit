using Android.Widget;

namespace Xamarin.Android.Behaviors.Input
{
    /// <summary>
    /// Behavior used on an EditText object and used to select all the text within it when it has focus
    /// </summary>
    public class SelectAllOnFocusBehavior : Behavior<EditText>
    {
        /// <summary>
        /// Method to override when the behavior is attached to the view.
        /// </summary>
        protected override void OnAttached()
        {
            this.AssociatedObject.SetSelectAllOnFocus(true);
        }
    }
}