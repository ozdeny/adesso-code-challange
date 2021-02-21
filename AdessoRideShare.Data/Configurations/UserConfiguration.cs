using AdessoRideShare.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdessoRideShare.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.UserId);

            builder.Property(u => u.UserId).UseIdentityColumn();

            builder.Property(m => m.Name).IsRequired().HasMaxLength(50);

            builder.ToTable("Users");
        }
    }
}
