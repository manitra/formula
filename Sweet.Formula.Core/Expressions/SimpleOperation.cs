using System;
using System.Linq;

namespace Sweet.Formula.Core.Expressions
{
    public class SimpleOperation : Expr
    {
        private readonly Func<double, double, double> OperateFunction;

        public SimpleOperation(Func<double, double, double> operatorFunction)
        {
            OperateFunction = operatorFunction;
        }

        public override double Eval()
        {
            double result = Children[0].Eval();
            foreach (Expr child in Children.Skip(1))
            {
                result = OperateFunction(result, child.Eval());
            }
            return result;
        }
    }
}