namespace PortfolioWebApi.Models
{
    public class Survey
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public int TimesTaken { get { return SurveyResponses.Count; } }

        public List<Question> Questions { get; set; } = [];
        public List<SurveyResponse> SurveyResponses { get; set;  } = [];
    }
}