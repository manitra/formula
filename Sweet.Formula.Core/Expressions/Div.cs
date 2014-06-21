namespace Sweet.Formula.Core.Expressions
{
    public class Div : SimpleOperation
    {
        public override byte Priority
        {
            get
            {
                return 4;
            }
        }

        public Div()
            : base((a, b) => a / b)
        {
        }
    }
}