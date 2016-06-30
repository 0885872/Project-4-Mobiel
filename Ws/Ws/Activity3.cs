using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Gms.Maps;

namespace Ws
{
  [Activity(Label = "Bekijk Locatie")]
  public class Activity3 : Activity, IOnMapReadyCallback
  {
    private GoogleMap mMap;
    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);

      // Create your application here
      SetContentView(Resource.Layout.layout3);

      SetUpMap();
    }

    private void SetUpMap()
    {
      if (mMap == null)
      {
        FragmentManager.FindFragmentById<MapFragment>(Resource.Id.map).GetMapAsync(this);
      }
    }

    public void OnMapReady(GoogleMap googleMap)
    {
      mMap = googleMap;
    }
  }
}