using System.Collections.Generic;

namespace PearsonCorrelationScore.Reviews
{
    public class Reviewer
    {
        public Dictionary<string, double> Reviews;
        public string Name { get; set; }

        public Reviewer()
        {
            Reviews = new Dictionary<string, double>();
        }

        public void AddReview(string title, double score)
        {
            Reviews.Add(title, score);    
        }
    }
}