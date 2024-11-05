using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioWebApi.Models;

namespace PortfolioWebApi.Configurations
{
    internal class QuestionConfiguration
    {
        internal QuestionConfiguration(EntityTypeBuilder<Question> entityBuilder) 
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            entityBuilder.HasOne(q => q.Survey).WithMany(s => s.Questions).HasForeignKey(q => q.SurveyId).OnDelete(DeleteBehavior.Restrict);
            entityBuilder.HasMany(q => q.MultipleChoiceOptions).WithOne(a => a.Question).HasForeignKey(a => a.QuestionId).OnDelete(DeleteBehavior.Restrict);
            entityBuilder.HasMany(q => q.QuestionResponses).WithOne(qr => qr.Question).HasForeignKey(qr => qr.QuestionId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}