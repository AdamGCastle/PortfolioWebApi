using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PortfolioWebApi.Models;

namespace PortfolioWebApi.Configurations
{
    internal class AccountConfiguration
    {
        internal AccountConfiguration(EntityTypeBuilder<Account> entityBuilder) 
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();

            entityBuilder.HasMany(a => a.SurveysCreated).WithOne(s => s.CreatedByAccount).HasForeignKey(s => s.CreatedByAccountId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
