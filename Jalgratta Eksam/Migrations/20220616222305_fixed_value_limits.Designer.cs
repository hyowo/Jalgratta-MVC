﻿// <auto-generated />
using Jalgratta_Eksam.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Jalgratta_Eksam.Migrations
{
    [DbContext(typeof(EksamidDBContext))]
    [Migration("20220616222305_fixed_value_limits")]
    partial class fixed_value_limits
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Jalgratta_Eksam.Models.Eksam", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Eesnimi")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<int>("Luba")
                        .HasColumnType("int");

                    b.Property<string>("Perekonnanimi")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<int>("Ring")
                        .HasColumnType("int");

                    b.Property<int>("Slaalom")
                        .HasColumnType("int");

                    b.Property<int>("Tanav")
                        .HasColumnType("int");

                    b.Property<int>("Teooria")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Eksamid");
                });
#pragma warning restore 612, 618
        }
    }
}
