﻿// <auto-generated />
using System;
using AdessoRideShare.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AdessoRideShare.Data.Migrations
{
    [DbContext(typeof(AdessoRideShareContext))]
    [Migration("20210221102015_InitialModel")]
    partial class InitialModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AdessoRideShare.Core.City", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CityId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("AdessoRideShare.Core.Ride", b =>
                {
                    b.Property<int>("RideId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ArrivalCityId")
                        .HasColumnType("int");

                    b.Property<int?>("CreatedUserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DepartureCityId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FreeSeatCount")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("MaxSeatCount")
                        .HasColumnType("int");

                    b.HasKey("RideId");

                    b.HasIndex("ArrivalCityId");

                    b.HasIndex("CreatedUserId");

                    b.HasIndex("DepartureCityId");

                    b.ToTable("Rides");
                });

            modelBuilder.Entity("AdessoRideShare.Core.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CityRide", b =>
                {
                    b.Property<int>("RidesRideId")
                        .HasColumnType("int");

                    b.Property<int>("RouteCitiesCityId")
                        .HasColumnType("int");

                    b.HasKey("RidesRideId", "RouteCitiesCityId");

                    b.HasIndex("RouteCitiesCityId");

                    b.ToTable("RouteCities");
                });

            modelBuilder.Entity("RideUser", b =>
                {
                    b.Property<int>("JoinedRidesRideId")
                        .HasColumnType("int");

                    b.Property<int>("JoinedUsersUserId")
                        .HasColumnType("int");

                    b.HasKey("JoinedRidesRideId", "JoinedUsersUserId");

                    b.HasIndex("JoinedUsersUserId");

                    b.ToTable("RideUsers");
                });

            modelBuilder.Entity("AdessoRideShare.Core.Ride", b =>
                {
                    b.HasOne("AdessoRideShare.Core.City", "ArrivalCity")
                        .WithMany()
                        .HasForeignKey("ArrivalCityId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("AdessoRideShare.Core.User", "CreatedUser")
                        .WithMany("CreatedRides")
                        .HasForeignKey("CreatedUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("AdessoRideShare.Core.City", "DepartureCity")
                        .WithMany()
                        .HasForeignKey("DepartureCityId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("ArrivalCity");

                    b.Navigation("CreatedUser");

                    b.Navigation("DepartureCity");
                });

            modelBuilder.Entity("CityRide", b =>
                {
                    b.HasOne("AdessoRideShare.Core.Ride", null)
                        .WithMany()
                        .HasForeignKey("RidesRideId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdessoRideShare.Core.City", null)
                        .WithMany()
                        .HasForeignKey("RouteCitiesCityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RideUser", b =>
                {
                    b.HasOne("AdessoRideShare.Core.Ride", null)
                        .WithMany()
                        .HasForeignKey("JoinedRidesRideId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AdessoRideShare.Core.User", null)
                        .WithMany()
                        .HasForeignKey("JoinedUsersUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AdessoRideShare.Core.User", b =>
                {
                    b.Navigation("CreatedRides");
                });
#pragma warning restore 612, 618
        }
    }
}
