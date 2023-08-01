﻿// <auto-generated />
using System;
using HastaneRandevuSistemi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HastaneRandevuSistemi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HastaneRandevuSistemi.Models.Doktor", b =>
                {
                    b.Property<int>("DoktorID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DoktorID"));

                    b.Property<string>("DoktorAd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DoktorSoyad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("HastaneID")
                        .HasColumnType("int");

                    b.Property<int?>("PoliklinikID")
                        .HasColumnType("int");

                    b.HasKey("DoktorID");

                    b.HasIndex("HastaneID");

                    b.HasIndex("PoliklinikID");

                    b.ToTable("DoktorTable");
                });

            modelBuilder.Entity("HastaneRandevuSistemi.Models.Hastane", b =>
                {
                    b.Property<int>("HastaneID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HastaneID"));

                    b.Property<string>("HastaneAd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HastaneID");

                    b.ToTable("HastaneTable");
                });

            modelBuilder.Entity("HastaneRandevuSistemi.Models.Poliklinik", b =>
                {
                    b.Property<int>("PoliklinikID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PoliklinikID"));

                    b.Property<int?>("HastaneID")
                        .HasColumnType("int");

                    b.Property<string>("PoliklinikAd")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PoliklinikID");

                    b.HasIndex("HastaneID");

                    b.ToTable("PoliklinikTable");
                });

            modelBuilder.Entity("HastaneRandevuSistemi.Models.Randevu", b =>
                {
                    b.Property<int>("RandevuID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RandevuID"));

                    b.Property<int?>("DoktorID")
                        .HasColumnType("int");

                    b.Property<int?>("HastaneID")
                        .HasColumnType("int");

                    b.Property<int?>("PoliklinikID")
                        .HasColumnType("int");

                    b.Property<string>("RandevuGun")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RandevuSaat")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RandevuID");

                    b.HasIndex("DoktorID");

                    b.HasIndex("HastaneID");

                    b.HasIndex("PoliklinikID");

                    b.ToTable("RandevuTable");
                });

            modelBuilder.Entity("HastaneRandevuSistemi.Models.Doktor", b =>
                {
                    b.HasOne("HastaneRandevuSistemi.Models.Hastane", "Hastane")
                        .WithMany("DoktorList")
                        .HasForeignKey("HastaneID");

                    b.HasOne("HastaneRandevuSistemi.Models.Poliklinik", "Poliklinik")
                        .WithMany("DoktorList")
                        .HasForeignKey("PoliklinikID");

                    b.Navigation("Hastane");

                    b.Navigation("Poliklinik");
                });

            modelBuilder.Entity("HastaneRandevuSistemi.Models.Poliklinik", b =>
                {
                    b.HasOne("HastaneRandevuSistemi.Models.Hastane", "Hastane")
                        .WithMany("PoliklinikList")
                        .HasForeignKey("HastaneID");

                    b.Navigation("Hastane");
                });

            modelBuilder.Entity("HastaneRandevuSistemi.Models.Randevu", b =>
                {
                    b.HasOne("HastaneRandevuSistemi.Models.Doktor", "Doktor")
                        .WithMany("RandevuList")
                        .HasForeignKey("DoktorID");

                    b.HasOne("HastaneRandevuSistemi.Models.Hastane", "Hastane")
                        .WithMany("RandevuList")
                        .HasForeignKey("HastaneID");

                    b.HasOne("HastaneRandevuSistemi.Models.Poliklinik", "Poliklinik")
                        .WithMany("RandevuList")
                        .HasForeignKey("PoliklinikID");

                    b.Navigation("Doktor");

                    b.Navigation("Hastane");

                    b.Navigation("Poliklinik");
                });

            modelBuilder.Entity("HastaneRandevuSistemi.Models.Doktor", b =>
                {
                    b.Navigation("RandevuList");
                });

            modelBuilder.Entity("HastaneRandevuSistemi.Models.Hastane", b =>
                {
                    b.Navigation("DoktorList");

                    b.Navigation("PoliklinikList");

                    b.Navigation("RandevuList");
                });

            modelBuilder.Entity("HastaneRandevuSistemi.Models.Poliklinik", b =>
                {
                    b.Navigation("DoktorList");

                    b.Navigation("RandevuList");
                });
#pragma warning restore 612, 618
        }
    }
}