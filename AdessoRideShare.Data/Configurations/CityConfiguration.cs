using AdessoRideShare.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Data.Configurations
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(c => c.CityId);

            builder.Property(c => c.CityId).UseIdentityColumn();

            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);

            builder.ToTable("Cities");

        }
    }
}
