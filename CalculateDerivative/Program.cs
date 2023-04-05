using MathFunctions;

namespace SplineInterpolation
{
    internal static class Program
    {
        static void Main()
        {
            float epsilon = 0.1f;
            float Function(float x) => 2 * (float)Math.Pow(x, 2);
            
            MathFunction mathFunction = new MathFunction(Function);
            Console.WriteLine("ВЫЧИСЛЕНИЕ ПЕРВОЙ ПРОИЗВОДНОЙ ФУНКЦИИ:\nf(x) = 2*x^2, e = {0}\n", epsilon);

            Console.WriteLine("| X = 0,01 | f'(X) эксп. = {0, 3:N3} | f'(X) теор. = 0,04 |", mathFunction.CalculateDerivative(0.01f, 1, epsilon));
            Console.WriteLine("| X = 0,1  | f'(X) эксп. = {0, 5:N3} | f'(X) теор. = 0,4  |", mathFunction.CalculateDerivative(0.1f, 1, epsilon));
            Console.WriteLine("| X = 1    | f'(X) эксп. = {0, 5:N3} | f'(X) теор. = 4    |", mathFunction.CalculateDerivative(1f, 1, epsilon));
            Console.WriteLine("| X = 10   | f'(X) эксп. = {0, 5:N2} | f'(X) теор. = 40   |", mathFunction.CalculateDerivative(10f, 1, epsilon));
            Console.WriteLine("| X = 100  | f'(X) эксп. = {0, 5:N1} | f'(X) теор. = 400  |", mathFunction.CalculateDerivative(100f, 1, epsilon));
        }
    }
}
