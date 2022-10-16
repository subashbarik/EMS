using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class AddressConfigurations : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.Property(p => p.Id).IsRequired().UseIdentityColumn();
            builder.Property(p => p.Address1).IsRequired();
            builder.Property(p => p.City).IsRequired();
            builder.Property(p => p.State).IsRequired();
            builder.Property(p => p.ZipCode).IsRequired();
            builder.Property(p => p.Country).IsRequired();
        }
    }
}
