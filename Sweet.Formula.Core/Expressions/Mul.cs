namespace Sweet.Formula.Core.Expressions
{
    public class Mul : SimpleOperation
    {
        public override byte Priority
        {
            get
            {
                return 4;
            }
        }

        public Mul()
            : base((a, b) => a * b)
        {
        }
    }
}