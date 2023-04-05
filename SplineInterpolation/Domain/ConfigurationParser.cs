using SplineInterpolation;
using System;
using System.Drawing;
using System.IO;

namespace FunctionApproximation.Domain
{
    public class ConfigurationParser
    {
        public ConfigurationParser()
        {

        }

        public Point<float>[] Parse(string path)
        {
            if (String.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            
            if (!File.Exists(path))
                throw new FileNotFoundException(nameof(path));

            StreamReader sr = new StreamReader(path);
            string[] parsedX = sr.ReadLine().Split(' ');
            string[] parsedY = sr.ReadLine().Split(' ');
            Point<float>[] points = new Point<float>[parsedX.Length];

            for (int i = 0; i < parsedX.Length; i++)
                points[i] = new Point<float>(Convert.ToInt32(parsedX[i]), Convert.ToInt32(parsedY[i]));  
            
            return points;
        }
    }
}
