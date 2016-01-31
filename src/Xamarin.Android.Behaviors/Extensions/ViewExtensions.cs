using System;
using Android.Views;

namespace Xamarin.Android.Behaviors
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
        public static void AttachBehavior<T>(this T view, Behavior<T> behavior) where T : View
        {
            EventHandler<View.ViewAttachedToWindowEventArgs> viewAttachedToWindow = null;
            viewAttachedToWindow = (sender, args) =>
            {
                view.ViewAttachedToWindow -= viewAttachedToWindow;

                behavior.Attach(view);
            };

            view.ViewAttachedToWindow += viewAttachedToWindow;

            EventHandler<View.ViewDetachedFromWindowEventArgs> viewDetachedFromWindow = null;
            viewDetachedFromWindow = (sender, args) =>
            {
                view.ViewDetachedFromWindow -= viewDetachedFromWindow;

                behavior.Remove(view);
            };

            view.ViewDetachedFromWindow += viewDetachedFromWindow;
        }

        /// <summary>
        /// Attaches the behaviors to the view.
        /// </summary>
        /// <typeparam name="T">Type of the view on which the behavior will be attached.</typeparam>
        /// <param name="view">The view.</param>
        /// <param name="behaviors">The behaviors to add to the view.</param>
        public static void AttachBehaviors<T>(this T view, params Behavior<T>[] behaviors) where T : View
        {
            EventHandler<View.ViewAttachedToWindowEventArgs> viewAttachedToWindow = null;
            viewAttachedToWindow = (sender, args) =>
            {
                view.ViewAttachedToWindow -= viewAttachedToWindow;

                foreach (var behavior in behaviors)
                {
                    behavior.Attach(view);
                }
            };

            view.ViewAttachedToWindow += viewAttachedToWindow;

            EventHandler<View.ViewDetachedFromWindowEventArgs> viewDetachedFromWindow = null;
            viewDetachedFromWindow = (sender, args) =>
            {
                view.ViewDetachedFromWindow -= viewDetachedFromWindow;

                foreach (var behavior in behaviors)
                {
                    behavior.Remove(view);
                }
            };

            view.ViewDetachedFromWindow += viewDetachedFromWindow;
        }

        /// <summary>
        /// Removes the behavior from the view.
        /// </summary>
        /// <typeparam name="T">Type of the view on which the behavior will be removed.</typeparam>
        /// <param name="view">The view.</param>
        /// <param name="behavior">The behavior.</param>
        public static void RemoveBehavior<T>(this T view, Behavior<T> behavior) where T : View
        {
            behavior.Remove(view);
        }

        /// <summary>
        /// Removes the behaviors from the view.
        /// </summary>
        /// <typeparam name="T">Type of the view on which the behavior will be removed.</typeparam>
        /// <param name="view">The view.</param>
        /// <param name="behaviors">The behavior to remove from the view.</param>
        public static void RemoveBehaviors<T>(this T view, params Behavior<T>[] behaviors) where T : View
        {
            foreach (var behavior in behaviors)
            {
                behavior.Remove(view);
            }
        }
    }
}