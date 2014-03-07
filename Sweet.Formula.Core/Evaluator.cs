using Sweet.Formula.Core.Expressions;
using Sweet.Formula.Core.Parsing;

namespace Sweet.Formula.Core
{
    public class Evaluator
    {
        private readonly string expr;
        private readonly Parser parser;

        public Evaluator(string expr)
        {
            this.expr = expr;
            parser = new Parser();
        }

        public Evaluator Set(string parameterName, double value)
        {
            return this;
        }

        public double Eval()
        {
            Expr expr = parser.Parse(this.expr);
            return expr.Eval();
        }
    }
}