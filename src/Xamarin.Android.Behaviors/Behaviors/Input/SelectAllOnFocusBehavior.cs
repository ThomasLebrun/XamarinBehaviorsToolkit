using System;
using Android.Views;
using Android.Widget;

namespace Xamarin.Android.Behaviors.Input
{
    /// <summary>
    /// Behavior used on an EditText object and used to select all the text within it when it has focus
    /// </summary>
    public class SelectAllOnFocusBehavior : Behavior<EditText>
    {
        private EventHandler<View.FocusChangeEventArgs> _focusChangeEventHandler;
        /// <summary>
        /// Method to override when the behavior is attached to the view.
        /// </summary>
        protected override void OnAttached()
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
        protected override void OnDetaching()
        {
            if (_focusChangeEventHandler != null)
            {
                this.AssociatedObject.FocusChange -= _focusChangeEventHandler;
            }
        }
    }
}