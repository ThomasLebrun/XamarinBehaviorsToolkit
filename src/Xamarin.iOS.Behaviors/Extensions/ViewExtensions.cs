using UIKit;

namespace iOS.Behaviors
{
    /// <summary>
    /// Extension method for the View object, allowing the behavior to be attached and removed.
    /// </summary>
    public static class ViewExtensions
    {
        /// <summary>
        /// Attaches the behavior to the view.
        /// </summary>
        /// <typeparam name="T">Type of the view on which the behavior will be attached.</typeparam>
        /// <param name="view">The view.</param>
        /// <param name="behavior">The behavior to add to the view.</param>
        public static void AttachBehavior<T>(this T view, Behavior<T> behavior) where T : UIView
        {
            behavior.Attach(view);
        }

        /// <summary>
        /// Attaches the behaviors to the view.
        /// </summary>
        /// <typeparam name="T">Type of the view on which the behavior will be attached.</typeparam>
        /// <param name="view">The view.</param>
        /// <param name="behaviors">The behaviors to add to the view.</param>
        public static void AttachBehaviors<T>(this T view, params Behavior<T>[] behaviors) where T : UIView
        {
            foreach (var behavior in behaviors)
            {
                behavior.Attach(view);
            }
        }

        /// <summary>
        /// Removes the behavior from the view.
        /// </summary>
        /// <typeparam name="T">Type of the view on which the behavior will be removed.</typeparam>
        /// <param name="view">The view.</param>
        /// <param name="behavior">The behavior.</param>
        public static void RemoveBehavior<T>(this T view, Behavior<T> behavior) where T : UIView
        {
            behavior.Remove();
        }

        /// <summary>
        /// Removes the behaviors from the view.
        /// </summary>
        /// <typeparam name="T">Type of the view on which the behavior will be removed.</typeparam>
        /// <param name="view">The view.</param>
        /// <param name="behaviors">The behavior to remove from the view.</param>
        public static void RemoveBehaviors<T>(this T view, params Behavior<T>[] behaviors) where T : UIView
        {
            foreach (var behavior in behaviors)
            {
                behavior.Remove();
            }
        }
    }
}