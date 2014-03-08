using Sweet.Formula.Core;

namespace Sweet.Formula.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            double result = new Evaluator("(235 +  2 + art - (unit*   qtt))/ ((1 + (xrt / 25)) * 2)")
            //double result = new Evaluator("unit*   qtt")
                .Set("unit", 2.75)
                .Set("qtt", 15)
                .Eval();

            System.Console.WriteLine(result);
        }
    }
}