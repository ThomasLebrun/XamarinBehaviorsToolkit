using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Widget;

namespace Android.Behaviors.Views
{
    public class RoundCornerEditTextBehavior : Behavior<EditText>
    {
        /// <summary>
        /// Gets or sets the color to use to draw the back of the EditText.
        /// </summary>
        public Color BackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the corner radius of the EditText.
        /// </summary>
        public float CornerRadius { get; set; }

        protected override void OnAttached()
        {
            var shape = new GradientDrawable();
            shape.SetCornerRadius(this.CornerRadius);
            shape.SetColor(this.BackgroundColor);


            this.AssociatedObject.SetBackgroundDrawable(shape);
        }
    }
}