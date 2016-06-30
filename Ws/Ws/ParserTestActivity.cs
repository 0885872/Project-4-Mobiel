using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Widget;
using Android.Content.Res;
using Android;

namespace Ws
{
    [Activity(Label = "ParserTestActivity")]
    public class ParserTestActivity : ListActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            AssetManager assets = this.Assets;
            CSVParser testParser = new CSVParser(assets);
            List<List<string>> csvList = testParser.readCSV("fietsdiefstal.csv");
            this.ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, csvList[0]);
        }
    }
}