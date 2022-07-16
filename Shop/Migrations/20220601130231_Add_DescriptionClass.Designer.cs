﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using Shop.Context;


#nullable disable

namespace Shop.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220601130231_Add_DescriptionClass")]
    partial class Add_DescriptionClass
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.4");

            modelBuilder.Entity("Shop.Models.Characteristic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("ProductId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Characteristics");
                });

            modelBuilder.Entity("Shop.Models.Description", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Description");
                });

            modelBuilder.Entity("Shop.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ProductId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Shop.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<int?>("DescriptionClassId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsStock")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Popularity")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<int>("TypeId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("WarrantyId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("DescriptionClassId");

                    b.HasIndex("TypeId");

                    b.HasIndex("WarrantyId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Shop.Models.ProductType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ProductTypes");
                });

            modelBuilder.Entity("Shop.Models.Warranty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Warranties");
                });

            modelBuilder.Entity("Shop.Models.Characteristic", b =>
                {
                    b.HasOne("Shop.Models.Product", null)
                        .WithMany("Characteristics")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("Shop.Models.Image", b =>
                {
                    b.HasOne("Shop.Models.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Shop.Models.Product", b =>
                {
                    b.HasOne("Shop.Models.Description", "DescriptionClass")
                        .WithMany()
                        .HasForeignKey("DescriptionClassId");

                    b.HasOne("Shop.Models.ProductType", "Type")
                        .WithMany("Products")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shop.Models.Warranty", "Warranty")
                        .WithMany("Products")
                        .HasForeignKey("WarrantyId");

                    b.Navigation("DescriptionClass");

                    b.Navigation("Type");

                    b.Navigation("Warranty");
                });

            modelBuilder.Entity("Shop.Models.Product", b =>
                {
                    b.Navigation("Characteristics");

                    b.Navigation("Images");
                });

            modelBuilder.Entity("Shop.Models.ProductType", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Shop.Models.Warranty", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
