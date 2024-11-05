using System.Text.Json.Serialization;

namespace PortfolioWebApi.Models
{
    public class MultipleChoiceOption
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public string Text { get; set; }
        public int QuestionId { get; set; }
        public int TimesSelected { get  { return MultipleChoiceOptionResponses.Count; } }

        [JsonIgnore]
        public Question Question { get; set; }

        [JsonIgnore]
        public List<MultipleChoiceOptionResponse> MultipleChoiceOptionResponses { get; set; } = [];
    }
}