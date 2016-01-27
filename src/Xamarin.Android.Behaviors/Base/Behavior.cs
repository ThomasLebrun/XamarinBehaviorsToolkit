using Android.Views;

namespace Xamarin.Android.Behaviors
{
    /// <summary>
    /// Base class for all the behaviors.
    /// </summary>
    /// <typeparam name="T">Type of the view on which the behavior will be attached.</typeparam>
    public abstract class Behavior<T> where T : View
    {
        /// <summary>
        /// Gets or sets the associated object.
        /// </summary>
        /// <value>
        /// The associated object.
        /// </value>
        public T AssociatedObject { get; private set; }

        /// <summary>
        /// Attaches the behavior to the specified view.
        /// </summary>
        /// <param name="view">The view.</param>
        internal void Attach(T view)
        {
            this.AssociatedObject = view;

            this.OnAttached();
        }

        /// <summary>
        /// Removes the behavior from the specified view.
        /// </summary>
        /// <param name="view">The view.</param>
        internal void Remove(T view)
        {
            this.AssociatedObject = view;

            this.OnDetaching();
        }

        /// <summary>
        /// Method to override when the behavior is attached to the view.
        /// </summary>
        public virtual void OnAttached()
        {
        }

        /// <summary>
        /// Method to override when the behavior is removed from the view.
        /// </summary>
        public virtual void OnDetaching()
        {
        }
    }
}
