using NUnit.Framework;
using PearsonCorrelationScore.Reviews;

namespace PearsonCorrelationScore.Tests.Reviews
{
    [TestFixture]
    class ReviewerTest
    {
        private Reviewer reviewer;
        
        [SetUp]
        public void SetUp()
        {
            reviewer = new Reviewer();
        }

        [Test]
        public void ReviewerShouldHaveAName()
        {
            reviewer.Name = "Robert";
        }

        [Test]
        public void ReviewerShouldStartWithNoReviews()
        {
            Assert.IsEmpty(reviewer.Reviews);
        }

        [Test]
        public void ReviewerShouldHaveAValidReview()
        {
            reviewer.AddReview("Clean Code", 5);
            Assert.IsNotEmpty(reviewer.Reviews);
        }
    }
}
