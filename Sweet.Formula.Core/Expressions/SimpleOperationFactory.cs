using System;
using System.Collections.Generic;

namespace Sweet.Formula.Core.Expressions
{
    using System.Globalization;

    public class SimpleOperationFactory
    {
        private static readonly IDictionary<string, Func<BinaryOperation>> Operators =
            new Dictionary<string, Func<BinaryOperation>>
                {
                    { "+", () => new Add()},
                    { "-", () => new Sub()},
                    { "*", () => new Mul()},
                    { "/", () => new Div()},
                };

        public BinaryOperation Create(string @operator)
        {
            return Operators[@operator]();
        }

        public bool IsOperatorChar(char c)
        {
            return Operators.ContainsKey(c.ToString(CultureInfo.InvariantCulture));
        }
    }
}