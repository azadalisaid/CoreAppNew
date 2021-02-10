﻿// <auto-generated />
using System;
using CoreApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CoreApp.Migrations
{
    [DbContext(typeof(PaymentDetailContext))]
    [Migration("20210126091432_updatequantitycolumnstokc1")]
    partial class updatequantitycolumnstokc1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("CoreApp.Models.CardType", b =>
                {
                    b.Property<int>("CardTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CardTypeName")
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("CardTypeId");

                    b.ToTable("CardTypes");
                });

            modelBuilder.Entity("CoreApp.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("CoreApp.Models.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CountryName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("StateId")
                        .HasColumnType("int");

                    b.HasKey("CountryId");

                    b.HasIndex("StateId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("CoreApp.Models.Menus", b =>
                {
                    b.Property<int>("MenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Path")
                        .HasColumnType("varchar(100)");

                    b.HasKey("MenuId");

                    b.ToTable("Menuss");
                });

            modelBuilder.Entity("CoreApp.Models.PaymentDetail", b =>
                {
                    b.Property<int>("PaymentDetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("CardNumber")
                        .HasColumnType("nvarchar(16)");

                    b.Property<string>("CardOwnerName")
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("CardTypeId")
                        .HasColumnType("int");

                    b.Property<string>("ExpirationDate")
                        .HasColumnType("nvarchar(5)");

                    b.Property<string>("SecurityCode")
                        .HasColumnType("nvarchar(3)");

                    b.HasKey("PaymentDetailId");

                    b.ToTable("PaymentDetails");
                });

            modelBuilder.Entity("CoreApp.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Price")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ProductCode")
                        .HasColumnType("nvarchar(20)");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("numeric(10,2)");

                    b.Property<int>("QuantityTypeId")
                        .HasColumnType("int");

                    b.Property<int>("SubCategoryId")
                        .HasColumnType("int");

                    b.Property<byte[]>("photo")
                        .HasColumnType("varbinary(MAX)");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("QuantityTypeId");

                    b.HasIndex("SubCategoryId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("CoreApp.Models.ProductStock", b =>
                {
                    b.Property<int>("ProductStockId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric(10,2)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("DateTime");

                    b.Property<int>("Multiply")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<decimal>("Quntity")
                        .HasColumnType("numeric(10,2)");

                    b.HasKey("ProductStockId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductStocks");
                });

            modelBuilder.Entity("CoreApp.Models.QuantityType", b =>
                {
                    b.Property<int>("QuantityTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("QuantityTypeName")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("QuantityTypeId");

                    b.ToTable("QuantityTypes");
                });

            modelBuilder.Entity("CoreApp.Models.Registration", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<int>("StateId")
                        .HasColumnType("int");

                    b.Property<string>("accountNo")
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("address")
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("city")
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("phoneNo")
                        .HasColumnType("nvarchar(20)");

                    b.Property<byte[]>("photo")
                        .HasColumnType("varbinary(MAX)");

                    b.Property<string>("street")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("id");

                    b.HasIndex("CountryId");

                    b.HasIndex("StateId");

                    b.ToTable("Registrations");
                });

            modelBuilder.Entity("CoreApp.Models.State", b =>
                {
                    b.Property<int>("StateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("StateName")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("StateId");

                    b.ToTable("States");
                });

            modelBuilder.Entity("CoreApp.Models.SubCategory", b =>
                {
                    b.Property<int>("SubCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("SubCategoryName")
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("SubCategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("SubCategories");
                });

            modelBuilder.Entity("CoreApp.Models.Country", b =>
                {
                    b.HasOne("CoreApp.Models.State", null)
                        .WithMany("Countries")
                        .HasForeignKey("StateId");
                });

            modelBuilder.Entity("CoreApp.Models.Product", b =>
                {
                    b.HasOne("CoreApp.Models.Category", "CategoryRel")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoreApp.Models.QuantityType", "QuantitiesRel")
                        .WithMany()
                        .HasForeignKey("QuantityTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoreApp.Models.SubCategory", "subCategoryRel")
                        .WithMany()
                        .HasForeignKey("SubCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoryRel");

                    b.Navigation("QuantitiesRel");

                    b.Navigation("subCategoryRel");
                });

            modelBuilder.Entity("CoreApp.Models.ProductStock", b =>
                {
                    b.HasOne("CoreApp.Models.Product", "products")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("products");
                });

            modelBuilder.Entity("CoreApp.Models.Registration", b =>
                {
                    b.HasOne("CoreApp.Models.Country", "countries")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoreApp.Models.State", "states")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("countries");

                    b.Navigation("states");
                });

            modelBuilder.Entity("CoreApp.Models.SubCategory", b =>
                {
                    b.HasOne("CoreApp.Models.Category", "CategoryRel")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoryRel");
                });

            modelBuilder.Entity("CoreApp.Models.State", b =>
                {
                    b.Navigation("Countries");
                });
#pragma warning restore 612, 618
        }
    }
}
