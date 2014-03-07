namespace Sweet.Formula.Core.Expressions
{
    public class Div : SimpleOperation
    {
        public Div() : base((a, b) => a/b)
        {
        }
    }
}