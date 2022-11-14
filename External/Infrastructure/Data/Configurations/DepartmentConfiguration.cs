using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class DepartmentConfiguration : BaseEntityConfigurations<Department>
    {
        public override void Configure(EntityTypeBuilder<Department> builder)
        {
            base.Configure(builder);
            builder.HasAlternateKey(p => p.Name);
            builder.Property(p => p.Name).IsRequired().HasMaxLength(20);
            builder.Property(p => p.CompanyId).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(500);
            // Company has one to many relationship with Department
            builder.HasOne(d => d.Company).WithMany(c => c.Departments).HasForeignKey(d => d.CompanyId);
            //builder.HasMany(e => e.Employees).WithOne();
        }
    }
}
