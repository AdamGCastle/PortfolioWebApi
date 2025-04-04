using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PortfolioWebApi.Contexts;
using PortfolioWebApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using bCrypt = BCrypt.Net.BCrypt;
using System.Text;
using Microsoft.AspNetCore.Cors;

namespace PortfolioWebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [EnableCors]
    public class AccountsController : ControllerBase
    {
        internal AcPortFolioDbContext _acPortfolioDb = new();
        readonly static IConfigurationSection _section = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("AuthenticationSettings");

        [HttpPost]
        public async Task<IActionResult> Create(AccountDto model)
        {
            if (string.IsNullOrWhiteSpace(model.Email) && string.IsNullOrWhiteSpace(model.Username))
            {
                return BadRequest("Please provide either a user name or an email address.");
            }

            if (string.IsNullOrWhiteSpace(model.Password))
            {
                return BadRequest("Password cannot be empty.");
            }

            if (_acPortfolioDb.Accounts.Any(u => u.Email == model.Email))
            {
                return Conflict("Email already in use.");
            }

            if (_acPortfolioDb.Accounts.Any(u => u.Username == model.Username))
            {
                return Conflict("Username already in use.");
            }

            var account = new Account
            {
                Username = model.Username,
                Email = model.Email,
                PasswordHash = bCrypt.HashPassword(model.Password),
                DateCreated = DateTime.Now
            };

            _acPortfolioDb.Accounts.Add(account);
            await _acPortfolioDb.SaveChangesAsync();

            return Ok(account);
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountDto model)
        {
            var dbAccount = await _acPortfolioDb.Accounts.FirstOrDefaultAsync(x => x.Username == model.Username);

            if (dbAccount == null)
            {
                return NotFound();
            }

            bool passwordVerified = bCrypt.Verify(model.Password, dbAccount.PasswordHash);

            if (!passwordVerified)
            {
                return Unauthorized();
            }

            return Ok(new AccountDto
            {                
                Username = dbAccount.Username,
                Id = dbAccount.Id,
                Token = GenerateJwtToken(dbAccount)
            });
        }
                
        private string GenerateJwtToken(Account account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_section["JWT_Secret"].ToString());

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                    new Claim(ClaimTypes.Name, account.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
