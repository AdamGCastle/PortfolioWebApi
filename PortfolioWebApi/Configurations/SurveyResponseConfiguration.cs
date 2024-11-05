using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioWebApi.Models;

namespace PortfolioWebApi.Configurations
{
    internal class SurveyResponseConfiguration
    {
        internal SurveyResponseConfiguration(EntityTypeBuilder<SurveyResponse> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            entityBuilder.HasOne(sr => sr.Survey).WithMany(s => s.SurveyResponses).HasForeignKey(sr => sr.SurveyId).OnDelete(DeleteBehavior.Restrict);
            entityBuilder.HasMany(sr => sr.QuestionResponses).WithOne(qr => qr.SurveyResponse).HasForeignKey(qr => qr.SurveyResponseId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}