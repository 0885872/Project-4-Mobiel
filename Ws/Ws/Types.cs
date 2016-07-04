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
using OxyPlot.Xamarin.Android;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Annotations;

namespace Types
{
    public interface GraphType
    {
        PlotModel Visit(Func<LinearAxis, LinearAxis, List<DataPoint>, PlotModel> onLineType, Func<List<PieSlice>, PlotModel> onPieType, Func<CategoryAxis, LinearAxis, List<BarItem>, List<string>, PlotModel> onBarType);
    }

    public class LineType : GraphType
    {
        private LinearAxis xAxis;
        private LinearAxis yAxis;
        private List<DataPoint> dataPoints;

        public LineType(LinearAxis x, LinearAxis y, List<DataPoint> dp)
        {
            xAxis = x;
            yAxis = y;
            dataPoints = dp;
        }

        public PlotModel Visit(Func<LinearAxis, LinearAxis, List<DataPoint>, PlotModel> onLineType, Func<List<PieSlice>, PlotModel> onPieType, Func<CategoryAxis, LinearAxis, List<BarItem>, List<string>, PlotModel> onBarType)
        {
            return onLineType(xAxis, yAxis, dataPoints);
        }
    }

    public class PieType : GraphType
    {
        private List<PieSlice> slices;

        public PieType(List<PieSlice> s)
        {
            slices = s;
        }

        public PlotModel Visit(Func<LinearAxis, LinearAxis, List<DataPoint>, PlotModel> onLineType, Func<List<PieSlice>, PlotModel> onPieType, Func<CategoryAxis, LinearAxis, List<BarItem>, List<string>, PlotModel> onBarType)
        {
            return onPieType(slices);
        }
    }

    public class BarType : GraphType
    {
        private CategoryAxis categoryAxis;
        private LinearAxis valueAxis;
        private List<BarItem> barItems;
        private List<string> labels;

        public BarType(CategoryAxis ca, LinearAxis va, List<BarItem> bi, List<string> ls)
        {
            categoryAxis = ca;
            valueAxis = va;
            barItems = bi;
            labels = ls;
        }

        public PlotModel Visit(Func<LinearAxis, LinearAxis, List<DataPoint>, PlotModel> onLineType, Func<List<PieSlice>, PlotModel> onPieType, Func<CategoryAxis, LinearAxis, List<BarItem>, List<string>, PlotModel> onBarType)
        {
            return onBarType(categoryAxis, valueAxis, barItems, labels);
        }
    }
}