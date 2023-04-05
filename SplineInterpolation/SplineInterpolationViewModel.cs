using FunctionApproximation.Domain;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplineInterpolation
{
    public class SplineInterpolationViewModel
    {
        public CubeSplineInterpolation splineInterpolation;
        private ConfigurationParser configurationParser;
        public SplineInterpolationViewModel() 
        {
            configurationParser = new ConfigurationParser(); 
        }

        public void ParseConfiguration(string path) {
            splineInterpolation = new CubeSplineInterpolation(configurationParser.Parse(path));
        }

        public Point<float>[] GetSplineValues(int derivativePower)
        {
            int length = Convert.ToInt32((splineInterpolation.Points[splineInterpolation.Points.Length - 1].X - splineInterpolation.Points[0].X) * 100);
            Point<float>[] points = new Point<float>[length];
            int index = 0;
            for (float i = splineInterpolation.Points[0].X; i < splineInterpolation.Points[splineInterpolation.Points.Length - 1].X - 0.01f; i += 0.01f)
            {
                points[index] = new Point<float>(i, splineInterpolation.CalculateSpline(i, derivativePower));
                index++;
            }
            return points;
        }
    }
}
