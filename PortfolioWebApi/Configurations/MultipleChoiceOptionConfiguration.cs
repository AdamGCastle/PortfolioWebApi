using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioWebApi.Models;

namespace PortfolioWebApi.Configurations
{
    internal class MultipleChoiceOptionConfiguration
    {
        internal MultipleChoiceOptionConfiguration(EntityTypeBuilder<MultipleChoiceOption> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            entityBuilder.HasOne(m => m.Question).WithMany(q => q.MultipleChoiceOptions).HasForeignKey(m => m.QuestionId).OnDelete(DeleteBehavior.Restrict);
            entityBuilder.HasMany(m => m.MultipleChoiceOptionResponses).WithOne(mr => mr.MultipleChoiceOption).HasForeignKey(mr => mr.MultipleChoiceOptionId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}