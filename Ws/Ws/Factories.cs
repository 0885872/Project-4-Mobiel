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

using Types;

namespace Factories
{
    public class PlotModelFactory
    {
        private GraphType graphType;

        public PlotModel Load()
        {
            //return graphType.Visit............
        }

        public PlotModelFactory(GraphType gt)
        {
            graphType = gt;
        }
    }

    public class PlotModelsFactory : Iterator<PlotModelFactory>
    {
        private int index;
        public List<GraphType> elements;

        public void Reset()
        {
            index = -1;
        }

        public Option<PlotModelFactory> GetNext()
        {
            index = index + 1;
            if (index > elements.Count)
            {
                PlotModelFactory gef = new PlotModelFactory(elements[index]);
                return new Some<PlotModelFactory>(gef);
            }
            else
            {
                return new None<PlotModelFactory>();
            }
        }

        public PlotModelsFactory(List<GraphType> e)
        {
            this.index = -1;
            this.elements = e;
        }
    }

    public class FormLoader
    {
        private List<GraphType> elements;

        public PlotModelsFactory Load()
        {
            return new PlotModelsFactory(elements);
        }

        public FormLoader(List<GraphType> e)
        {
            this.elements = e;
        }
    }

    public class PlotModelsFactoryToPlotModels
    {
        private List<Option<PlotModel>> factory;
        private Option<PlotModelFactory> currentFactory;
        private int index = -1;

        public Option<PlotModel> GetNext()
        {
            index = index + 1;
            return factory[index];
        }

        public PlotModelsFactoryToPlotModels(PlotModelsFactory ef)
        {
            factory = new System.Collections.Generic.List<Option<PlotModel>>();

            while (true)
            {
                this.currentFactory = ef.GetNext();

                if (this.currentFactory.IsSome())
                {
                    // PlotModelFactory dummy_factory = new PlotModelFactory(new LabelType(new Vector2(0, 0), "None"));
                    // PlotModelFactory unpacked_factory = this.currentFactory.visit<PlotModelFactory>(x => x, () => dummy_factory);
                    factory.Add(new Some<PlotModel>(unpacked_factory.Load()));
                }
                else
                {
                    factory.Add(new None<PlotModel>());
                    break;
                }
            }
        }
    }
}