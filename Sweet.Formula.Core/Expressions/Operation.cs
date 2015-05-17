using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sweet.Formula.Core.Evaluation;

namespace Sweet.Formula.Core.Expressions
{
    public abstract class Operation : Expr
    {
        /// <summary>
        /// This represents the relative priority of operators.
        /// 1 is the highest then the greater is the number, the lower is the priority
        /// </summary>
        public abstract byte Priority { get; }
    }
}
