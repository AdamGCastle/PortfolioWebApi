using System.Text.Json.Serialization;

namespace PortfolioWebApi.Models
{
    public class QuestionResponse
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int SurveyResponseId { get; set; }
        public string Text { get; set; }
        public int? MultipleChoiceOptionId { get; set; }

        [JsonIgnore]
        public SurveyResponse SurveyResponse { get; set; }

        [JsonIgnore]
        public Question Question { get; set; }

        public List<MultipleChoiceOptionResponse> MultipleChoiceOptionResponses { get; set; } = [];
    }
}