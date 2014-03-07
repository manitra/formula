using System;
using System.Collections.Generic;

namespace Sweet.Formula.Core.Expressions
{
    public class SimpleOperationFactory
    {
        private static readonly IDictionary<string, Func<SimpleOperation>> operators =
            new Dictionary<string, Func<SimpleOperation>>
                {
                    { "+", () => new Add()},
                    { "-", () => new Sub()},
                    { "*", () => new Mul()},
                    { "/", () => new Div()},
                };

        public SimpleOperation Create(string @operator)
        {
            return operators[@operator]();
        }

        public bool IsOperatorChar(char c)
        {
            return operators.ContainsKey(c.ToString());
        }
    }
}