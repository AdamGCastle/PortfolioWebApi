using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioWebApi.Models;

namespace PortfolioWebApi.Configurations
{
    internal class SurveyConfiguration
    {
        internal SurveyConfiguration(EntityTypeBuilder<Survey> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            entityBuilder.HasMany(s => s.Questions).WithOne(q => q.Survey).HasForeignKey(q => q.SurveyId).OnDelete(DeleteBehavior.Restrict);
            entityBuilder.HasMany(s => s.SurveyResponses).WithOne(sr => sr.Survey).HasForeignKey(sr => sr.SurveyId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}