using System.Text.Json.Serialization;

namespace PortfolioWebApi.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsMultipleChoice { get; set; }
        public bool MultipleAnswersPermitted { get; set; }
        public int SurveyId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
        public bool Removed { get; set; }

        public List<MultipleChoiceOption> MultipleChoiceOptions { get; set; } = [];
        public List<QuestionResponse> QuestionResponses { get; set; } = [];

        [JsonIgnore]
        public Survey Survey { get; set; }
    }
}