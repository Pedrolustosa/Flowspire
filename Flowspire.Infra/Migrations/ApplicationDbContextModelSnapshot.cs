﻿// <auto-generated />
using System;
using Flowspire.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Flowspire.Infra.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.13");

            modelBuilder.Entity("Flowspire.Domain.Entities.AdvisorCustomer", b =>
                {
                    b.Property<string>("AdvisorId")
                        .HasColumnType("TEXT");

                    b.Property<string>("CustomerId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("AssignedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("AdvisorId", "CustomerId");

                    b.HasIndex("CustomerId");

                    b.ToTable("AdvisorCustomers");

                    b.HasData(
                        new
                        {
                            AdvisorId = "8889e42f-da86-43f2-8352-bd2872ae8ef3",
                            CustomerId = "76081757-646f-4627-8233-91372046418b",
                            AssignedAt = new DateTime(2025, 2, 23, 20, 38, 51, 188, DateTimeKind.Utc).AddTicks(3610)
                        },
                        new
                        {
                            AdvisorId = "8889e42f-da86-43f2-8352-bd2872ae8ef3",
                            CustomerId = "c5ea5694-b0fe-4524-92ab-2a66020eeb36",
                            AssignedAt = new DateTime(2025, 2, 28, 20, 38, 51, 188, DateTimeKind.Utc).AddTicks(3612)
                        });
                });

            modelBuilder.Entity("Flowspire.Domain.Entities.Budget", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Budgets");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 200.00m,
                            CategoryId = 1,
                            EndDate = new DateTime(2025, 4, 4, 20, 38, 51, 188, DateTimeKind.Utc).AddTicks(3379),
                            StartDate = new DateTime(2025, 2, 3, 20, 38, 51, 188, DateTimeKind.Utc).AddTicks(3378),
                            UserId = "76081757-646f-4627-8233-91372046418b"
                        },
                        new
                        {
                            Id = 2,
                            Amount = 100.00m,
                            CategoryId = 3,
                            EndDate = new DateTime(2025, 4, 4, 20, 38, 51, 188, DateTimeKind.Utc).AddTicks(3385),
                            StartDate = new DateTime(2025, 2, 3, 20, 38, 51, 188, DateTimeKind.Utc).AddTicks(3384),
                            UserId = "c5ea5694-b0fe-4524-92ab-2a66020eeb36"
                        });
                });

            modelBuilder.Entity("Flowspire.Domain.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Alimentação",
                            UserId = "76081757-646f-4627-8233-91372046418b"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Transporte",
                            UserId = "76081757-646f-4627-8233-91372046418b"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Lazer",
                            UserId = "c5ea5694-b0fe-4524-92ab-2a66020eeb36"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Educação",
                            UserId = "c5ea5694-b0fe-4524-92ab-2a66020eeb36"
                        });
                });

            modelBuilder.Entity("Flowspire.Domain.Entities.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsRead")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ReceiverId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SenderId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("SentAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("Messages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Content = "Olá, preciso de ajuda com meu orçamento!",
                            IsRead = false,
                            ReceiverId = "8889e42f-da86-43f2-8352-bd2872ae8ef3",
                            SenderId = "76081757-646f-4627-8233-91372046418b",
                            SentAt = new DateTime(2025, 3, 5, 18, 38, 51, 188, DateTimeKind.Utc).AddTicks(3548)
                        },
                        new
                        {
                            Id = 2,
                            Content = "Claro, vamos analisar suas despesas.",
                            IsRead = true,
                            ReceiverId = "76081757-646f-4627-8233-91372046418b",
                            SenderId = "8889e42f-da86-43f2-8352-bd2872ae8ef3",
                            SentAt = new DateTime(2025, 3, 5, 19, 38, 51, 188, DateTimeKind.Utc).AddTicks(3555)
                        },
                        new
                        {
                            Id = 3,
                            Content = "Qual é o melhor investimento agora?",
                            IsRead = false,
                            ReceiverId = "8889e42f-da86-43f2-8352-bd2872ae8ef3",
                            SenderId = "c5ea5694-b0fe-4524-92ab-2a66020eeb36",
                            SentAt = new DateTime(2025, 3, 5, 17, 38, 51, 188, DateTimeKind.Utc).AddTicks(3556)
                        });
                });

            modelBuilder.Entity("Flowspire.Domain.Entities.RefreshToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsRevoked")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Flowspire.Domain.Entities.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.ToTable("Transactions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = -50.00m,
                            CategoryId = 1,
                            Date = new DateTime(2025, 2, 28, 20, 38, 51, 188, DateTimeKind.Utc).AddTicks(3272),
                            Description = "Supermercado",
                            UserId = "76081757-646f-4627-8233-91372046418b"
                        },
                        new
                        {
                            Id = 2,
                            Amount = -20.00m,
                            CategoryId = 2,
                            Date = new DateTime(2025, 3, 2, 20, 38, 51, 188, DateTimeKind.Utc).AddTicks(3290),
                            Description = "Uber",
                            UserId = "76081757-646f-4627-8233-91372046418b"
                        },
                        new
                        {
                            Id = 3,
                            Amount = -30.00m,
                            CategoryId = 3,
                            Date = new DateTime(2025, 3, 3, 20, 38, 51, 188, DateTimeKind.Utc).AddTicks(3310),
                            Description = "Cinema",
                            UserId = "c5ea5694-b0fe-4524-92ab-2a66020eeb36"
                        },
                        new
                        {
                            Id = 4,
                            Amount = 1000.00m,
                            CategoryId = 4,
                            Date = new DateTime(2025, 3, 4, 20, 38, 51, 188, DateTimeKind.Utc).AddTicks(3311),
                            Description = "Salário",
                            UserId = "c5ea5694-b0fe-4524-92ab-2a66020eeb36"
                        });
                });

            modelBuilder.Entity("Flowspire.Domain.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "04be54be-a2fd-41af-8ab7-85976fe92a2a",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "fa4bb30c-cd21-45da-a947-b44b8b5ffa81",
                            Email = "admin@flowspire.com",
                            EmailConfirmed = true,
                            FullName = "Admin User",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@FLOWSPIRE.COM",
                            NormalizedUserName = "ADMIN@FLOWSPIRE.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEHW5ZbwiVf3BVg5/cep6Nzqh4Hwa1XsC26Xxzf1fcW4K6k6nRtlH3Bo4VrPLyjRw6w==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "78756207-d4ee-4353-a3f1-85fbbe60e454",
                            TwoFactorEnabled = false,
                            UserName = "admin@flowspire.com"
                        },
                        new
                        {
                            Id = "8889e42f-da86-43f2-8352-bd2872ae8ef3",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "35b25954-cc51-459f-98eb-a66eddf006c8",
                            Email = "advisor@flowspire.com",
                            EmailConfirmed = true,
                            FullName = "Financial Advisor",
                            LockoutEnabled = false,
                            NormalizedEmail = "ADVISOR@FLOWSPIRE.COM",
                            NormalizedUserName = "ADVISOR@FLOWSPIRE.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEB6dOCsVvoUJ1CeTJB3cYDfMb5iXfRYeFGsX97lxT/ElG45QCQGuA/pc6p0NXRLJ/w==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "33a76f8c-500d-4d74-b29f-2f8b448b63bf",
                            TwoFactorEnabled = false,
                            UserName = "advisor@flowspire.com"
                        },
                        new
                        {
                            Id = "76081757-646f-4627-8233-91372046418b",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "eb2510ca-30e1-435b-8eac-cecf79e9f15a",
                            Email = "customer1@flowspire.com",
                            EmailConfirmed = true,
                            FullName = "Customer One",
                            LockoutEnabled = false,
                            NormalizedEmail = "CUSTOMER1@FLOWSPIRE.COM",
                            NormalizedUserName = "CUSTOMER1@FLOWSPIRE.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAENB5rZTKXJHimcdAtb8tbtq3j+WXctQAj3k44NduTh6H/oaxiNQLm5PytQE5kgd0iQ==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "fdc4b71c-ba5f-4d75-b005-1f8621ec2f52",
                            TwoFactorEnabled = false,
                            UserName = "customer1@flowspire.com"
                        },
                        new
                        {
                            Id = "c5ea5694-b0fe-4524-92ab-2a66020eeb36",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "baceea9d-57ce-4686-b01d-ac7171949d53",
                            Email = "customer2@flowspire.com",
                            EmailConfirmed = true,
                            FullName = "Customer Two",
                            LockoutEnabled = false,
                            NormalizedEmail = "CUSTOMER2@FLOWSPIRE.COM",
                            NormalizedUserName = "CUSTOMER2@FLOWSPIRE.COM",
                            PasswordHash = "AQAAAAIAAYagAAAAEDO+QJKdGmYy0JQY61Kv49Xmbd0MAjaKyO5w0jGsymCat1IORiJ5Y8CviR7JUmtkfA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "773c93e1-97ac-4984-bd17-d32f9813faf3",
                            TwoFactorEnabled = false,
                            UserName = "customer2@flowspire.com"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "c4717e83-2d66-49b7-b8cd-459ffa300769",
                            Name = "Administrator",
                            NormalizedName = "ADMINISTRATOR"
                        },
                        new
                        {
                            Id = "486e87cc-6243-45b3-9977-63fbabf8edb8",
                            Name = "FinancialAdvisor",
                            NormalizedName = "FINANCIALADVISOR"
                        },
                        new
                        {
                            Id = "21c5a76d-69e4-4858-8b21-7bb55dae3814",
                            Name = "Customer",
                            NormalizedName = "CUSTOMER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "04be54be-a2fd-41af-8ab7-85976fe92a2a",
                            RoleId = "c4717e83-2d66-49b7-b8cd-459ffa300769"
                        },
                        new
                        {
                            UserId = "8889e42f-da86-43f2-8352-bd2872ae8ef3",
                            RoleId = "486e87cc-6243-45b3-9977-63fbabf8edb8"
                        },
                        new
                        {
                            UserId = "76081757-646f-4627-8233-91372046418b",
                            RoleId = "21c5a76d-69e4-4858-8b21-7bb55dae3814"
                        },
                        new
                        {
                            UserId = "c5ea5694-b0fe-4524-92ab-2a66020eeb36",
                            RoleId = "21c5a76d-69e4-4858-8b21-7bb55dae3814"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Flowspire.Domain.Entities.AdvisorCustomer", b =>
                {
                    b.HasOne("Flowspire.Domain.Entities.User", "Advisor")
                        .WithMany()
                        .HasForeignKey("AdvisorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Flowspire.Domain.Entities.User", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Advisor");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Flowspire.Domain.Entities.Budget", b =>
                {
                    b.HasOne("Flowspire.Domain.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Flowspire.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Flowspire.Domain.Entities.Category", b =>
                {
                    b.HasOne("Flowspire.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Flowspire.Domain.Entities.Message", b =>
                {
                    b.HasOne("Flowspire.Domain.Entities.User", "Receiver")
                        .WithMany()
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Flowspire.Domain.Entities.User", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Flowspire.Domain.Entities.RefreshToken", b =>
                {
                    b.HasOne("Flowspire.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Flowspire.Domain.Entities.Transaction", b =>
                {
                    b.HasOne("Flowspire.Domain.Entities.Category", "Category")
                        .WithMany("Transactions")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Flowspire.Domain.Entities.User", "User")
                        .WithMany("Transactions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Flowspire.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Flowspire.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Flowspire.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Flowspire.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Flowspire.Domain.Entities.Category", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("Flowspire.Domain.Entities.User", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
