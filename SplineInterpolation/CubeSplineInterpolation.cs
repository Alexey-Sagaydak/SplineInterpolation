using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SLAESolver;
using MathFunctions;

namespace SplineInterpolation
{
    public class CubeSplineInterpolation
    {
        private Point[] _points;
        private float[] _a;
        private float[] _b;
        private float[] _c;
        private float[] _d;
        private int n;

        public CubeSplineInterpolation(Point[] points)
        {
            if (points == null)
                throw new ArgumentNullException(nameof(points));
            _points= points;
            n = points.Length;
            InitializeArrays();
            CalculateCoefficients();
            
        }
        private float CalculateCubePolynom(int polynomIndex, float x)
        {
            return _a[polynomIndex] + _b[polynomIndex] * (x - _points[polynomIndex].X) 
                + _c[polynomIndex] * (float) Math.Pow(x - _points[polynomIndex].X, 2) 
                + _d[polynomIndex] * (float) Math.Pow(x - _points[polynomIndex].X, 3); 
        }
        public float SplineValue(float x, int derivativePower)
        {
            float y = 0;
            int splineIndex;
            
            if (x < _points[0].X || x > _points[n - 1].X)
                throw new ArgumentOutOfRangeException(nameof(x));

            splineIndex = GetSplineIndex(x);
            
            return CalculateDerivative(x, splineIndex, derivativePower, 0.1f); 
        }
                
        private int GetSplineIndex(float x)
        {
            for (int i = 1; i < n; i++)
                if (x <= _points[i].X) 
                    return i - 1;

            return -1;
        }

        private void CalculateCoefficients()
        {
            CalculateA();
            CalculateC();
            CalculateB();
            CalculateD();
        }

        private void CalculateA() 
        {
            for (int i = 0; i < _a.Length; i++)
                _a[i] = _points[i].Y;
        }
        
        private void CalculateB() 
        {
            for (int i = 0; i < n - 1; i++)
                _b[i] = (_points[i + 1].Y - _points[i].Y) / CalculateH(i) - (_c[i + 1] + 2 * _c[i]) * CalculateH(i) / 3.0f;
        }
        
        private void CalculateC() 
        {
            float[,] A = new float[n, n];
            Matrix matrix = new Matrix(new float[ n - 2, n - 1 ], n - 2, n - 1);
            SweepMethod sweepMethod= new SweepMethod();

            for (int i = 1; i < n - 1; i++)
            {
                A[i, i - 1] = CalculateH(i - 1);
                A[i, i] = 2 * (CalculateH(i - 1) + CalculateH(i));
                A[i, i + 1] = CalculateH(i);
            }
             
            for (int i = 1; i < n - 1; i++)
                for (int j = 1; j < n - 1; j++)
                    matrix[i - 1, j - 1] = A[i, j];
            
            for (int i = 1; i < n - 1; i++)
                matrix[i - 1, n - 2] = 3 * ((_points[i + 1].Y - _points[i].Y) / CalculateH(i) - (_points[i].Y - _points[i - 1].Y) / CalculateH(i - 1));
            
            float[] result = sweepMethod.Solve(matrix);
            
            for (int i = 1; i < n - 1; i++)
                _c[i] = result[i - 1];
        }
        
        private void CalculateD() 
        { 
            for (int i = 0; i < n - 1; i++)
                _d[i] = (_c[i + 1] - _c[i]) / (3 * CalculateH(i)); 
        }

        private float CalculateH(int index) => _points[index + 1].X - _points[index].X;
        public float CalculateDerivative(float x, int coeffIndex, int power, float step)
        {
            if (power == 0) return CalculateCubePolynom(coeffIndex, x);
            return (CalculateDerivative(x + step, coeffIndex, power - 1, step)
                - CalculateDerivative(x - step, coeffIndex, power - 1, step)) / (2.0f * step);
        }
        private void InitializeArrays()
        {
            _a = new float[n - 1];
            _b = new float[n - 1];
            _c = new float[n];
            _d = new float[n - 1];
        }
    }
}
