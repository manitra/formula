using System;
using System.Linq;

namespace Sweet.Formula.Core.Expressions
{
    using Sweet.Formula.Core.Evaluation;

    public abstract class SimpleOperation : Expr
    {
        private readonly Func<double, double, double> operateFunction;

        /// <summary>
        /// This represents the relative priority of operators.
        /// 1 is the highest then the greater is the number, the lower is the priority
        /// </summary>
        public abstract byte Priority { get; }

        protected SimpleOperation(Func<double, double, double> operatorFunction)
        {
            operateFunction = operatorFunction;
        }

        public override double Eval(Scope scope)
        {
            double result = Children[0].Eval(scope);
            return Children
                .Skip(1)
                .Aggregate(result, (current, child) => operateFunction(current, child.Eval(scope)));
        }
    }
}