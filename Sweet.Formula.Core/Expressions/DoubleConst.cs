namespace Sweet.Formula.Core.Expressions
{
    using System.Globalization;

    using Sweet.Formula.Core.Evaluation;

    public class DoubleConst : Expr
    {
        private readonly double _value;

        public DoubleConst(double value)
        {
            _value = value;
        }

        public override double Eval(Scope scope)
        {
            return _value;
        }

        protected override void WriteSelf(System.IO.TextWriter output)
        {
            output.Write(_value.ToString(CultureInfo.InvariantCulture));
        }
    }
}