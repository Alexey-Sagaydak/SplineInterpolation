using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SplineInterpolation
{
    public partial class Form1 : Form
    {
        private SplineInterpolationViewModel viewModel;
        public Form1()
        {
            InitializeComponent();
            viewModel = new SplineInterpolationViewModel();
            pathTextBox.Text = "config.txt";
        }

        private void solveButton_Click(object sender, EventArgs e)
        {
            string path = pathTextBox.Text;
            viewModel.ParseConfiguration(path);
            BuildSplineInterpolation();
            BuildFirstDerivative();
            BuildSecondDerivative();
            BuildPoints();
        }

        private void BuildSplineInterpolation()
        {
            Point<float>[] points = viewModel.GetSplineValues(0);
            BuildPlot(points, "Куб. сплайн", ChartDashStyle.Solid, Color.Red, 3);
        }
        private void BuildFirstDerivative()
        {
            Point<float>[] points = viewModel.GetSplineValues(1);
            BuildPlot(points, "1-я производная", ChartDashStyle.Solid, Color.Green, 3);
        }
        private void BuildSecondDerivative()
        {
            Point<float>[] points = viewModel.GetSplineValues(2);
            BuildPlot(points, "2-я производная", ChartDashStyle.Solid, Color.Blue, 3);
        }
        private void BuildPoints()
        {
            BuildPlot(viewModel.splineInterpolation.Points, $"Точки", ChartDashStyle.Solid, Color.LightSeaGreen, 10, SeriesChartType.Point);
        }
        private void BuildPlot(Point<float>[] points, string name, ChartDashStyle style, Color color,
            int borderWidth, SeriesChartType seriesChartType = SeriesChartType.Spline)
        {
            chart.Series.Add(name);
            chart.Series.FindByName(name).BorderDashStyle = style;
            chart.Series.FindByName(name).ChartType = seriesChartType;
            chart.Series.FindByName(name).Color = color;
            chart.Series.FindByName(name).BorderWidth = borderWidth;
            chart.Series.FindByName(name).MarkerSize = borderWidth;


            for (int i = 0; i < points.Length - 1; i++)
                chart.Series.FindByName(name).Points.AddXY(points[i].X, points[i].Y);
        }
    }
}
