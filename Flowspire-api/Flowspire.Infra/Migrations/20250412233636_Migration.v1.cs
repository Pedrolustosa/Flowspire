using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Flowspire.Infra.Migrations
{
    /// <inheritdoc />
    public partial class Migrationv1 : Migration
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
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Gender = table.Column<int>(type: "INTEGER", nullable: false),
                    AddressLine1 = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    AddressLine2 = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    State = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(type: "TEXT", maxLength: 20, nullable: true),
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
                    IsRead = table.Column<bool>(type: "INTEGER", nullable: false),
                    ReadAt = table.Column<DateTime>(type: "TEXT", nullable: true)
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
                name: "FinancialTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OriginalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Fee = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Discount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false),
                    PaymentMethod = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    IsRecurring = table.Column<bool>(type: "INTEGER", nullable: false),
                    NextOccurrence = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinancialTransactions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancialTransactions_Categories_CategoryId",
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
                columns: new[] { "Id", "AccessFailedCount", "AddressLine1", "AddressLine2", "BirthDate", "City", "ConcurrencyStamp", "Country", "CreatedAt", "Email", "EmailConfirmed", "FirstName", "Gender", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "PostalCode", "SecurityStamp", "State", "TwoFactorEnabled", "UpdatedAt", "UserName" },
                values: new object[,]
                {
                    { "550e8400-e29b-41d4-a716-446655440001", 0, "123 Admin St.", null, new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin City", null, "Admin Country", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin@flowspire.com", true, "Admin", 2, "User", false, null, "ADMIN@FLOWSPIRE.COM", "ADMIN@FLOWSPIRE.COM", "AQAAAAIAAYagAAAAEOHoVo8odfnYJO74+Zqgn9ehoAI2Qm+jKjz9sSslEPtS9a04CgbaoA6GL8DwhFB46Q==", "+15555550001", true, "00000", "ebdf372d-73ae-4beb-8a67-55b49543fd80", "Admin State", false, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin@flowspire.com" },
                    { "550e8400-e29b-41d4-a716-446655440002", 0, "101 Advisor Ave.", null, new DateTime(1985, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Advisor City", null, "Advisor Country", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "advisor1@flowspire.com", true, "Advisor", 0, "One", false, null, "ADVISOR1@FLOWSPIRE.COM", "ADVISOR1@FLOWSPIRE.COM", "AQAAAAIAAYagAAAAEODO1bcWNNq2C4wu157+LdOWJ/dAGhNVFB5njW6kMFLChyPtyioont4do/JOyCFwSg==", "+15555550002", true, "11111", "ceadc170-ca55-4d1c-834b-a8b18d06e6e7", "Advisor State", false, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "advisor1@flowspire.com" },
                    { "550e8400-e29b-41d4-a716-446655440003", 0, "102 Advisor Ave.", null, new DateTime(1987, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Advisor City", null, "Advisor Country", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "advisor2@flowspire.com", true, "Advisor", 0, "Two", false, null, "ADVISOR2@FLOWSPIRE.COM", "ADVISOR2@FLOWSPIRE.COM", "AQAAAAIAAYagAAAAEGEPSnBJkJw1mcskXXfh7yD+E8oKpU3vPnmtbZSv1OSDQV82+JlsV19Rq2b2XlgH1A==", "+15555550003", true, "11112", "0b1d8231-6a8d-47ae-9070-48384bc733b8", "Advisor State", false, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "advisor2@flowspire.com" },
                    { "550e8400-e29b-41d4-a716-446655440004", 0, "201 Customer Rd.", null, new DateTime(1990, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Customer City", null, "Customer Country", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "customer1@flowspire.com", true, "Customer", 1, "One", false, null, "CUSTOMER1@FLOWSPIRE.COM", "CUSTOMER1@FLOWSPIRE.COM", "AQAAAAIAAYagAAAAEPyVcPUyT6hWluiBpwyG3T/KE+wELnKzwMGJj/OIXKBxK2QTLMYTGtwB0Hwsv1uc9w==", "+15555550004", true, "22222", "57648e11-1c17-4095-a5de-e5eaac77ec48", "Customer State", false, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "customer1@flowspire.com" },
                    { "550e8400-e29b-41d4-a716-446655440005", 0, "202 Customer Rd.", null, new DateTime(1992, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Customer City", null, "Customer Country", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "customer2@flowspire.com", true, "Customer", 1, "Two", false, null, "CUSTOMER2@FLOWSPIRE.COM", "CUSTOMER2@FLOWSPIRE.COM", "AQAAAAIAAYagAAAAEIquTehMxQbUhtxxPyROVwNf9M7E7uesUK71xcE/rzNDG8XSQ92BA1jCSVLoejaw1w==", "+15555550005", true, "22223", "899e3ce0-8d09-447b-8920-f3d40b5a6353", "Customer State", false, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "customer2@flowspire.com" },
                    { "550e8400-e29b-41d4-a716-446655440006", 0, "203 Customer Rd.", null, new DateTime(1995, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Customer City", null, "Customer Country", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "customer3@flowspire.com", true, "Customer", 2, "Three", false, null, "CUSTOMER3@FLOWSPIRE.COM", "CUSTOMER3@FLOWSPIRE.COM", "AQAAAAIAAYagAAAAEFviGToBmTA4Fu56Ci3PZKf8fSTiuaqAa4vudEXsgEqk2LfS1fTOcQdwujZeqNdiyw==", "+15555550006", true, "22224", "a66f68e3-ea74-44c8-8686-bc9eb8494fa2", "Customer State", false, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "customer3@flowspire.com" }
                });

            migrationBuilder.InsertData(
                table: "AdvisorCustomers",
                columns: new[] { "AdvisorId", "CustomerId", "AssignedAt" },
                values: new object[,]
                {
                    { "550e8400-e29b-41d4-a716-446655440002", "550e8400-e29b-41d4-a716-446655440004", new DateTime(2025, 3, 23, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9782) },
                    { "550e8400-e29b-41d4-a716-446655440002", "550e8400-e29b-41d4-a716-446655440005", new DateTime(2025, 3, 28, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9782) },
                    { "550e8400-e29b-41d4-a716-446655440003", "550e8400-e29b-41d4-a716-446655440006", new DateTime(2025, 4, 2, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9782) }
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
                    { 1, "Food", "550e8400-e29b-41d4-a716-446655440004" },
                    { 2, "Transport", "550e8400-e29b-41d4-a716-446655440004" },
                    { 3, "Health", "550e8400-e29b-41d4-a716-446655440004" },
                    { 4, "Housing", "550e8400-e29b-41d4-a716-446655440004" },
                    { 5, "Leisure", "550e8400-e29b-41d4-a716-446655440005" },
                    { 6, "Education", "550e8400-e29b-41d4-a716-446655440005" },
                    { 7, "Clothing", "550e8400-e29b-41d4-a716-446655440005" },
                    { 8, "Food", "550e8400-e29b-41d4-a716-446655440006" },
                    { 9, "Technology", "550e8400-e29b-41d4-a716-446655440006" },
                    { 10, "Leisure", "550e8400-e29b-41d4-a716-446655440006" },
                    { 11, "Health", "550e8400-e29b-41d4-a716-446655440005" },
                    { 12, "Education", "550e8400-e29b-41d4-a716-446655440006" }
                });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "Content", "IsRead", "ReadAt", "ReceiverId", "SenderId", "SentAt" },
                values: new object[,]
                {
                    { 1, "Hello, I need help with my budget!", false, null, "550e8400-e29b-41d4-a716-446655440002", "550e8400-e29b-41d4-a716-446655440004", new DateTime(2025, 4, 2, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9690) },
                    { 2, "Sure, let’s review your expenses.", true, new DateTime(2025, 4, 4, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9690), "550e8400-e29b-41d4-a716-446655440004", "550e8400-e29b-41d4-a716-446655440002", new DateTime(2025, 4, 3, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9690) },
                    { 3, "What is the best investment now?", false, null, "550e8400-e29b-41d4-a716-446655440002", "550e8400-e29b-41d4-a716-446655440005", new DateTime(2025, 4, 7, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9690) },
                    { 4, "I recommend index funds.", true, new DateTime(2025, 4, 9, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9690), "550e8400-e29b-41d4-a716-446655440005", "550e8400-e29b-41d4-a716-446655440002", new DateTime(2025, 4, 8, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9690) },
                    { 5, "How can I reduce my expenses?", false, null, "550e8400-e29b-41d4-a716-446655440003", "550e8400-e29b-41d4-a716-446655440006", new DateTime(2025, 4, 9, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9690) },
                    { 6, "Let's review your categories.", false, null, "550e8400-e29b-41d4-a716-446655440006", "550e8400-e29b-41d4-a716-446655440003", new DateTime(2025, 4, 10, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9690) },
                    { 7, "Thank you for your help!", false, null, "550e8400-e29b-41d4-a716-446655440002", "550e8400-e29b-41d4-a716-446655440004", new DateTime(2025, 4, 11, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9690) },
                    { 8, "Lembre-se de registrar seus investimentos.", true, new DateTime(2025, 3, 30, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9690), "550e8400-e29b-41d4-a716-446655440005", "550e8400-e29b-41d4-a716-446655440002", new DateTime(2025, 3, 29, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9690) },
                    { 9, "Obrigado, acabei de atualizar.", true, new DateTime(2025, 3, 31, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9690), "550e8400-e29b-41d4-a716-446655440002", "550e8400-e29b-41d4-a716-446655440005", new DateTime(2025, 3, 30, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9690) },
                    { 10, "Quero aprender mais sobre reserva de emergência.", false, null, "550e8400-e29b-41d4-a716-446655440003", "550e8400-e29b-41d4-a716-446655440006", new DateTime(2025, 4, 7, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9690) },
                    { 11, "Vamos marcar uma call pra isso amanhã?", true, new DateTime(2025, 4, 8, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9690), "550e8400-e29b-41d4-a716-446655440006", "550e8400-e29b-41d4-a716-446655440003", new DateTime(2025, 4, 8, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9690) }
                });

            migrationBuilder.InsertData(
                table: "Budgets",
                columns: new[] { "Id", "Amount", "CategoryId", "EndDate", "StartDate", "UserId" },
                values: new object[,]
                {
                    { 10, 500.00m, 1, new DateTime(2025, 5, 12, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9490), new DateTime(2025, 2, 12, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9490), "550e8400-e29b-41d4-a716-446655440004" },
                    { 11, 120.00m, 2, new DateTime(2025, 5, 12, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9490), new DateTime(2025, 2, 12, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9490), "550e8400-e29b-41d4-a716-446655440004" },
                    { 12, 100.00m, 5, new DateTime(2025, 6, 12, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9490), new DateTime(2025, 3, 12, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9490), "550e8400-e29b-41d4-a716-446655440004" },
                    { 13, 600.00m, 6, new DateTime(2025, 6, 12, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9490), new DateTime(2025, 2, 12, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9490), "550e8400-e29b-41d4-a716-446655440005" },
                    { 14, 200.00m, 3, new DateTime(2025, 5, 12, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9490), new DateTime(2025, 1, 12, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9490), "550e8400-e29b-41d4-a716-446655440005" },
                    { 15, 3000.00m, 9, new DateTime(2025, 6, 12, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9490), new DateTime(2024, 12, 12, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9490), "550e8400-e29b-41d4-a716-446655440006" },
                    { 16, 200.00m, 10, new DateTime(2025, 7, 12, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9490), new DateTime(2025, 3, 12, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9490), "550e8400-e29b-41d4-a716-446655440006" }
                });

            migrationBuilder.InsertData(
                table: "FinancialTransactions",
                columns: new[] { "Id", "Amount", "CategoryId", "CreatedAt", "Date", "Description", "Discount", "Fee", "IsRecurring", "NextOccurrence", "Notes", "OriginalAmount", "PaymentMethod", "Type", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, -80.00m, 1, new DateTime(2024, 9, 27, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), new DateTime(2024, 9, 27, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), "Supermarket", null, null, false, null, "Compras básicas", -80.00m, "Cartão", 1, new DateTime(2024, 9, 27, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), "550e8400-e29b-41d4-a716-446655440004" },
                    { 2, -15.00m, 2, new DateTime(2024, 10, 2, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), new DateTime(2024, 10, 2, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), "Bus", null, null, false, null, "Transporte público", -15.00m, "Dinheiro", 1, new DateTime(2024, 10, 2, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), "550e8400-e29b-41d4-a716-446655440004" },
                    { 3, 2000.00m, 1, new DateTime(2024, 10, 11, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), new DateTime(2024, 10, 11, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), "Salary", null, null, true, null, "Salário mensal", 2000.00m, "Transferência", 0, new DateTime(2024, 10, 11, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), "550e8400-e29b-41d4-a716-446655440004" },
                    { 4, -50.00m, 3, new DateTime(2024, 11, 7, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), new DateTime(2024, 11, 7, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), "Pharmacy", null, null, false, null, "Remédios", -50.00m, "Cartão", 1, new DateTime(2024, 11, 7, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), "550e8400-e29b-41d4-a716-446655440004" },
                    { 5, -800.00m, 4, new DateTime(2024, 11, 9, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), new DateTime(2024, 11, 9, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), "Rent", null, null, true, null, "Aluguel mensal", -800.00m, "Transferência", 1, new DateTime(2024, 11, 9, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), "550e8400-e29b-41d4-a716-446655440004" },
                    { 6, -39.90m, 5, new DateTime(2024, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Netflix Subscription", null, null, true, new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Entretenimento mensal", -39.90m, "Cartão", 1, new DateTime(2024, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "550e8400-e29b-41d4-a716-446655440004" },
                    { 7, -22.50m, 2, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Uber Ride", null, 1.50m, false, null, "Corrida rápida", -22.50m, "Pix", 1, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "550e8400-e29b-41d4-a716-446655440004" },
                    { 8, 1800.00m, 1, new DateTime(2025, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Freelance Project", null, null, false, null, "Projeto de site", 1800.00m, "Transferência", 0, new DateTime(2025, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "550e8400-e29b-41d4-a716-446655440004" },
                    { 20, -40.00m, 5, new DateTime(2024, 10, 5, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), new DateTime(2024, 10, 5, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), "Cinema", null, null, false, null, "Filme em cartaz", -40.00m, "Cartão", 1, new DateTime(2024, 10, 5, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), "550e8400-e29b-41d4-a716-446655440005" },
                    { 21, -150.00m, 6, new DateTime(2024, 10, 9, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), new DateTime(2024, 10, 9, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), "Online Course", null, null, false, null, "Curso de investimento", -150.00m, "Cartão", 1, new DateTime(2024, 10, 9, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), "550e8400-e29b-41d4-a716-446655440005" },
                    { 22, 1000.00m, 6, new DateTime(2024, 10, 10, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), new DateTime(2024, 10, 10, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), "Freelance", null, null, false, null, "Serviço de edição", 1000.00m, "Transferência", 0, new DateTime(2024, 10, 10, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), "550e8400-e29b-41d4-a716-446655440005" },
                    { 23, -19.90m, 5, new DateTime(2024, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Spotify Premium", null, null, true, new DateTime(2025, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Assinatura mensal", -19.90m, "Cartão", 1, new DateTime(2024, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "550e8400-e29b-41d4-a716-446655440005" },
                    { 24, -250.00m, 3, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dentist appointment", 50.00m, null, false, null, "Consulta odontológica", -250.00m, "Cartão", 1, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "550e8400-e29b-41d4-a716-446655440005" },
                    { 35, -70.00m, 8, new DateTime(2024, 9, 30, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), new DateTime(2024, 9, 30, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), "Restaurant", null, null, false, null, "Jantar fora", -70.00m, "Pix", 1, new DateTime(2024, 9, 30, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), "550e8400-e29b-41d4-a716-446655440006" },
                    { 36, -1500.00m, 9, new DateTime(2024, 10, 7, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), new DateTime(2024, 10, 7, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), "Notebook", null, null, false, null, "Compra de notebook", -1500.00m, "Cartão", 1, new DateTime(2024, 10, 7, 23, 36, 35, 515, DateTimeKind.Utc).AddTicks(9328), "550e8400-e29b-41d4-a716-446655440006" },
                    { 37, -89.90m, 6, new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Udemy Course: C# Clean Architecture", 40.00m, null, false, null, "Curso avançado", -89.90m, "Cartão", 1, new DateTime(2025, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "550e8400-e29b-41d4-a716-446655440006" },
                    { 38, 150.00m, 10, new DateTime(2025, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Birthday Gift - Pix Received", null, null, false, null, "Presente de aniversário", 150.00m, "Pix", 0, new DateTime(2025, 4, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "550e8400-e29b-41d4-a716-446655440006" }
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
                name: "IX_FinancialTransactions_CategoryId",
                table: "FinancialTransactions",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialTransactions_UserId",
                table: "FinancialTransactions",
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
                name: "FinancialTransactions");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
