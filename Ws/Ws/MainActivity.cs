using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Ws
{
  [Activity(Label = "Fiets Trommel", MainLauncher = true, Icon = "@drawable/icon")]
  public class MainActivity : Activity
  {
    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      // Set our view from the "main" layout resource
      SetContentView(Resource.Layout.Main);

      // Get our button from the layout resource,
      // and attach an event to it
      Button button = FindViewById<Button>(Resource.Id.InformatieButton);
      button.Click += delegate
      {
        StartActivity(typeof(Activity1));
      };

      Button button2 = FindViewById<Button>(Resource.Id.OpslaanLocatieButton);
      button2.Click += delegate
      {
        StartActivity(typeof(Activity2));
      };

      Button button3 = FindViewById<Button>(Resource.Id.BekijkLocatieButton);
      button3.Click += delegate
      {
        StartActivity(typeof(Activity3));
      };
    }
  }
}