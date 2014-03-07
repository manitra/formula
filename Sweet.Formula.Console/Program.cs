using Sweet.Formula.Core;

namespace Sweet.Formula.Console
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            double result = new Evaluator("(unit*qtt)/100")
                .Set("unit", 2.75)
                .Set("qtt", 15)
                .Eval();

            System.Console.WriteLine(result);
        }
    }
}