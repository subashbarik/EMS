using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class CompanyConfiguration : BaseEntityConfigurations<Company>
    {
        public override void Configure(EntityTypeBuilder<Company> builder)
        {
            base.Configure(builder);
            builder.HasAlternateKey(p => p.Name);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
            builder.Property(p => p.LogoUrl).IsRequired();
            builder.Property(p => p.Address1).HasMaxLength(100);
            builder.Property(p => p.Address2).HasMaxLength(100);
            builder.Property(p => p.City).HasMaxLength(100);
            builder.Property(p => p.State).HasMaxLength(100);
            builder.Property(p => p.ZipCode).HasMaxLength(20);
            builder.Property(p => p.Country).HasMaxLength(100);
            builder.Property(p => p.Description).HasMaxLength(500);
            //builder.HasMany<Department>().WithOne();
            //builder.HasMany(d => d.Departments).WithOne();
        }
    }
}
