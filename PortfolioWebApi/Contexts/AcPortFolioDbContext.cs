using Microsoft.EntityFrameworkCore;
using PortfolioWebApi.Configurations;
using PortfolioWebApi.Extensions;
using PortfolioWebApi.Models;

namespace PortfolioWebApi.Contexts
{
    internal class AcPortFolioDbContext : DbContext
    {
        readonly static IConfigurationSection _section = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build()
            .GetSection("ConnectionStrings");

        internal DbSet<Survey> Surveys { get; set; }
        internal DbSet<SurveyResponse> SurveyResponses { get; set; }
        internal DbSet<Question> Questions { get; set; }
        internal DbSet<QuestionResponse> QuestionResponses { get; set; }
        internal DbSet<MultipleChoiceOption> MultipleChoiceOptions { get; set; }
        internal DbSet<MultipleChoiceOptionResponse> MultipleChoiceOptionResponses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new SurveyConfiguration(modelBuilder.Entity<Survey>());
            new SurveyResponseConfiguration(modelBuilder.Entity<SurveyResponse>());
            new QuestionConfiguration(modelBuilder.Entity<Question>());
            new QuestionResponseConfiguration(modelBuilder.Entity<QuestionResponse>());
            new MultipleChoiceOptionConfiguration(modelBuilder.Entity<MultipleChoiceOption>());
            new MultipleChoiceOptionResponseConfiguration(modelBuilder.Entity<MultipleChoiceOptionResponse>());
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (_section["DefaultConnection"].ToStringOrEmpty().Contains("localhost"))
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=AcPortfolio;Trusted_Connection=True;MultipleActiveResultSets=true");
                return;
            }

            optionsBuilder.UseSqlServer($"Server=tcp:acprojects.database.windows.net,1433;Initial Catalog=AcPortfolioDb;Persist Security Info=False;User ID={_section["Username"]};Password={_section["Password"]}" +
                    ";MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
}