﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using skolesystem.Data;

#nullable disable

namespace skolesystem.Migrations.UsersDb
{
    [DbContext(typeof(UsersDbContext))]
    partial class UsersDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("skolesystem.Models.Users", b =>
                {
                    b.Property<int>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("email_confirmed")
                        .HasColumnType("int");

                    b.Property<int>("is_deleted")
                        .HasColumnType("int");

                    b.Property<int>("lockout_enabled")
                        .HasColumnType("int");

                    b.Property<int>("lockout_end")
                        .HasColumnType("int");

                    b.Property<string>("password_hash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("phone_confirmed")
                        .HasColumnType("int");

                    b.Property<string>("surname")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("try_failed_count")
                        .HasColumnType("int");

                    b.Property<int>("twofactor_enabled")
                        .HasColumnType("int");

                    b.Property<int>("user_information_id")
                        .HasColumnType("int");

                    b.HasKey("user_id");

                    b.ToTable("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
