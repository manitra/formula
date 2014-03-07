namespace Sweet.Formula.Core.Expressions
{
    public class Sub : SimpleOperation
    {
        public Sub() : base((a, b) => a - b)
        {
        }
    }
}