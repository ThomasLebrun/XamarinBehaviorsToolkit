using System;
using Android.Widget;

namespace Xamarin.Android.Behaviors.TestApp
{
    /// <summary>
    /// Sample behavior used to increment a counter and display its value on the Text property of a button.
    /// </summary>
    public class IncrementCounterBehavior : Behavior<Button>
    {
        private int _counter;
        private EventHandler _clickEventHandler;

        protected override void OnAttached()
        {
            this.AssociatedObject.Text = _counter.ToString();

            _clickEventHandler += (sender, args) =>
            {
                _counter++;

                this.AssociatedObject.Text = _counter.ToString();
            };

            this.AssociatedObject.Click += _clickEventHandler;
        }

        protected override void OnDetaching()
        {
            if (_clickEventHandler != null)
            {
                this.AssociatedObject.Click -= _clickEventHandler;
            }
        }
    }
}