﻿// <auto-generated />
using System;
using Garage3.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Garage3.Migrations
{
    [DbContext(typeof(Garage3Context))]
    [Migration("20210326103413_Test")]
    partial class Test
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Garage3.Models.Booking", b =>
                {
                    b.Property<int>("BookingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BookedByMemberID")
                        .HasColumnType("int");

                    b.Property<DateTime>("BookedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("BookingId");

                    b.HasIndex("BookedByMemberID");

                    b.ToTable("Booking");
                });

            modelBuilder.Entity("Garage3.Models.Member", b =>
                {
                    b.Property<int>("MemberID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("ExtendedMemberShipEndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Joined")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MembershipTypeType")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PersonalIdentityNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MemberID");

                    b.HasIndex("MembershipTypeType");

                    b.ToTable("Member");
                });

            modelBuilder.Entity("Garage3.Models.MembershipType", b =>
                {
                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Discount")
                        .HasColumnType("decimal(18,4)");

                    b.HasKey("Type");

                    b.ToTable("MembershipType");
                });

            modelBuilder.Entity("Garage3.Models.ParkingSpace", b =>
                {
                    b.Property<int>("ParkingSpaceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("ParkingSpaceID");

                    b.ToTable("ParkingSpace");
                });

            modelBuilder.Entity("Garage3.Models.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("LicenseNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("NumberOfWheels")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("OwnerMemberID")
                        .HasColumnType("int");

                    b.Property<string>("VehicleTypeType")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerMemberID");

                    b.HasIndex("VehicleTypeType");

                    b.ToTable("Vehicle");
                });

            modelBuilder.Entity("Garage3.Models.VehicleType", b =>
                {
                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.HasKey("Type");

                    b.ToTable("VehicleTypes");
                });

            modelBuilder.Entity("ParkingSpaceVehicle", b =>
                {
                    b.Property<int>("ParkedAtParkingSpaceID")
                        .HasColumnType("int");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("ParkedAtParkingSpaceID", "VehicleId");

                    b.HasIndex("VehicleId");

                    b.ToTable("ParkingSpaceVehicle");
                });

            modelBuilder.Entity("Garage3.Models.Booking", b =>
                {
                    b.HasOne("Garage3.Models.Member", "BookedBy")
                        .WithMany()
                        .HasForeignKey("BookedByMemberID");

                    b.Navigation("BookedBy");
                });

            modelBuilder.Entity("Garage3.Models.Member", b =>
                {
                    b.HasOne("Garage3.Models.MembershipType", "MembershipType")
                        .WithMany()
                        .HasForeignKey("MembershipTypeType");

                    b.Navigation("MembershipType");
                });

            modelBuilder.Entity("Garage3.Models.Vehicle", b =>
                {
                    b.HasOne("Garage3.Models.Member", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerMemberID");

                    b.HasOne("Garage3.Models.VehicleType", "VehicleType")
                        .WithMany()
                        .HasForeignKey("VehicleTypeType");

                    b.Navigation("Owner");

                    b.Navigation("VehicleType");
                });

            modelBuilder.Entity("ParkingSpaceVehicle", b =>
                {
                    b.HasOne("Garage3.Models.ParkingSpace", null)
                        .WithMany()
                        .HasForeignKey("ParkedAtParkingSpaceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Garage3.Models.Vehicle", null)
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
