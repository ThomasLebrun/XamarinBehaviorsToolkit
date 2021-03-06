using UIKit;

namespace iOS.Behaviors
{
    /// <summary>
    /// Base class for all the behaviors.
    /// </summary>
    /// <typeparam name="T">Type of the associatedObject on which the behavior will be attached.</typeparam>
    public abstract class Behavior<T> where T : UIView
    {
        /// <summary>
        /// Gets the object associated to the behavior.
        /// </summary>
        /// <value>
        /// The associated object.
        /// </value>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        protected T AssociatedObject { get; private set; }

        /// <summary>
        /// Attaches the behavior to the specified associatedObject.
        /// </summary>
        /// <param name="associatedObject">The associatedObject.</param>
        internal void Attach(T associatedObject)
        {
            this.AssociatedObject = associatedObject;

            this.OnAttached();
        }

        /// <summary>
        /// Removes the behavior from the associatedObject.
        /// </summary>
        internal void Remove()
        {
            this.OnDetaching();

            this.AssociatedObject = null;
        }

        /// <summary>
        /// Method to override when the behavior is attached to the associatedObject.
        /// </summary>
        protected virtual void OnAttached()
        {
        }

        /// <summary>
        /// Method to override when the behavior is removed from the associatedObject.
        /// </summary>
        protected virtual void OnDetaching()
        {
        }
    }
}
