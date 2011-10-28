using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PearsonCorrelationScore.Reviews;

namespace PearsonCorrelationScore
{
    public class PearsonCorrelation : ISimilarityScore
    {
        private readonly Reviewer CompareTo;
        private readonly Reviewer CompareWith;
        private List<string> similarTitles;

        public PearsonCorrelation(Reviewer compareTo, Reviewer compareWith)
        {
            CompareTo = compareTo;
            CompareWith = compareWith;
            similarTitles = FindSharedItems();
        }

        public double Score()
        {
            return CalculatePearsonCorrelationScore();
        }

        private double CalculatePearsonCorrelationScore()
        {
            var n = similarTitles.Count;
            if (n == 0)
            {
                return 0.0;
            }

            var sum1 = SumScoresCompareTo();
            var sum2 = SumScoresCompareWith();

            var sum1Sq = SumSquaresCompareTo();
            var sum2Sq = SumSquaresCompareWith();

            var pSum = SumProductsOfBothReviewers();
            
            //num=    pSum − (sum1 * sum2 / n) 
            var num = pSum - (sum1 * sum2 / n);

            //  den=       sqrt((sum1Sq −      pow(sum1, 2) / n) * (sum2Sq −      pow(sum2, 2) / n))
            var den = Math.Sqrt((sum1Sq - Math.Pow(sum1, 2) / n) * (sum2Sq - Math.Pow(sum2, 2) / n));

            if (den == 0.0)
            {
                return 0.0;
            }

            var answer = num / den;
            return Math.Round(answer, 3);
        }

        public double SumProductsOfBothReviewers()
        {
            return similarTitles.Sum(title => CompareTo.Reviews[title] * CompareWith.Reviews[title]);
        }

        public double SumSquaresCompareWith()
        {
            return similarTitles.Sum(title => Math.Pow(CompareWith.Reviews[title], 2));
        }

        public double SumSquaresCompareTo()
        {
            return similarTitles.Sum(title => Math.Pow(CompareTo.Reviews[title], 2));
        }

        public double SumScoresCompareTo()
        {
            return similarTitles.Sum(title => CompareTo.Reviews[title]);
        }

        public double SumScoresCompareWith()
        {
            return similarTitles.Sum(title => CompareWith.Reviews[title]);
        }

        public List<string> FindSharedItems()
        {
            return (from r in CompareTo.Reviews where CompareWith.Reviews.ContainsKey(r.Key) select r.Key).ToList();
        }

        private static void Main(string[] args)
        {
        }
    }
}
