﻿// <auto-generated />
using System;
using DuploParser.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DuploParser.Migrations
{
    [DbContext(typeof(AppDb))]
    partial class AppDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DuploParser.Models.Filter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Brand")
                        .HasColumnType("longtext");

                    b.Property<string>("Code")
                        .HasColumnType("longtext");

                    b.Property<bool?>("Pins")
                        .HasColumnType("tinyint(1)");

                    b.Property<double?>("Profile")
                        .HasColumnType("double");

                    b.Property<double?>("Radius")
                        .HasColumnType("double");

                    b.Property<bool?>("RunFlat")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Season")
                        .HasColumnType("longtext");

                    b.Property<double?>("Width")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("Filters");
                });
#pragma warning restore 612, 618
        }
    }
}