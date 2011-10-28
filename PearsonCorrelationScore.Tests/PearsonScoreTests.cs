using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace PearsonCorrelationScore.Tests
{
    [TestFixture]
    class PearsonScoreTests
    {
        private PearsonCorrelation pearsonCorrelationScore;

        [SetUp]
        public void SetUp()
        {
            pearsonCorrelationScore = new PearsonCorrelation(ReviewerBuilder.BuildReviewer1(), ReviewerBuilder.BuildReviewer2());
        }

        [Test]
        public void ReviewersThatHaveReviewedUniqueTitlesShouldNotBeSimilar()
        {
            var r1 = ReviewerBuilder.BuildReviewer1();
            var r2 = ReviewerBuilder.BuildAReviewerThatReviewedSomethingUnique();
            pearsonCorrelationScore = new PearsonCorrelation(r1, r2);
            Assert.AreEqual(0, pearsonCorrelationScore.Score());
        }

        [Test]
        public void ReviewersThatHaveTheSameTasteShouldHaveAPerfectScore()
        {
            var r1 = ReviewerBuilder.BuildReviewer1();
            pearsonCorrelationScore = new PearsonCorrelation(r1, r1);
            Assert.AreEqual(1.0, pearsonCorrelationScore.Score());
        }

        [Test]
        public void ShouldSumAllOfTheScoresFromTheFirstUser()
        {
            Assert.AreEqual(18.0, pearsonCorrelationScore.SumScoresCompareTo());
        }

        [Test]
        public void ShouldSumAllOfTheScoresFromTheSecondUser()
        {
            Assert.AreEqual(19.5, pearsonCorrelationScore.SumScoresCompareWith());
        }

        [Test]
        public void ShouldSumTheSquaresOfAllOfTheScoresFromTheFirstUser()
        {
            Assert.AreEqual(55.0, pearsonCorrelationScore.SumSquaresCompareTo());
        }

        [Test]
        public void ShouldSumTheSquaresOfAllOfTheScoresFromTheSecondUser()
        {
            Assert.AreEqual(69.75, pearsonCorrelationScore.SumSquaresCompareWith());
        }

        [Test]
        public void ShouldSumTheProductsOfBothReviewers()
        {
            Assert.AreEqual(59.5, pearsonCorrelationScore.SumProductsOfBothReviewers());
        }

        [Test]
        public void TwoReviewersWithSomeSimilarReviewsShouldHaveAScoreBetweenZeroAndOne()
        {
            Assert.AreEqual(0.396, pearsonCorrelationScore.Score());
        }

        [Test]
        public void ReviewersThatHaveTheSameTasteShouldHaveAllSimilarItems()
        {
            var r1 = ReviewerBuilder.BuildReviewer1();
            pearsonCorrelationScore = new PearsonCorrelation(r1, r1);
            Assert.AreEqual(6, pearsonCorrelationScore.FindSharedItems().Count);
        }

        [Test]
        public void ShouldHaveASingleMaximumReview()
        {
            var max = ReviewerBuilder.BuildOneReviewMax();
            Assert.AreEqual(5, max.Reviews.First().Value);
        }

        [Test]
        public void TwoSimilarReviewersShouldHaveOneLikeReview()
        {
            var r1 = ReviewerBuilder.BuildOneReviewMax();
            var r2 = ReviewerBuilder.BuildOneReviewMin();

            pearsonCorrelationScore = new PearsonCorrelation(r1, r2);

            Assert.IsNotEmpty(pearsonCorrelationScore.FindSharedItems());
        }
    }
}
