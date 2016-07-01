using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Widget;
using Android.Content.Res;
using Android;
using System;

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
            List<Tuple<string, int>> tuples = testParser.sum("fietsdiefstal.csv", "kleur", 20);
            List<List<string>> csvList = testParser.readCSV("fietsdiefstal.csv");
            List<string> listedTuple = new List<string>();
            foreach (Tuple<string, int> tuple in tuples)
            {
                listedTuple.Add(tuple.Item1 + " - " + tuple.Item2.ToString());
            }

            this.ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, listedTuple);
        }
    }
}