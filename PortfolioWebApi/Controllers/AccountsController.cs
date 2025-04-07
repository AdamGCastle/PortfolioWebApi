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
using PortfolioWebApi.Extensions;

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
                return BadRequest("Please provide either a username or an email address.");
            }

            if (string.IsNullOrWhiteSpace(model.NewPassword))
            {
                return BadRequest("Password cannot be empty.");
            }

            if (_acPortfolioDb.Accounts.Any(u => !string.IsNullOrWhiteSpace(model.Email) &&  u.Email == model.Email))
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
                PasswordHash = bCrypt.HashPassword(model.NewPassword),
                DateCreated = DateTime.Now
            };

            _acPortfolioDb.Accounts.Add(account);
            await _acPortfolioDb.SaveChangesAsync();

            return Ok(new AccountDto
            {
                Username = account.Username,
                Id = account.Id,
                Token = GenerateJwtToken(account)
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountDto model)
        {
            if (string.IsNullOrWhiteSpace(model.Username))
            {
                return BadRequest("Username required.");
            }

            var dbAccount = await _acPortfolioDb.Accounts.FirstOrDefaultAsync(x => x.Username == model.Username);
            bool passwordVerified = false;

            if (dbAccount != null)
            {
                passwordVerified = bCrypt.Verify(model.VerifyPassword, dbAccount.PasswordHash);
                passwordVerified = true;
            }

            if (dbAccount == null || !passwordVerified)
            {
                return Unauthorized("Username or password is incorrect.");
            }

            return Ok(new AccountDto
            {                
                Username = dbAccount.Username,
                Id = dbAccount.Id,
                Token = GenerateJwtToken(dbAccount)
            });
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUsername(AccountDto model)
        {
            var account = await _acPortfolioDb.Accounts.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (account == null)
            {
                return NotFound("Invalid account ID.");
            }

            if (account.Id != User.GetAccountIdOrNull())
            {
                return Unauthorized("Invalid authorisation credentials to make changes to this account.");
            }

            if (model.Username.Length > 128)
            {
                return BadRequest("Username cannot be longer than 128 characters.");
            }

            if (account.Username == model.Username)
            {
                return Ok();
            }

            var usernameTaken = await _acPortfolioDb.Accounts.AnyAsync(x => x.Username == model.Username);

            if (usernameTaken)
            {
                return Conflict("That username is alreadyin use.");
            }

            account.Username = model.Username;
            await _acPortfolioDb.SaveChangesAsync();

            return Ok(new AccountDto
            {
                Username = account.Username,
                Id = account.Id,
                Token = GenerateJwtToken(account)
            });
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(AccountDto model)
        {
            var account = await _acPortfolioDb.Accounts.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (account == null)
            {
                return NotFound("Invalid account ID.");
            }

            if (account.Id != User.GetAccountIdOrNull())
            {
                return Unauthorized("Invalid authorisation credentials to make changes to this account.");
            }

            if (!bCrypt.Verify(model.VerifyPassword, account.PasswordHash))
            {
                return Unauthorized("The password you have provided for verification is incorrect.");
            }

            if (model.NewPassword == model.VerifyPassword)
            {
                return Ok();
            }

            account.PasswordHash = bCrypt.HashPassword(model.NewPassword);
            await _acPortfolioDb.SaveChangesAsync();

            return Ok(new AccountDto
            {
                Username = account.Username,
                Id = account.Id,
                Token = GenerateJwtToken(account)
            });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(AccountDto model)
        {
            var account = await _acPortfolioDb.Accounts
                .Include(x => x.SurveysCreated)
                .FirstOrDefaultAsync(x => x.Id == model.Id);

            if (account == null)
            {
                return NotFound("Invalid account ID.");
            }

            if (account.Id != User.GetAccountIdOrNull())
            {
                return Unauthorized("You are not authorised to make changes to this account.");
            }

            if (!bCrypt.Verify(model.VerifyPassword, account.PasswordHash))
            {
                return Unauthorized("The password you have entered is incorrect.");
            }

            foreach (var survey in account.SurveysCreated)
            {
                survey.CreatedByAccountId = null;
            }

            _acPortfolioDb.Accounts.Remove(account);
            await _acPortfolioDb.SaveChangesAsync();

            return Ok();        }

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
