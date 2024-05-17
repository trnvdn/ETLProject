﻿// <auto-generated />
using System;
using ETL_Lib.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ETL_Lib.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ETL_Lib.Models.CabTrip", b =>
                {
                    b.Property<Guid>("TripID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DOLocationID")
                        .HasColumnType("int");

                    b.Property<DateTime>("DropoffDateTime")
                        .HasColumnType("datetime2");

                    b.Property<double>("FareAmount")
                        .HasColumnType("float");

                    b.Property<int>("PULocationID")
                        .HasColumnType("int");

                    b.Property<int>("PassengerCount")
                        .HasColumnType("int");

                    b.Property<DateTime>("PickupDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("StoreAndFwdFlag")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TipAmount")
                        .HasColumnType("float");

                    b.Property<double>("TripDistance")
                        .HasColumnType("float");

                    b.HasKey("TripID");

                    b.HasIndex("DropoffDateTime")
                        .HasDatabaseName("IX_CabTrip_DropoffDateTime");

                    b.HasIndex("PULocationID")
                        .HasDatabaseName("IX_CabTrip_PULocationID");

                    b.HasIndex("TripDistance")
                        .HasDatabaseName("IX_CabTrip_TripDistance");

                    b.ToTable("CabTrips");
                });
#pragma warning restore 612, 618
        }
    }
}
