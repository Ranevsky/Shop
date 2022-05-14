﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shop.Database;

#nullable disable

namespace Shop.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220514213304_change_image")]
    partial class change_image
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.4");

            modelBuilder.Entity("Shop.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("ProductId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("Shop.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Characteristic")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Popularity")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductType")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Warranty")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Shop.Models.Image", b =>
                {
                    b.HasOne("Shop.Models.Product", null)
                        .WithMany("Images")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("Shop.Models.Product", b =>
                {
                    b.Navigation("Images");
                });
#pragma warning restore 612, 618
        }
    }
}
