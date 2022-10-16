using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.Property(p => p.Id).IsRequired().UseIdentityColumn();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.LogoUrl).IsRequired();
            //builder.HasMany<Department>().WithOne();
            //builder.HasMany(d => d.Departments).WithOne();
        }
    }
}
