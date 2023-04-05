using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace SplineInterpolation
{
    internal static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            /*float Func(float x)
            {
                return (float)Math.Pow(x, 2) + 2 * (float)Math.Log(x);
            }

            MathFunction.MathFunction mathFunction = new MathFunction.MathFunction(Func);
            Console.WriteLine(mathFunction.CalculateDerivative(5, 5, 0.1f));*/
        }
    }
}
