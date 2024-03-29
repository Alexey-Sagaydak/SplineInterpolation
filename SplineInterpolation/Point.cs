﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplineInterpolation
{
    public struct Point<T>
    {
        public T X { get; set; }
        public T Y { get; set; }

        public Point(T x, T y) 
        { 
            X = x; 
            Y = y;
        }
    }
}
