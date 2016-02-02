using System;
using Android.Content;
using Android.Views.InputMethods;
using Android.Widget;

namespace Android.Behaviors.Input
{
    /// <summary>
    /// Behavior used on an EditText object and used to hide the software keyboard when one of the following key is pressed: "Done", "Search" or "Go"
    /// </summary>
    public class HideKeyboardOnEnterKeyBehavior : Behavior<EditText>
    {
        /// <summary>
        /// Gets or sets the application context.
        /// </summary>
        /// <value>
        /// The application context.
        /// </value>
        public Context ApplicationContext { get; set; }

        private EventHandler<TextView.EditorActionEventArgs> _editorActionEventHandler;

        /// <summary>
        /// Method to override when the behavior is attached to the view.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">ApplicationContext property needs to be set in order to use this behavior.</exception>
        protected override void OnAttached()
        {
            if (this.ApplicationContext == null)
            {
                throw new InvalidOperationException("ApplicationContext property needs to be set in order to use this behavior.");
            }

            _editorActionEventHandler = (sender, args) =>
            {
                if (args.ActionId == ImeAction.Done || args.ActionId == ImeAction.Search || args.ActionId == ImeAction.Go || args.ActionId == ImeAction.Next)
                {
                    var inputManager = (InputMethodManager)this.ApplicationContext.GetSystemService(Context.InputMethodService);
                    inputManager.HideSoftInputFromWindow(this.AssociatedObject.WindowToken, HideSoftInputFlags.None);

                    args.Handled = true;
                }
            };

            this.AssociatedObject.EditorAction += _editorActionEventHandler;
        }

        /// <summary>
        /// Method to override when the behavior is removed from the view.
        /// </summary>
        protected override void OnDetaching()
        {
            if (_editorActionEventHandler != null)
            {
                this.AssociatedObject.EditorAction -= _editorActionEventHandler;
            }
        }
    }
}