using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Innovations.Model.Schemas.Users;

namespace Innovations.Data.Mapping.Users
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("role")
                .HasKey(r => r.idrole);

        }
    }
}
