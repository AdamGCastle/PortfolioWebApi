namespace PortfolioWebApi.Models
{
    public class AccountDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string VerifyPassword { get; set; }
        public string NewPassword { get; set; }
        public string Token { get; set; }
    }
}
