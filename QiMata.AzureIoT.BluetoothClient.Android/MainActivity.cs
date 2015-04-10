using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace QiMata.AzureIoT.BluetoothClient.Android
{
    [Activity(Label = "QiMata IoT Bluetooth Client", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Button _startButton;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            _startButton = FindViewById<Button>(Resource.Id.StartButton);

            _startButton.Click += delegate(object sender, EventArgs e) { SetupBackgroundService(); };
        }

        private void SetupBackgroundService()
        {
            this.StartService(new Intent(this, typeof(EventHubService)));
        }
    }
}

