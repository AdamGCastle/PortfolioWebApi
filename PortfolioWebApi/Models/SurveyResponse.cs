using System.Text.Json.Serialization;

namespace PortfolioWebApi.Models
{
    public class SurveyResponse
    {
        public int Id { get; set; }
        public string SurveyTakerName { get; set; }
        public int SurveyId { get; set; }
        public DateTime DateCreated { get; set; }

        [JsonIgnore]
        public Survey Survey { get; set; }

        public List<QuestionResponse> QuestionResponses { get; set; } = new List<QuestionResponse>();
    }
}