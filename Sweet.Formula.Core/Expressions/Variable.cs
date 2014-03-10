using System;

namespace Sweet.Formula.Core.Expressions
{
    public class Variable : Expr
    {
        private readonly string name;
        private readonly Func<string, double> variableValueReader;

        public Variable(string name, Func<string, double> variableValueReader)
        {
            this.name = name;
            this.variableValueReader = variableValueReader;
        }

        public override double Eval()
        {
            return variableValueReader(name);
        }

        protected override void WriteSelf(System.IO.TextWriter output)
        {
            output.Write("@" + name);
        }
    }
}