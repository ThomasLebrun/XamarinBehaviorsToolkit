using UIKit;

namespace iOS.Behaviors.Input
{
    /// <summary>
    /// Behavior used on an EditText object and used to hide the software keyboard when one of the following key is pressed: "Done", "Search" or "Go"
    /// </summary>
    public class HideKeyboardOnEnterKeyBehavior : Behavior<UITextField>
    {
        private UITextFieldCondition _hideKeyboardCondition;

        /// <summary>
        /// Method to override when the behavior is attached to the view.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">ApplicationContext property needs to be set in order to use this behavior.</exception>
        protected override void OnAttached()
        {
            _hideKeyboardCondition = sender =>
            {
                sender.ResignFirstResponder();

                return false;
            };

            this.AssociatedObject.ShouldReturn = _hideKeyboardCondition;
        }
    }
}