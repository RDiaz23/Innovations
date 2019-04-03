using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Innovations.Model.Schemas;

namespace Innovations.Data.Mapping
{
    public class InfractionMap : IEntityTypeConfiguration<Infraction>
    {
        public void Configure(EntityTypeBuilder<Infraction> builder)
        {
            builder.ToTable("infractions")
               .HasKey(i => i.id);
        }
    }
}
