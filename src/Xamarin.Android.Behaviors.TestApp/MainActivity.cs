using System.Windows.Input;
using Android.App;
using Android.Widget;
using Android.OS;
using Xamarin.Android.Behaviors.Command;
using Xamarin.Android.Behaviors.Input;
using Xamarin.Android.Behaviors.TestApp.Helpers;

namespace Xamarin.Android.Behaviors.TestApp
{
    [Activity(
        Label = "Xamarin Android Behaviors",
        MainLauncher = true, 
        Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        public ICommand TestCommand { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

            this.TestCommand = new RelayCommand(() =>
            {
                System.Diagnostics.Debug.WriteLine("In TestCommandExexute!");
            }, () => true);

            // Custom behavior
            var button = FindViewById<Button>(Resource.Id.MyButton);
            button.AttachBehavior(new IncrementCounterBehavior());

            // Built-in behaviors
            var editText = FindViewById<EditText>(Resource.Id.MyEditText);
            editText.AttachBehavior(new HideKeyboardOnEnterKeyBehavior { ApplicationContext = this.ApplicationContext } );

            var mySecondEditText = FindViewById<EditText>(Resource.Id.MySecondEditText);
            mySecondEditText.AttachBehavior(new SelectAllOnFocusBehavior());

            var secondButton = FindViewById<Button>(Resource.Id.MySecondButton);
            secondButton.AttachBehavior(new EventToCommandBehavior("Click", this.TestCommand));
        }
    }
}

