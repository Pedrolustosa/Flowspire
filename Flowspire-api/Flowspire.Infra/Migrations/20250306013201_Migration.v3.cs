using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Flowspire.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Migrationv3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdvisorCustomers",
                columns: table => new
                {
                    AdvisorId = table.Column<string>(type: "TEXT", nullable: false),
                    CustomerId = table.Column<string>(type: "TEXT", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvisorCustomers", x => new { x.AdvisorId, x.CustomerId });
                    table.ForeignKey(
                        name: "FK_AdvisorCustomers_AspNetUsers_AdvisorId",
                        column: x => x.AdvisorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdvisorCustomers_AspNetUsers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SenderId = table.Column<string>(type: "TEXT", nullable: false),
                    ReceiverId = table.Column<string>(type: "TEXT", nullable: false),
                    Content = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    SentAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsRead = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Token = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Expires = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsRevoked = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Budgets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budgets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Budgets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Budgets_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transactions_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "660e8400-e29b-41d4-a716-446655440001", null, "Administrator", "ADMINISTRATOR" },
                    { "660e8400-e29b-41d4-a716-446655440002", null, "FinancialAdvisor", "FINANCIALADVISOR" },
                    { "660e8400-e29b-41d4-a716-446655440003", null, "Customer", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FullName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "550e8400-e29b-41d4-a716-446655440001", 0, null, "admin@flowspire.com", true, "Admin User", false, null, "ADMIN@FLOWSPIRE.COM", "ADMIN@FLOWSPIRE.COM", "AQAAAAIAAYagAAAAENKIF7gxkoyhwjzx0Y5CRudQxl8D7KtXXupapeUCg6ZUw7aKZeF7JITq52WfIMlctg==", null, false, "5eb9a427-a867-4844-b0f2-b318ba4ce273", false, "admin@flowspire.com" },
                    { "550e8400-e29b-41d4-a716-446655440002", 0, null, "advisor1@flowspire.com", true, "Advisor One", false, null, "ADVISOR1@FLOWSPIRE.COM", "ADVISOR1@FLOWSPIRE.COM", "AQAAAAIAAYagAAAAEIlUS4bXTv3jODtSWuP3/DYVNC7XN3WuDLhD6ZVcUX2o4niAlGGFwzI/+c5OOyrF4g==", null, false, "e6e5a48c-109f-4712-a239-89f941208eb3", false, "advisor1@flowspire.com" },
                    { "550e8400-e29b-41d4-a716-446655440003", 0, null, "advisor2@flowspire.com", true, "Advisor Two", false, null, "ADVISOR2@FLOWSPIRE.COM", "ADVISOR2@FLOWSPIRE.COM", "AQAAAAIAAYagAAAAEBHUGfH/RlwvjfgbQLUr7DGFbUhqRqdz4TNBF6Fj7PDUZUHElncUgTfJf52EqzDXLA==", null, false, "bff49fb9-01cf-4bf1-91a3-71049170ac83", false, "advisor2@flowspire.com" },
                    { "550e8400-e29b-41d4-a716-446655440004", 0, null, "customer1@flowspire.com", true, "Customer One", false, null, "CUSTOMER1@FLOWSPIRE.COM", "CUSTOMER1@FLOWSPIRE.COM", "AQAAAAIAAYagAAAAEGTCbw1TrPnY7AsjuOfBZdjsMGKjuCVneqjMayEpC68gzBTnOfau5mNcCpXTGgH/7g==", null, false, "15b344a1-47bf-480b-a027-73a4332ac100", false, "customer1@flowspire.com" },
                    { "550e8400-e29b-41d4-a716-446655440005", 0, null, "customer2@flowspire.com", true, "Customer Two", false, null, "CUSTOMER2@FLOWSPIRE.COM", "CUSTOMER2@FLOWSPIRE.COM", "AQAAAAIAAYagAAAAEEo0gbdxE13Tfcg3lMjAhhW1UuorzUbC4TB+4p8aJ2xB7UZMflmhICiiENV68WmYng==", null, false, "3f6675f4-c14c-44ea-a2a6-dd4e73dc310e", false, "customer2@flowspire.com" },
                    { "550e8400-e29b-41d4-a716-446655440006", 0, null, "customer3@flowspire.com", true, "Customer Three", false, null, "CUSTOMER3@FLOWSPIRE.COM", "CUSTOMER3@FLOWSPIRE.COM", "AQAAAAIAAYagAAAAENA8MXRiJVad44ANtMfglUb+UC27MAd7Evs/NpuopYg6L8aeKup38M7Y8+GkANVBOA==", null, false, "1c547459-021f-479e-81d5-58db0006da86", false, "customer3@flowspire.com" }
                });

            migrationBuilder.InsertData(
                table: "AdvisorCustomers",
                columns: new[] { "AdvisorId", "CustomerId", "AssignedAt" },
                values: new object[,]
                {
                    { "550e8400-e29b-41d4-a716-446655440002", "550e8400-e29b-41d4-a716-446655440004", new DateTime(2025, 2, 14, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5134) },
                    { "550e8400-e29b-41d4-a716-446655440002", "550e8400-e29b-41d4-a716-446655440005", new DateTime(2025, 2, 19, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5134) },
                    { "550e8400-e29b-41d4-a716-446655440003", "550e8400-e29b-41d4-a716-446655440006", new DateTime(2025, 2, 24, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5134) }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "660e8400-e29b-41d4-a716-446655440001", "550e8400-e29b-41d4-a716-446655440001" },
                    { "660e8400-e29b-41d4-a716-446655440002", "550e8400-e29b-41d4-a716-446655440002" },
                    { "660e8400-e29b-41d4-a716-446655440002", "550e8400-e29b-41d4-a716-446655440003" },
                    { "660e8400-e29b-41d4-a716-446655440003", "550e8400-e29b-41d4-a716-446655440004" },
                    { "660e8400-e29b-41d4-a716-446655440003", "550e8400-e29b-41d4-a716-446655440005" },
                    { "660e8400-e29b-41d4-a716-446655440003", "550e8400-e29b-41d4-a716-446655440006" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, "Alimentação", "550e8400-e29b-41d4-a716-446655440004" },
                    { 2, "Transporte", "550e8400-e29b-41d4-a716-446655440004" },
                    { 3, "Saúde", "550e8400-e29b-41d4-a716-446655440004" },
                    { 4, "Moradia", "550e8400-e29b-41d4-a716-446655440004" },
                    { 5, "Lazer", "550e8400-e29b-41d4-a716-446655440005" },
                    { 6, "Educação", "550e8400-e29b-41d4-a716-446655440005" },
                    { 7, "Vestuário", "550e8400-e29b-41d4-a716-446655440005" },
                    { 8, "Alimentação", "550e8400-e29b-41d4-a716-446655440006" },
                    { 9, "Tecnologia", "550e8400-e29b-41d4-a716-446655440006" },
                    { 10, "Lazer", "550e8400-e29b-41d4-a716-446655440006" }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "Content", "IsRead", "ReceiverId", "SenderId", "SentAt" },
                values: new object[,]
                {
                    { 1, "Olá, preciso de ajuda com meu orçamento!", false, "550e8400-e29b-41d4-a716-446655440002", "550e8400-e29b-41d4-a716-446655440004", new DateTime(2025, 2, 24, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5078) },
                    { 2, "Claro, vamos analisar suas despesas.", true, "550e8400-e29b-41d4-a716-446655440004", "550e8400-e29b-41d4-a716-446655440002", new DateTime(2025, 2, 25, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5078) },
                    { 3, "Qual é o melhor investimento agora?", false, "550e8400-e29b-41d4-a716-446655440002", "550e8400-e29b-41d4-a716-446655440005", new DateTime(2025, 3, 1, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5078) },
                    { 4, "Recomendo fundos de índice.", true, "550e8400-e29b-41d4-a716-446655440005", "550e8400-e29b-41d4-a716-446655440002", new DateTime(2025, 3, 2, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5078) },
                    { 5, "Como reduzir meus gastos?", false, "550e8400-e29b-41d4-a716-446655440003", "550e8400-e29b-41d4-a716-446655440006", new DateTime(2025, 3, 3, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5078) },
                    { 6, "Vamos revisar suas categorias.", false, "550e8400-e29b-41d4-a716-446655440006", "550e8400-e29b-41d4-a716-446655440003", new DateTime(2025, 3, 4, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5078) }
                });

            migrationBuilder.InsertData(
                table: "Budgets",
                columns: new[] { "Id", "Amount", "CategoryId", "EndDate", "StartDate", "UserId" },
                values: new object[,]
                {
                    { 1, 300.00m, 1, new DateTime(2025, 4, 6, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5000), new DateTime(2024, 12, 6, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5000), "550e8400-e29b-41d4-a716-446655440004" },
                    { 2, 200.00m, 3, new DateTime(2025, 4, 6, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5000), new DateTime(2024, 12, 6, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5000), "550e8400-e29b-41d4-a716-446655440004" },
                    { 3, 1000.00m, 4, new DateTime(2025, 4, 6, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5000), new DateTime(2024, 12, 6, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5000), "550e8400-e29b-41d4-a716-446655440004" },
                    { 4, 150.00m, 5, new DateTime(2025, 4, 6, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5000), new DateTime(2024, 12, 6, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5000), "550e8400-e29b-41d4-a716-446655440005" },
                    { 5, 400.00m, 6, new DateTime(2025, 4, 6, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5000), new DateTime(2024, 12, 6, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5000), "550e8400-e29b-41d4-a716-446655440005" },
                    { 6, 250.00m, 8, new DateTime(2025, 4, 6, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5000), new DateTime(2024, 12, 6, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5000), "550e8400-e29b-41d4-a716-446655440006" },
                    { 7, 100.00m, 10, new DateTime(2025, 4, 6, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5000), new DateTime(2024, 12, 6, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(5000), "550e8400-e29b-41d4-a716-446655440006" }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "Amount", "CategoryId", "Date", "Description", "UserId" },
                values: new object[,]
                {
                    { 1, -80.00m, 1, new DateTime(2024, 12, 22, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Supermercado", "550e8400-e29b-41d4-a716-446655440004" },
                    { 2, -15.00m, 2, new DateTime(2024, 12, 27, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Ônibus", "550e8400-e29b-41d4-a716-446655440004" },
                    { 3, 2000.00m, 1, new DateTime(2025, 1, 5, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Salário", "550e8400-e29b-41d4-a716-446655440004" },
                    { 4, -50.00m, 3, new DateTime(2025, 2, 1, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Farmácia", "550e8400-e29b-41d4-a716-446655440004" },
                    { 5, -800.00m, 4, new DateTime(2025, 2, 3, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Aluguel", "550e8400-e29b-41d4-a716-446655440004" },
                    { 6, -120.00m, 1, new DateTime(2025, 1, 27, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Supermercado", "550e8400-e29b-41d4-a716-446655440004" },
                    { 7, -60.00m, 1, new DateTime(2025, 2, 19, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Supermercado", "550e8400-e29b-41d4-a716-446655440004" },
                    { 8, -25.00m, 2, new DateTime(2025, 2, 24, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Uber", "550e8400-e29b-41d4-a716-446655440004" },
                    { 9, 2200.00m, 1, new DateTime(2025, 3, 5, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Salário", "550e8400-e29b-41d4-a716-446655440004" },
                    { 10, -40.00m, 5, new DateTime(2024, 12, 30, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Cinema", "550e8400-e29b-41d4-a716-446655440005" },
                    { 11, -150.00m, 6, new DateTime(2025, 1, 3, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Curso Online", "550e8400-e29b-41d4-a716-446655440005" },
                    { 12, 1000.00m, 6, new DateTime(2025, 1, 4, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Freelance", "550e8400-e29b-41d4-a716-446655440005" },
                    { 13, -90.00m, 7, new DateTime(2025, 2, 1, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Roupas", "550e8400-e29b-41d4-a716-446655440005" },
                    { 14, -35.00m, 5, new DateTime(2025, 2, 4, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Cinema", "550e8400-e29b-41d4-a716-446655440005" },
                    { 15, -60.00m, 5, new DateTime(2025, 2, 26, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Show", "550e8400-e29b-41d4-a716-446655440005" },
                    { 16, 1200.00m, 6, new DateTime(2025, 3, 5, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Freelance", "550e8400-e29b-41d4-a716-446655440005" },
                    { 17, -70.00m, 8, new DateTime(2024, 12, 25, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Restaurante", "550e8400-e29b-41d4-a716-446655440006" },
                    { 18, -1500.00m, 9, new DateTime(2025, 1, 1, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Notebook", "550e8400-e29b-41d4-a716-446655440006" },
                    { 19, -50.00m, 10, new DateTime(2025, 1, 27, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Jogo Online", "550e8400-e29b-41d4-a716-446655440006" },
                    { 20, -100.00m, 8, new DateTime(2025, 2, 3, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Supermercado", "550e8400-e29b-41d4-a716-446655440006" },
                    { 21, -30.00m, 10, new DateTime(2025, 2, 27, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Cinema", "550e8400-e29b-41d4-a716-446655440006" },
                    { 22, 1800.00m, 8, new DateTime(2025, 3, 4, 1, 32, 0, 359, DateTimeKind.Utc).AddTicks(4714), "Salário", "550e8400-e29b-41d4-a716-446655440006" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdvisorCustomers_CustomerId",
                table: "AdvisorCustomers",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_CategoryId",
                table: "Budgets",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Budgets_UserId",
                table: "Budgets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_UserId",
                table: "Categories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ReceiverId",
                table: "Messages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_SenderId",
                table: "Messages",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CategoryId",
                table: "Transactions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvisorCustomers");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Budgets");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
