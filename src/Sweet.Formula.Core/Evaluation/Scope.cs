using System.Collections.Generic;

namespace Sweet.Formula.Core.Evaluation
{
    public class Scope
    {
        private readonly Dictionary<string, double> variables;

        public Scope()
        {
            variables = new Dictionary<string, double>();
        }

        public Scope Set(string parameterName, double value)
        {
            variables[parameterName] = value;
            return this;
        }

        public double Get(string parameterName)
        {
            return variables[parameterName];
        }
    }
}
