using AdessoRideShare.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Data.Configurations
{
    public class RideConfiguration : IEntityTypeConfiguration<Ride>
    {
        public void Configure(EntityTypeBuilder<Ride> builder)
        {
            builder.HasKey(a => a.RideId);

            builder.Property(m => m.RideId).UseIdentityColumn();

            //builder.Property(m => m.DepartureCityId).IsRequired();

            //builder.Property(m => m.ArrivalCityId).IsRequired();

            builder.HasOne(r => r.ArrivalCity).WithMany().OnDelete(DeleteBehavior.SetNull).HasForeignKey(r => r.ArrivalCityId);

            builder.HasOne(r => r.DepartureCity).WithMany().OnDelete(DeleteBehavior.NoAction).HasForeignKey(r => r.DepartureCityId);

            builder.HasOne(r => r.CreatedUser).WithMany(r => r.CreatedRides).OnDelete(DeleteBehavior.Cascade).HasForeignKey(r => r.CreatedUserId);

            builder.HasMany(r => r.JoinedUsers).WithMany(r => r.JoinedRides).UsingEntity(r => r.ToTable("RideUsers"));

            builder.HasMany(r => r.RouteCities).WithMany(r => r.Rides).UsingEntity(r => r.ToTable("RouteCities"));

            builder.ToTable("Rides");
        }
    }
}
