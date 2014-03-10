using System;
using System.Collections.Generic;
using System.IO;

namespace Sweet.Formula.Core.Expressions
{
    public abstract class Expr
    {
        protected Expr()
        {
            Children = new List<Expr>();
        }

        public Expr Parent { get; private set; }
        public IList<Expr> Children { get; private set; }

        public abstract double Eval();

        public Expr AddChild(Expr child)
        {
            Children.Add(child);
            child.Parent = this;
            return this;
        }

        public Expr AddChildren(IEnumerable<Expr> children)
        {
            foreach (var child in children)
                AddChild(child);

            return this;
        }

        public override string ToString()
        {
            var result = new StringWriter();
            ToString(new List<IndentType>(), result);
            return result.ToString();
        }

        protected virtual void ToString(IList<IndentType> indents, TextWriter output)
        {
            WriteIndents(indents, output);
            WriteSelf(output);
            output.Write(Environment.NewLine);
            WriteChildren(indents, output);
        }

        protected virtual void WriteSelf(TextWriter output)
        {
            output.Write(GetType().Name);
        }

        protected virtual void WriteChildren(IList<IndentType> parentIndents, TextWriter output)
        {
            for (int i = 0; i < Children.Count; i++)
            {
                var newIndents = CalculateChildIndents(parentIndents, i == Children.Count - 1);
                Children[i].ToString(newIndents, output);
            }
        }

        private void WriteIndents(IEnumerable<IndentType> indents, TextWriter output)
        {
            foreach (var t in indents)
            {
                switch (t)
                {
                    case IndentType.Blank:
                        output.Write(" ");
                        break;
                    case IndentType.MiddleWithElement:
                        output.Write("├");
                        break;
                    case IndentType.MiddleWithoutElement:
                        output.Write("│");
                        break;
                    case IndentType.Last:
                        output.Write("└");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("indents", "Unknown indent type : " + t);
                }
            }
        }

        private IList<IndentType> CalculateChildIndents(IList<IndentType> parentIndents, bool isLast)
        {
            var result = new List<IndentType>(parentIndents.Count + 1);
            foreach (var indent in parentIndents)
            {
                IndentType childIndent;
                switch (indent)
                {
                    case IndentType.Blank:
                    case IndentType.MiddleWithoutElement:
                        childIndent = indent;
                        break;
                    case IndentType.MiddleWithElement:
                        childIndent = IndentType.MiddleWithoutElement;
                        break;
                    case IndentType.Last:
                        childIndent = IndentType.Blank;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                result.Add(childIndent);
            }

            result.Add(isLast ? IndentType.Last : IndentType.MiddleWithElement);

            return result;
        }

        protected enum IndentType
        {
            Blank,
            MiddleWithElement,
            MiddleWithoutElement,
            Last
        }
    }
}