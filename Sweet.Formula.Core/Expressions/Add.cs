namespace Sweet.Formula.Core.Expressions
{
    public class Add : SimpleOperation
    {
        public Add() : base((a, b) => a + b)
        {
        }
    }
}