﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Reservation.Data;

namespace Reservation.Data.Migrations
{
    [DbContext(typeof(ReservationContext))]
    [Migration("20210204115128_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbo")
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("Reservation.Domains.Entities.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .UseIdentityColumn();

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("birthDate");

                    b.Property<int>("ContactTypeId")
                        .HasColumnType("int")
                        .HasColumnName("contactTypeId");

                    b.Property<string>("Logo")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("logo");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("name");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("phone");

                    b.HasKey("Id");

                    b.HasIndex("ContactTypeId");

                    b.ToTable("Contact");
                });

            modelBuilder.Entity("Reservation.Domains.Entities.ContactType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("description");

                    b.HasKey("Id");

                    b.ToTable("ContactType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Phisical"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Company"
                        });
                });

            modelBuilder.Entity("Reservation.Domains.Entities.Reserve", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .UseIdentityColumn();

                    b.Property<int>("ContactId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("description");

                    b.Property<bool>("Favorite")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("favorite");

                    b.Property<int>("Ranking")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0)
                        .HasColumnName("ranking");

                    b.Property<DateTime>("Schedule")
                        .HasColumnType("datetime2")
                        .HasColumnName("schedule");

                    b.HasKey("Id");

                    b.HasIndex("ContactId");

                    b.ToTable("Reserve");
                });

            modelBuilder.Entity("Reservation.Domains.Entities.Contact", b =>
                {
                    b.HasOne("Reservation.Domains.Entities.ContactType", "ContactType")
                        .WithMany("Contacts")
                        .HasForeignKey("ContactTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ContactType");
                });

            modelBuilder.Entity("Reservation.Domains.Entities.Reserve", b =>
                {
                    b.HasOne("Reservation.Domains.Entities.Contact", "Contact")
                        .WithMany("Reservs")
                        .HasForeignKey("ContactId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contact");
                });

            modelBuilder.Entity("Reservation.Domains.Entities.Contact", b =>
                {
                    b.Navigation("Reservs");
                });

            modelBuilder.Entity("Reservation.Domains.Entities.ContactType", b =>
                {
                    b.Navigation("Contacts");
                });
#pragma warning restore 612, 618
        }
    }
}
