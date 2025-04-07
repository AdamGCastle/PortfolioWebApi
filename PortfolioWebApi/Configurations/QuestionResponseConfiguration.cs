using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioWebApi.Models;

namespace PortfolioWebApi.Configurations
{
    internal class QuestionResponseConfiguration
    {
        internal QuestionResponseConfiguration(EntityTypeBuilder<QuestionResponse> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            entityBuilder.HasOne(qr => qr.Question).WithMany(q => q.QuestionResponses).HasForeignKey(qr => qr.QuestionId).OnDelete(DeleteBehavior.NoAction);
            entityBuilder.HasOne(qr => qr.SurveyResponse).WithMany(sr => sr.QuestionResponses).HasForeignKey(qr => qr.SurveyResponseId).OnDelete(DeleteBehavior.Cascade);
            entityBuilder.HasMany(qr => qr.MultipleChoiceOptionResponses).WithOne(mr => mr.QuestionResponse).HasForeignKey(mr => mr.QuestionResponseId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}