namespace Sweet.Formula.Core.Tests.Evaluation
{
    using NUnit.Framework;
    using Sweet.Formula.Core.Evaluation;

    [TestFixture]
    public class DoubleEvaluatorTest
    {
        [Test]
        [TestCase(1, "      1     ")]
        [TestCase(1, "1")]
        [TestCase(0.0, "0.0")]
        [TestCase(-2.67987, "-2.67987")]
        [TestCase(1234567890.12, "1234567890.12")]
        public void EvaluatingConstantWorks(double expected, string formula)
        {
            Assert.AreEqual(expected, new Evaluator(formula).Eval());
        }

        [Test]
        [TestCase(3, "1 + 2    ")]
        [TestCase(-10, "1    - 11")]
        [TestCase(50, " 5 * 10")]
        [TestCase(7, "56 / 8 ")]
        public void EvaluatingOperationWithConstantsWorks(double expected, string formula)
        {
            Assert.AreEqual(expected, new Evaluator(formula).Eval());
        }

        [Test]
        [TestCase(51, "1 + 10 * 5")]
        [TestCase(3, "1 + 10 / 5")]
        [TestCase(-49, "1 - 10 * 5")]
        [TestCase(-1, "1 - 10 / 5")]
        public void EvaluatingOperationWithPrecedenceWorks(double expected, string formula)
        {
            Assert.AreEqual(expected, new Evaluator(formula).Eval());
        }

        [Test]
        [TestCase(6, "1 + 2 + 3")]
        [TestCase(-4, "1 - 2 - 3")]
        [TestCase(0.8, "2 * 4 / 10")]
        [TestCase(3, "2 / 4 * 6")]
        [TestCase(1, "1024 / 2 / 2 / 2 / 2 / 2 / 2 / 2 / 2 / 2 / 2")]
        [TestCase(1, "10 - 1 - 1 - 1 - 1 - 1 - 1 - 1 - 1 - 1")]
        public void EvaluatingOperationAssociativityWorks(double expected, string formula)
        {
            Assert.AreEqual(expected, new Evaluator(formula).Eval());
        }

        [Test]
        public void EvaluatingVariableWorks()
        {
            Assert.AreEqual(
                5,
                new Evaluator("unit")
                    .Set("unit", 5)
                    .Eval()
            );
        }

        [Test]
        [ExpectedException]
        public void EvaluatingNonExistingVariableThrows()
        {
            new Evaluator("unit").Eval();
        }

        [Test]
        public void EvaluateConstantWithVariableOperationWorks()
        {
            Assert.AreEqual(
                5.5,
                new Evaluator("2 * unit ")
                    .Set("unit", 2.75)
                    .Eval()
            );
        }

        [Test]
        [TestCase(1, "(1)")]
        [TestCase(1, "((((((((((1))))))))))")]
        public void EvaluatingRedundantBlockWorks(double expected, string formula)
        {
            Assert.AreEqual(expected, new Evaluator(formula).Eval());
        }

        [Test]
        [TestCase(40, "10 * (1 + 3)")]
        [TestCase(-5, "10 / (1 - 3)")]
        public void EvaluatingBlockOverridesOperatorPrecedence(double expected, string formula)
        {
            Assert.AreEqual(expected, new Evaluator(formula).Eval());
        }

        [Test]
        [TestCase(-490, "10 - (1000 - 500)")]
        [TestCase(5, "10 / (1000 / 500)")]
        public void EvaluatingBlockOverridesOperatorAssociativity(double expected, string formula)
        {
            Assert.AreEqual(expected, new Evaluator(formula).Eval());
        }
    }
}
