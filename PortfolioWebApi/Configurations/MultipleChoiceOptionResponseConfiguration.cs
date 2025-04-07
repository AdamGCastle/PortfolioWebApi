using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioWebApi.Models;

namespace PortfolioWebApi.Configurations
{
    internal class MultipleChoiceOptionResponseConfiguration
    {
        internal MultipleChoiceOptionResponseConfiguration(EntityTypeBuilder<MultipleChoiceOptionResponse> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            entityBuilder.HasOne(m => m.QuestionResponse).WithMany(q => q.MultipleChoiceOptionResponses).HasForeignKey(m => m.QuestionResponseId).OnDelete(DeleteBehavior.Cascade);
            entityBuilder.HasOne(mr => mr.MultipleChoiceOption).WithMany(m => m.MultipleChoiceOptionResponses).HasForeignKey(mr => mr.MultipleChoiceOptionId).OnDelete(DeleteBehavior.Cascade);
        }        
    }
}
