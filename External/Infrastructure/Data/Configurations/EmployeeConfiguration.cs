using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class EmployeeConfiguration : BaseEntityConfigurations<Employee>
    {
        public override void Configure(EntityTypeBuilder<Employee> builder)
        {
            base.Configure(builder);
            builder.Property(p => p.UserID).HasMaxLength(450);   
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(30);
            builder.Property(p => p.MiddleName).HasMaxLength(30);
            builder.Property(p => p.LastName).IsRequired().HasMaxLength(30);
            builder.Property(p => p.Name).HasComputedColumnSql("[FirstName] + ' ' + [MiddleName] + ' ' + [LastName]");
            builder.Property(p => p.Age).HasMaxLength(3);
            builder.Property(p => p.HireDate).HasDefaultValueSql("getdate()");
            builder.Property(p => p.Sex).HasMaxLength(1);
            builder.Property(p => p.Basic).HasDefaultValue(0);
            builder.Property(p => p.TAPercentage).HasDefaultValue(0);
            builder.Property(p => p.DAPercentage).HasDefaultValue(0);
            builder.Property(p => p.HRAPercentage).HasDefaultValue(0);
            builder.Property(p => p.Address1).HasMaxLength(100);
            builder.Property(p => p.Address2).HasMaxLength(100);
            builder.Property(p => p.City).HasMaxLength(100);
            builder.Property(p => p.State).HasMaxLength(100);
            builder.Property(p => p.ZipCode).HasMaxLength(20);
            builder.Property(p => p.Country).HasMaxLength(100);
            builder.Property(p => p.Description).HasMaxLength(500);
            builder.Property(p => p.EmployeeTypeId).IsRequired();
            builder.Property(p => p.DepartmentId).IsRequired();
            builder.Property(p => p.DesignationId).IsRequired();
            // Employee has Many to One relationship with Department
            builder.HasOne(e => e.Department).WithMany(d => d.Employees).HasForeignKey(e => e.DepartmentId);
            // Employee has Many to One relationship with Designation
            builder.HasOne(e => e.Designation).WithMany(d => d.Employees).HasForeignKey(e => e.DesignationId);
            //Employee has One to One relationship with Address
            //builder.HasOne(e => e.Address).WithOne(a => a.Employee).HasForeignKey<Address>(a => a.EmployeeId);
        }
    }
}
