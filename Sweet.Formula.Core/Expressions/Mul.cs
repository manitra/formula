namespace Sweet.Formula.Core.Expressions
{
    public class Mul : SimpleOperation
    {
        public Mul() : base((a, b) => a*b)
        {
        }
    }
}