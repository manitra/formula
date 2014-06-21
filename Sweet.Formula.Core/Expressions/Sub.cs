namespace Sweet.Formula.Core.Expressions
{
    public class Sub : SimpleOperation
    {
        public override byte Priority
        {
            get
            {
                return 5;
            }
        }

        public Sub() : base((a, b) => a - b)
        {
        }
    }
}