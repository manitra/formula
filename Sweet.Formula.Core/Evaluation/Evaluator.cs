namespace Sweet.Formula.Core.Evaluation
{
    using Sweet.Formula.Core.Expressions;
    using Sweet.Formula.Core.Parsing;

    public class Evaluator
    {
        private readonly string expr;
        private readonly Parser parser;
        private readonly Scope scope;

        public Evaluator(string expr)
        {
            this.expr = expr;
            this.parser = new Parser();
            this.scope = new Scope();
        }

        public Evaluator Set(string parameterName, double value)
        {
            scope.Set(parameterName, value);
            return this;
        }

        public double Eval()
        {
            Expr parsedExpr = parser.Parse(expr);
#if DEBUG
            System.Console.WriteLine(parsedExpr);
#endif

            return parsedExpr.Eval(this.scope);
        }
    }
}