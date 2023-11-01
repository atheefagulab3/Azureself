﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using prj.Context;

#nullable disable

namespace prj.Migrations
{
    [DbContext(typeof(ProfileContext))]
    [Migration("20231030113344_inti")]
    partial class inti
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("prj.Model.AdditionalTraveller", b =>
                {
                    b.Property<int>("AdditionalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AdditionalId"));

                    b.Property<string>("AdditionalName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int?>("ProfileCustomerId")
                        .HasColumnType("int");

                    b.HasKey("AdditionalId");

                    b.HasIndex("ProfileCustomerId");

                    b.ToTable("additional_Travellers");
                });

            modelBuilder.Entity("prj.Model.UserProfile", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<DateTime>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MaritalStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("MobileNumber")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("profiles");
                });

            modelBuilder.Entity("prj.Model.AdditionalTraveller", b =>
                {
                    b.HasOne("prj.Model.UserProfile", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileCustomerId");

                    b.Navigation("Profile");
                });
#pragma warning restore 612, 618
        }
    }
}
