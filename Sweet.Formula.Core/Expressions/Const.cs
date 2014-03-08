namespace Sweet.Formula.Core.Expressions
{
    public class Const : Expr
    {
        private readonly double value;

        public Const(double value)
        {
            this.value = value;
        }

        public override double Eval()
        {
            return value;
        }

        protected override void WriteSelf(System.IO.TextWriter output)
        {
            output.Write(value);
        }
    }
}