﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SampleStoreCQRS.Infra.Data.Contexts.Common.DataContext;

namespace SampleStoreCQRS.Infra.Data.Migrations
{
    [DbContext(typeof(SampleStoreCQRSDataContext))]
    partial class SampleStoreCQRSDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Phone");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateAt");

                    b.Property<Guid?>("CustomerId");

                    b.Property<string>("Number");

                    b.Property<Guid?>("PaymentId");

                    b.Property<int>("Status");

                    b.Property<DateTime>("UpdateAt");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.HasIndex("Number")
                        .IsUnique()
                        .HasFilter("[Number] IS NOT NULL");

                    b.HasIndex("PaymentId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models.OrderItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1024);

                    b.Property<Guid?>("OrderId");

                    b.Property<decimal>("Price");

                    b.Property<Guid?>("ProductId");

                    b.Property<decimal>("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1024);

                    b.Property<string>("Image");

                    b.Property<decimal>("Price");

                    b.Property<int>("QuantityOnHand");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(60);

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("SampleStoreCQRS.Domain.Contexts.Promotions.Models.Coupon", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Cod");

                    b.Property<decimal>("Percentage");

                    b.HasKey("Id");

                    b.HasIndex("Cod")
                        .IsUnique()
                        .HasFilter("[Cod] IS NOT NULL");

                    b.ToTable("Cupons");
                });

            modelBuilder.Entity("SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models.Customer", b =>
                {
                    b.OwnsOne("SampleStoreCQRS.Domain.Core.ValueObjects.Document", "Document", b1 =>
                        {
                            b1.Property<Guid>("CustomerId");

                            b1.Property<string>("Number")
                                .HasColumnName("Document.Number");

                            b1.HasKey("CustomerId");

                            b1.HasIndex("Number")
                                .IsUnique()
                                .HasFilter("[Document.Number] IS NOT NULL");

                            b1.ToTable("Customers");

                            b1.HasOne("SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models.Customer")
                                .WithOne("Document")
                                .HasForeignKey("SampleStoreCQRS.Domain.Core.ValueObjects.Document", "CustomerId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("SampleStoreCQRS.Domain.Core.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("CustomerId");

                            b1.Property<string>("Address")
                                .HasColumnName("Email.Address");

                            b1.HasKey("CustomerId");

                            b1.HasIndex("Address")
                                .IsUnique()
                                .HasFilter("[Email.Address] IS NOT NULL");

                            b1.ToTable("Customers");

                            b1.HasOne("SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models.Customer")
                                .WithOne("Email")
                                .HasForeignKey("SampleStoreCQRS.Domain.Core.ValueObjects.Email", "CustomerId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("SampleStoreCQRS.Domain.Core.ValueObjects.Name", "Name", b1 =>
                        {
                            b1.Property<Guid>("CustomerId");

                            b1.Property<string>("FirstName")
                                .HasColumnName("Name.FirstName");

                            b1.Property<string>("LastName")
                                .HasColumnName("Name.LastName");

                            b1.HasKey("CustomerId");

                            b1.ToTable("Customers");

                            b1.HasOne("SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models.Customer")
                                .WithOne("Name")
                                .HasForeignKey("SampleStoreCQRS.Domain.Core.ValueObjects.Name", "CustomerId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models.Order", b =>
                {
                    b.HasOne("SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models.Payment", "Payment")
                        .WithMany()
                        .HasForeignKey("PaymentId");

                    b.OwnsOne("SampleStoreCQRS.Domain.Core.ValueObjects.DiscountCupon", "DiscountCupon", b1 =>
                        {
                            b1.Property<Guid>("OrderId");

                            b1.Property<string>("Cod")
                                .HasColumnName("DiscountCupon.Cod");

                            b1.Property<decimal>("Percentage")
                                .HasColumnName("DiscountCupon.Percentage");

                            b1.HasKey("OrderId");

                            b1.ToTable("Orders");

                            b1.HasOne("SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models.Order")
                                .WithOne("DiscountCupon")
                                .HasForeignKey("SampleStoreCQRS.Domain.Core.ValueObjects.DiscountCupon", "OrderId")
                                .OnDelete(DeleteBehavior.Cascade);

                            b1.OwnsOne("SampleStoreCQRS.Domain.Core.ValueObjects.Period", "ValidadePeriod", b2 =>
                                {
                                    b2.Property<Guid>("DiscountCuponOrderId");

                                    b2.Property<DateTime>("End")
                                        .HasColumnName("DiscountCupon.ValidadePeriod.End");

                                    b2.Property<DateTime>("Start")
                                        .HasColumnName("DiscountCupon.ValidadePeriod.Start");

                                    b2.HasKey("DiscountCuponOrderId");

                                    b2.ToTable("Orders");

                                    b2.HasOne("SampleStoreCQRS.Domain.Core.ValueObjects.DiscountCupon")
                                        .WithOne("ValidadePeriod")
                                        .HasForeignKey("SampleStoreCQRS.Domain.Core.ValueObjects.Period", "DiscountCuponOrderId")
                                        .OnDelete(DeleteBehavior.Cascade);
                                });
                        });
                });

            modelBuilder.Entity("SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models.OrderItem", b =>
                {
                    b.HasOne("SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models.Order")
                        .WithMany("Items")
                        .HasForeignKey("OrderId");

                    b.HasOne("SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models.Payment", b =>
                {
                    b.OwnsOne("SampleStoreCQRS.Domain.Core.ValueObjects.CreditCard", "CreditCard", b1 =>
                        {
                            b1.Property<Guid>("PaymentId");

                            b1.Property<int>("Cvv")
                                .HasColumnName("CreditCard.Cvv");

                            b1.Property<string>("Number")
                                .HasColumnName("CreditCard.Number")
                                .HasColumnType("varchar(19)")
                                .HasMaxLength(19);

                            b1.Property<string>("PrintName")
                                .HasColumnName("CreditCard.PrintName")
                                .HasColumnType("varchar(100)")
                                .HasMaxLength(100);

                            b1.Property<string>("Validate")
                                .HasColumnName("CreditCard.Validate")
                                .HasColumnType("varchar(19)")
                                .HasMaxLength(19);

                            b1.HasKey("PaymentId");

                            b1.ToTable("Payments");

                            b1.HasOne("SampleStoreCQRS.Domain.Contexts.Checkout.Orders.Models.Payment")
                                .WithOne("CreditCard")
                                .HasForeignKey("SampleStoreCQRS.Domain.Core.ValueObjects.CreditCard", "PaymentId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("SampleStoreCQRS.Domain.Contexts.Promotions.Models.Coupon", b =>
                {
                    b.OwnsOne("SampleStoreCQRS.Domain.Core.ValueObjects.Period", "ValidadePeriod", b1 =>
                        {
                            b1.Property<Guid>("CouponId");

                            b1.Property<DateTime>("End")
                                .HasColumnName("ValidadePeriod.End");

                            b1.Property<DateTime>("Start")
                                .HasColumnName("ValidadePeriod.Start");

                            b1.HasKey("CouponId");

                            b1.ToTable("Cupons");

                            b1.HasOne("SampleStoreCQRS.Domain.Contexts.Promotions.Models.Coupon")
                                .WithOne("ValidadePeriod")
                                .HasForeignKey("SampleStoreCQRS.Domain.Core.ValueObjects.Period", "CouponId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });
#pragma warning restore 612, 618
        }
    }
}