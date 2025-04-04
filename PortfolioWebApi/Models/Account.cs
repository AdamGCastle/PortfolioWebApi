namespace PortfolioWebApi.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime DateCreated { get; set; }

        public List<Survey> SurveysCreated { get; set; } = [];
    }
}
