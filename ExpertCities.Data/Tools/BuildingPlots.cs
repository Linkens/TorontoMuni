using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace ExpertCities.Data
{
    public static class BuildingPlots
    {
        public static PlotModel GetChartWork(Asset Build)
        {
            var Plot = new PlotModel();
            Plot.Title = "Work Task";
            var Ser = new LineSeries();
            var Actions = Build.Works.Select(w => w.Actions).Aggregate((a, b) => { a.AddRange(b); return a; }).OrderBy(w=>w.Date).GroupBy(w=>w.Date.Date);
            foreach (var item in Actions)
            {
                Ser.Points.Add(DateTimeAxis.CreateDataPoint(item.Key, item.Count()));
            }
            Plot.Axes.Add(new DateTimeAxis() {  Position = AxisPosition.Bottom , MinorIntervalType= DateTimeIntervalType.Days });
            Plot.Axes.Add(new LinearAxis() {  Position = AxisPosition.Left, Minimum=0, MinorStep = 1, MajorStep = 1 , MajorGridlineStyle = LineStyle.Dash });
            Plot.Series.Add(Ser);

            Plot.InvalidatePlot(true);
            return Plot;
        }
    }
}
