using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(p => p.Id).IsRequired().UseIdentityColumn();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(20);
            builder.Property(p => p.CompanyId).IsRequired();
            // Company has one to many relationship with Department
            builder.HasOne(d => d.Company).WithMany(c => c.Departments).HasForeignKey(d => d.CompanyId);
            //builder.HasMany(e => e.Employees).WithOne();
        }
    }
}
