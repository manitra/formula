namespace Sweet.Formula.Core.Tests.Evaluation
{
    using NUnit.Framework;
    using Sweet.Formula.Core.Evaluation;

    [TestFixture]
    public class boolEvaluatorTest
    {
        [Test]
        [TestCase(true, "true")]
        [TestCase(true, "True")]
        [TestCase(false, "false")]
        [TestCase(false, "False")]
        public void EvaluatingConstantWorks(bool expected, string formula)
        {
            Assert.AreEqual(expected, new Evaluator(formula).Eval());
        }

        [Test]
        [TestCase(true, "true && true")]
        [TestCase(false, "true && false")]
        [TestCase(false, "false && true")]
        [TestCase(false, "false && false")]
        public void EvaluatingAndOperationWithConstantsWorks(bool expected, string formula)
        {
            Assert.AreEqual(expected, new Evaluator(formula).Eval());
        }

        [Test]
        [TestCase(true, "true || true")]
        [TestCase(false, "true || false")]
        [TestCase(false, "false || true")]
        [TestCase(false, "false || false")]
        public void EvaluatingOrOperationWithConstantsWorks(bool expected, string formula)
        {
            Assert.AreEqual(expected, new Evaluator(formula).Eval());
        }

        [Test]
        [TestCase(true, "1 < 2")]
        [TestCase(false, "2 < 1")]
        [TestCase(false, "1 < 1")]
        [TestCase(false, "0 < 0")]
        public void EvaluatingLtOperationWithConstantsWorks(bool expected, string formula)
        {
            Assert.AreEqual(expected, new Evaluator(formula).Eval());
        }

        [Test]
        [TestCase(true, "1 <= 2")]
        [TestCase(false, "2 <= 1")]
        [TestCase(true, "1 <= 1")]
        [TestCase(true, "0 <= 0")]
        public void EvaluatingLeOperationWithConstantsWorks(bool expected, string formula)
        {
            Assert.AreEqual(expected, new Evaluator(formula).Eval());
        }

        [Test]
        [TestCase(false, "1 > 2")]
        [TestCase(true, "2 > 1")]
        [TestCase(false, "1 > 1")]
        [TestCase(false, "0 > 0")]
        public void EvaluatingGtOperationWithConstantsWorks(bool expected, string formula)
        {
            Assert.AreEqual(expected, new Evaluator(formula).Eval());
        }

        [Test]
        [TestCase(false, "1 >= 2")]
        [TestCase(true, "2 >= 1")]
        [TestCase(true, "1 >= 1")]
        [TestCase(true, "0 >= 0")]
        public void EvaluatingGeOperationWithConstantsWorks(bool expected, string formula)
        {
            Assert.AreEqual(expected, new Evaluator(formula).Eval());
        }

        [Test]
        [TestCase(false, "1 == 2")]
        [TestCase(false, "2 == 1")]
        [TestCase(true, "1 == 1")]
        [TestCase(true, "0 == 0")]
        public void EvaluatingEqualsOperationWithConstantsWorks(bool expected, string formula)
        {
            Assert.AreEqual(expected, new Evaluator(formula).Eval());
        }

        [Test]
        [TestCase(true, "1 != 2")]
        [TestCase(true, "2 != 1")]
        [TestCase(false, "1 != 1")]
        [TestCase(false, "0 != 0")]
        public void EvaluatingIsDifferentOperationWithConstantsWorks(bool expected, string formula)
        {
            Assert.AreEqual(expected, new Evaluator(formula).Eval());
        }
    }
}
