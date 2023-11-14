﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using skolesystem.Data;

#nullable disable

namespace skolesystem.Migrations.SkemaDb
{
    [DbContext(typeof(SkemaDbContext))]
    [Migration("20231113153153_Skema")]
    partial class Skema
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("skolesystem.Models.Skema", b =>
                {
                    b.Property<int>("schedule_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("class_id")
                        .HasColumnType("int");

                    b.Property<string>("day_of_week")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("end_time")
                        .HasColumnType("int");

                    b.Property<int>("start_time")
                        .HasColumnType("int");

                    b.Property<int>("user_subject_id")
                        .HasColumnType("int");

                    b.HasKey("schedule_id");

                    b.ToTable("Skema");
                });
#pragma warning restore 612, 618
        }
    }
}