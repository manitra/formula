using System;
using System.Linq;

namespace Sweet.Formula.Core.Expressions
{
    using Sweet.Formula.Core.Evaluation;

    public abstract class BinaryOperation : Operation
    {
        private readonly Func<double, double, double> _operateFunction;

        protected BinaryOperation(Func<double, double, double> operatorFunction)
        {
            _operateFunction = operatorFunction;
        }

        public override double Eval(Scope scope)
        {
            double result = Children[0].Eval(scope);
            return Children
                .Skip(1)
                .Aggregate(result, (current, child) => _operateFunction(current, child.Eval(scope)));
        }
    }
}