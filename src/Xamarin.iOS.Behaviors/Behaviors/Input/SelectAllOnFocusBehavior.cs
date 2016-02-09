using System;
using ObjCRuntime;
using UIKit;

namespace iOS.Behaviors.Input
{
    /// <summary>
    /// Behavior used on an EditText object and used to select all the text within it when it has focus
    /// </summary>
    public class SelectAllOnFocusBehavior : Behavior<UITextField>
    {
        private EventHandler _editingDidBeginEventHandler;

        /// <summary>
        /// Method to override when the behavior is attached to the view.
        /// </summary>
        protected override void OnAttached()
        {
            _editingDidBeginEventHandler = (sender, e) =>
            {
                this.AssociatedObject.PerformSelector(new Selector("selectAll"), null, 0.0f);
            };

            this.AssociatedObject.EditingDidBegin += _editingDidBeginEventHandler;
        }

        /// <summary>
        /// Method to override when the behavior is removed from the associatedObject.
        /// </summary>
        protected override void OnDetaching()
        {
            if (_editingDidBeginEventHandler != null)
            {
                this.AssociatedObject.EditingDidBegin -= _editingDidBeginEventHandler;
            }
        }
    }
}