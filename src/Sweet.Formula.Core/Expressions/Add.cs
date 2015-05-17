namespace Sweet.Formula.Core.Expressions
{
    public class Add : BinaryOperation
    {
        public override byte Priority
        {
            get
            {
                return 5;
            }
        }

        public Add()
            : base((a, b) => a + b)
        {
        }
    }
}