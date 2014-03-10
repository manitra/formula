using System.Collections.Generic;
using Sweet.Formula.Core.Expressions;
using Sweet.Formula.Core.Parsing;

namespace Sweet.Formula.Core
{
    public class Evaluator
    {
        private readonly string expr;
        private readonly Parser parser;
        private readonly Dictionary<string, double> variables;

        public Evaluator(string expr)
        {
            this.expr = expr;
            parser = new Parser();
            variables = new Dictionary<string, double>();
        }

        public Evaluator Set(string parameterName, double value)
        {
            variables[parameterName] = value;
            return this;
        }

        public double Eval()
        {
            Expr parsedExpr = parser.Parse(this.expr);
            System.Console.WriteLine(parsedExpr);

            return parsedExpr.Eval();
        }
    }
}