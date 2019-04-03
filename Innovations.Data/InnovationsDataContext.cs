using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Innovations.Data.Mapping.Users;
using Innovations.Model.Schemas.Users;
using Innovations.Data.Mapping;
using Innovations.Model.Schemas;

namespace Innovations.Data
{
    public partial class InnovationsDataContext : DbContext
    {
        public InnovationsDataContext(DbContextOptions<InnovationsDataContext> options): base(options)
        {

        }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Infraction> Infractions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new DriverMap());
            modelBuilder.ApplyConfiguration(new CarMap());
            modelBuilder.ApplyConfiguration(new InfractionMap());
        }
    }
}
