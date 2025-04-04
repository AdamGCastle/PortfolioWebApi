using System.Text.Json.Serialization;

namespace PortfolioWebApi.Models
{
    public class Survey
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public int TimesTaken { get { return SurveyResponses.Count; } }

        public int? CreatedByAccountId { get; set; }

        public List<Question> Questions { get; set; } = [];
        public List<SurveyResponse> SurveyResponses { get; set;  } = [];

        [JsonIgnore]
        public Account CreatedByAccount { get; set; }
    }
}