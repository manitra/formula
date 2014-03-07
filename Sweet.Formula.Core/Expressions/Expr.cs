using System.Collections.Generic;

namespace Sweet.Formula.Core.Expressions
{
    public abstract class Expr
    {
        public Expr()
        {
            Children = new List<Expr>();
        }

        public IList<Expr> Children { get; private set; }

        public abstract double Eval();

        public Expr AddChild(Expr child)
        {
            Children.Add(child);
            return this;
        }

        public Expr AddChildren(IEnumerable<Expr> children)
        {
            foreach (var child in children)
                Children.Add(child);

            return this;
        }
    }
}