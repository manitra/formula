using System;

namespace Sweet.Formula.Core.Expressions
{
    using Sweet.Formula.Core.Evaluation;

    public class Variable : Expr
    {
        private readonly string name;

        public Variable(string name)
        {
            this.name = name;
        }

        public override double Eval(Scope scope)
        {
            return scope.Get(name);
        }

        protected override void WriteSelf(System.IO.TextWriter output)
        {
            output.Write("@" + name);
        }
    }
}