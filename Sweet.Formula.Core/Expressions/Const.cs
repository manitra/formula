namespace Sweet.Formula.Core.Expressions
{
    using Sweet.Formula.Core.Evaluation;

    public class Const : Expr
    {
        private readonly double value;

        public Const(double value)
        {
            this.value = value;
        }

        public override double Eval(Scope scope)
        {
            return value;
        }

        protected override void WriteSelf(System.IO.TextWriter output)
        {
            output.Write((value < 0 ? "(" : "") + value + (value < 0 ? ")" : ""));
        }
    }
}