using System.Text.Json.Serialization;

namespace PortfolioWebApi.Models
{
    public class MultipleChoiceOptionResponse
    {
        public int Id { get; set; }
        public int MultipleChoiceOptionId { get; set; }
        public int QuestionResponseId { get; set; }
                
        public MultipleChoiceOption MultipleChoiceOption { get; set; }

        [JsonIgnore]
        public QuestionResponse QuestionResponse { get; set; }
    }
}
