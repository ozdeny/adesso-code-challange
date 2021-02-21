using System;
using System.Collections.Generic;
using System.Text;
using AdessoRideShare.Core;
using AdessoRideShare.Data.Configurations;
using Microsoft.EntityFrameworkCore;



namespace AdessoRideShare.Data
{
    public class AdessoRideShareContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Ride> Rides { get; set; }

        public AdessoRideShareContext(DbContextOptions<AdessoRideShareContext> options) :
            base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CityConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new RideConfiguration());
        }
    }
}
