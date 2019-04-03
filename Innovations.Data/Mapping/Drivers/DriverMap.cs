using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Innovations.Model.Schemas;

namespace Innovations.Data.Mapping
{
    public class DriverMap : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.ToTable("drivers")
               .HasKey(d => d.dni);
        }
    }
}
