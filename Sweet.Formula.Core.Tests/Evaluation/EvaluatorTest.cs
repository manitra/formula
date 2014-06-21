namespace Sweet.Formula.Core.Tests.Evaluation
{
    using NUnit.Framework;
    using Sweet.Formula.Core.Evaluation;

    [TestFixture]
    public class EvaluatorTest
    {
        [Test]
        public void EvaluateConstantWorks()
        {
            Assert.AreEqual(1, new Evaluator("1").Eval());
        }

        [Test]
        [TestCase(3, "1 + 2    ")]
        [TestCase(-10, "1    - 11")]
        [TestCase(50, " 5 * 10")]
        [TestCase(7, "56 / 8 ")]
        public void EvaluateConstantOperationWorks(double expected, string formula)
        {
            Assert.AreEqual(expected, new Evaluator(formula).Eval());
        }

        [Test]
        [TestCase(51, "1 + 10 * 5")]
        [TestCase(3, "1 + 10 / 5")]
        [TestCase(-49, "1 - 10 * 5")]
        [TestCase(-1, "1 - 10 / 5")]
        public void EvaluateOperationPrecedenceWorks(double expected, string formula)
        {
            Assert.AreEqual(expected, new Evaluator(formula).Eval());
        }

        [Test]
        [TestCase(6, "1 + 2 + 3")]
        [TestCase(-4, "1 - 2 - 3")]
        [TestCase(0.8, "2 * 4 / 10")]
        [TestCase(3, "2 / 4 * 6")]
        public void EvaluateOperationAssociativityWorks(double expected, string formula)
        {
            Assert.AreEqual(expected, new Evaluator(formula).Eval());
        }

        [Test]
        public void EvaluateVariableWorks()
        {
            Assert.AreEqual(
                5,
                new Evaluator("unit")
                    .Set("unit", 5)
                    .Eval()
            );
        }

        [Test]
        public void EvaluateConstantWithVariableOperationWorks()
        {
            Assert.AreEqual(
                5.5,
                new Evaluator("2 * unit")
                    .Set("unit", 2.75)
                    .Eval()
            );
        }

    }
}
