using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(p => p.Id).IsRequired().UseIdentityColumn();
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(30);
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(30);
            builder.Property(p => p.Age).IsRequired();
            builder.Property(p => p.DepartmentId).IsRequired();
            builder.Property(p => p.DesignationId).IsRequired();
            // Employee has Many to One relationship with Department
            builder.HasOne(e => e.Department).WithMany(d => d.Employees).HasForeignKey(e => e.DepartmentId);
            // Employee has Many to One relationship with Designation
            builder.HasOne(e => e.Designation).WithMany(d => d.Employees).HasForeignKey(e => e.DesignationId);
            //Employee has One to One relationship with Address
            builder.HasOne(e => e.Address).WithOne(a => a.Employee).HasForeignKey<Address>(a => a.EmployeeId);
        }
    }
}
