﻿// <auto-generated />
using System;
using KufarAnalyzer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KufarAnalyzer.Data.Migrations
{
    [DbContext(typeof(KufarContext))]
    [Migration("20240516182819_AddSubway")]
    partial class AddSubway
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("KufarAnalyzer.Data.Entities.KufarFlat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Floor")
                        .HasColumnType("int");

                    b.Property<double?>("Latitude")
                        .HasColumnType("float");

                    b.Property<double?>("Longitude")
                        .HasColumnType("float");

                    b.Property<double?>("PricePerSquareMeterUsd")
                        .HasColumnType("float");

                    b.Property<double>("PriceUsd")
                        .HasColumnType("float");

                    b.Property<int?>("RoomCount")
                        .HasColumnType("int");

                    b.Property<string>("Subway")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("KufarFlats");
                });
#pragma warning restore 612, 618
        }
    }
}
