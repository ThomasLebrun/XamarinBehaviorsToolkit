using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Widget;

namespace Xamarin.Android.Behaviors.Views
{
    public class BorderColoredEditTextBehavior : Behavior<EditText>
    {
        /// <summary>
        /// Gets or sets the color to use to draw the border.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Gets or sets the width of the stroke to use to draw the border.
        /// </summary>
        public float StrokeWidth { get; set; }

        /// <summary>
        /// Method to override when the behavior is attached to the view.
        /// </summary>
        protected override void OnAttached()
        {
            var shape = new ShapeDrawable();
            shape.Paint.Color = this.Color;
            shape.Paint.StrokeWidth = this.StrokeWidth;
            
            shape.Paint.SetStyle(Paint.Style.Stroke);

            this.AssociatedObject.SetBackgroundDrawable(shape);
        }
    }
}